using CFMS.Application.Common;
using CFMS.Application.Services.SignalR;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CFMS.Application.Features.FarmFeat.InviteEnrollFarm
{
    public class InviteEnrollFarmDecisionCommandHandler : IRequestHandler<InviteEnrollFarmDecisionCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;
        private readonly NotiHub _hubContext;

        public InviteEnrollFarmDecisionCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService, NotiHub hubContext)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
            _hubContext = hubContext;
        }

        public async Task<BaseResponse<bool>> Handle(InviteEnrollFarmDecisionCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.GetUserId();
            var existUser = _unitOfWork.UserRepository.Get(filter: u => u.UserId.ToString().Equals(userId)).FirstOrDefault();
            if (existUser == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Người dùng không tồn tại");
            }

            var existNoti = _unitOfWork.NotificationRepository.Get(filter: n => n.NotificationId.Equals(request.NotificationId)).FirstOrDefault();
            if (existNoti == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Thông báo không tồn tại");
            }

            if (existNoti.IsRead.Equals(3))
            {
                return BaseResponse<bool>.FailureResponse(message: "Yêu cầu này đã được chấp nhận");
            }

            if (existNoti.IsRead.Equals(4))
            {
                return BaseResponse<bool>.FailureResponse(message: "Yêu cầu này đã bị từ chối");
            }

            var input = existNoti.Content;
            var match = Regex.Match(input, @"trang trại\s+(\S+)\s+\(");
            var farmCode = match.Success ? match.Groups[1].Value : "Không xác định";

            var existFarm = _unitOfWork.FarmRepository.Get(filter: f => f.FarmCode.Equals(farmCode) && f.IsDeleted == false).FirstOrDefault();
            if (existFarm == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Trang trại không tồn tại");
            }

            try
            {
                existNoti.IsRead = request.Decision.Equals(1) ? 3 : 4;

                _unitOfWork.NotificationRepository.Update(existNoti);

                if (existNoti.NotificationType.Contains("INVITE"))
                {
                    if (request.Decision.Equals(1))
                    {
                        var matchInvite = Regex.Match(existNoti.Content, @"đảm nhận vị trí\s+(.*?)\s+(trong trang trại)", RegexOptions.IgnoreCase);
                        var farmRole = matchInvite.Success ? matchInvite.Groups[1].Value : "Không xác định";

                        existFarm.FarmEmployees.Add(new FarmEmployee
                        {
                            FarmId = existFarm.FarmId,
                            UserId = existUser.UserId,
                            StartDate = DateTime.Now.ToLocalTime().AddHours(7),
                            Status = 1,
                            FarmRole = farmRole switch
                            {
                                "nhân viên" => 3,
                                "quản lý" => 4,
                                "chủ trang trại" => 5,
                                _ => 3
                            }
                        });

                        _unitOfWork.FarmRepository.Update(existFarm);
                    }

                    var managerFarms = _unitOfWork.FarmEmployeeRepository.GetIncludeMultiLayer(filter: f => f.FarmId.Equals(existFarm.FarmId) && (f.FarmRole > 4) && f.Status == 1 && f.IsDeleted == false,
                                            include: x => x
                                            .Include(f => f.User)
                                            ).ToList();

                    var sendTasks = managerFarms
                        .Select(mf =>
                        {
                            var msg = new Notification
                            {
                                UserId = mf.UserId,
                                NotificationName = "Thông báo mời tham gia trang trại",
                                NotificationType = "INVITE_FARM_DECISION",
                                Content = $"{existUser.FullName} đã {request.Decision switch { 1 => "đã chấp nhận", 0 => "đã từ chối", _ => "vẫn đang suy nghĩ về" }} lời mời tham gia trang trại {existFarm.FarmCode} ({existFarm.FarmName})",
                                IsRead = 0,
                            };

                            _unitOfWork.NotificationRepository.Insert(msg);
                            return _hubContext.SendMessageToUser(mf.UserId.ToString(), msg);
                        });

                    await System.Threading.Tasks.Task.WhenAll(sendTasks);

                    var notiReceive = new Notification
                    {
                        UserId = existUser.UserId,
                        NotificationName = "Thông báo mời tham gia trang trại",
                        NotificationType = "INVITE_FARM_DECISION",
                        Content = $"Bạn {request.Decision switch { 1 => "đã chấp nhận", 0 => "đã từ chối", _ => "vẫn đang suy nghĩ về" }} lời mời tham gia trang trại {existFarm.FarmCode} ({existFarm.FarmName}). Hãy vào tương tác ngay",
                        IsRead = 0,
                    };

                    _unitOfWork.NotificationRepository.Insert(notiReceive);
                    await _hubContext.SendMessageToUser(notiReceive?.UserId?.ToString(), notiReceive);
                }

                if (existNoti.NotificationType.Contains("ENROLL"))
                {
                    if (request.Decision.Equals(1))
                    {
                        var matchEnroll= Regex.Match(existNoti.Content, @"đảm nhận vị trí\s+(.*?)\s+(trong trang trại)", RegexOptions.IgnoreCase);
                        var farmRole = matchEnroll.Success ? matchEnroll.Groups[1].Value : "Không xác định";

                        existFarm.FarmEmployees.Add(new FarmEmployee
                        {
                            FarmId = existFarm.FarmId,
                            UserId = existNoti.UserId,
                            StartDate = DateTime.Now.ToLocalTime().AddHours(7),
                            Status = 1,
                            FarmRole = farmRole switch
                            {
                                "nhân viên" => 3,
                                "quản lý" => 4,
                                "chủ trang trại" => 5,
                                _ => 3
                            }
                        });

                        _unitOfWork.FarmRepository.Update(existFarm);
                    }

                    var notiSend = new Notification
                    {
                        UserId = existNoti.UserId,
                        NotificationName = "Thông báo yêu cầu tham gia trang trại",
                        NotificationType = "ENROLL_FARM_DECISION",
                        Content = $"Bạn {request.Decision switch {1 => "đã được chấp nhận", 0 => "đã bị từ chối", _ => "vẫn đang chờ phê duyệt"}} tham gia trang trại {existFarm.FarmCode} ({existFarm.FarmName})",
                        IsRead = 0,
                    };

                    _unitOfWork.NotificationRepository.Insert(notiSend);
                    await _hubContext.SendMessageToUser(notiSend?.UserId?.ToString(), notiSend);

                    var notiReceive = new Notification
                    {
                        UserId = existUser.UserId,
                        NotificationName = "Thông báo yêu cầu tham gia trang trại",
                        NotificationType = "ENROLL_FARM_DECISION",
                        Content = $"Bạn {request.Decision switch {1 => "đã chấp nhận", 0 => "đã bị từ chối", _ => "vẫn đang phê duyệt"}} yêu cầu tham gia trang trại {existFarm.FarmCode} ({existFarm.FarmName} của {existUser.FullName})",
                        IsRead = 0,
                    };

                    _unitOfWork.NotificationRepository.Insert(notiReceive);
                    await _hubContext.SendMessageToUser(notiReceive?.UserId?.ToString(), notiReceive);
                }

                await _unitOfWork.SaveChangesAsync();

                return request.Decision.Equals(1)
                    ? BaseResponse<bool>.SuccessResponse(message: "Bạn đã chấp nhận lời đề nghị thành công")
                    : BaseResponse<bool>.SuccessResponse(message: "Bạn đã từ chối lời đề nghị thành công");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse("Thất bại: " + ex.Message);
            }
        }
    }
}
