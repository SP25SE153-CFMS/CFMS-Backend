using CFMS.Application.Common;
using CFMS.Application.Events;
using CFMS.Application.Features.FarmFeat.Create;
using CFMS.Application.Services.SignalR;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CFMS.Application.Features.FarmFeat.InviteEnrollFarm
{
    public class InviteEnrollFarmCommandHandler : IRequestHandler<InviteEnrollFarmCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;
        private readonly NotiHub _hubContext;

        public InviteEnrollFarmCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService, NotiHub hubContext)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
            _hubContext = hubContext;
        }

        public async Task<BaseResponse<bool>> Handle(InviteEnrollFarmCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.GetUserId();
            var existUser = _unitOfWork.UserRepository.GetIncludeMultiLayer(filter: u => u.UserId.ToString().Equals(userId),
                include: x => x
                .Include(f => f.FarmEmployees)
                ).FirstOrDefault();
            if (existUser == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Người dùng không tồn tại");
            }

            var method = request.MethodAccess.ToLower();
            if (method != "invite" && method != "enroll" && method != "invitation" && method != "enrollment")
            {
                return BaseResponse<bool>.SuccessResponse(message: "Phương thức không hợp lệ");
            }

            var existFarm = _unitOfWork.FarmRepository.GetIncludeMultiLayer(filter: f => f.FarmCode.Equals(request.FarmCode) && f.IsDeleted == false,
                include: x => x
                .Include(f => f.FarmEmployees)
                   .ThenInclude(fe => fe.User)
                ).FirstOrDefault();
            if (existFarm == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Trang trại không tồn tại");
            }

            try
            {
                if (method.Equals("invite") || method.Equals("invitation"))
                {
                    if (existUser.FarmEmployees.FirstOrDefault(x => x.FarmId.Equals(existFarm.FarmId))?.FarmRole < 4
                        || existUser.FarmEmployees.FirstOrDefault(x => x.FarmId.Equals(existFarm.FarmId)) == null)
                    {
                        return BaseResponse<bool>.FailureResponse("Bạn không có quyền mời người khác tham gia trang trại");
                    }

                    var existInvitedUsers = existFarm.FarmEmployees.FirstOrDefault(t => request.employessInvitation.Select(x => x.UserId).Contains(t.UserId ?? Guid.Empty));
                    if (existInvitedUsers != null)
                    {
                        return BaseResponse<bool>.FailureResponse(message: $"{existInvitedUsers?.User?.FullName} đã thuộc trang trại này rồi");
                    }

                    var invitedNoti = _unitOfWork.NotificationRepository.GetIncludeMultiLayer(filter: x => request.employessInvitation.Select(x => x.UserId).Contains(x.UserId) 
                    && x.CreatedByUserId.Equals(existUser.UserId) 
                    && x.NotificationType.Equals("INVITE_FARM")
                    && x.IsDeleted == false,
                        include: x => x
                        .Include(f => f.User)
                        )
                        .OrderByDescending(x => x.CreatedWhen)
                        .Take(10)
                        .ToList();

                    if (invitedNoti != null)
                    {
                        foreach (var item in invitedNoti)
                        {
                            var input = item.Content;
                            var match = Regex.Match(input, @"trang trại\s+(\S+)\s+\(");
                            var farmCode = match.Success ? match.Groups[1].Value : "Không xác định";

                            var invitedUser = _unitOfWork.NotificationRepository.GetIncludeMultiLayer(filter: x => request.employessInvitation.Select(x => x.UserId).Contains(x.UserId)
                            && x.CreatedByUserId.Equals(existUser.UserId)
                            && x.NotificationType.Equals("INVITE_FARM")
                            && existFarm.FarmCode.Equals(farmCode)
                            && x.IsDeleted == false,
                                include: x => x
                                .Include(f => f.User)
                                ).FirstOrDefault();

                            if ((invitedUser != null) && (DateTime.Now.ToLocalTime() - invitedUser.CreatedWhen).TotalMinutes < 3)
                            {
                                return BaseResponse<bool>.FailureResponse(message: $"Bạn đã mời {invitedUser?.User?.FullName} vào trang trại {existFarm.FarmCode} ({existFarm.FarmName}) rồi. Hãy đợi phản hồi");
                            }
                        }
                    }

                    foreach (var x in request.employessInvitation)
                    {
                        var role = request.FarmRole switch
                        {
                            3 => "nhân viên",
                            4 => "quản lý",
                            5 => "chủ trang trại",
                            _ => "không xác định"
                        };

                        var notiSend = new Notification
                        {
                            UserId = x.UserId,
                            NotificationName = "Thông báo mời tham gia trang trại",
                            NotificationType = "INVITE_FARM",
                            Content = $"{existUser.FullName} đã mời bạn đảm nhận vị trí {role} trong trang trại {existFarm.FarmCode} ({existFarm.FarmName})",
                            IsRead = 0,
                        };

                        _unitOfWork.NotificationRepository.Insert(notiSend);
                        await _hubContext.SendMessageToUser(x.UserId.ToString()!, notiSend);
                    }

                    string? userInvited = "";
                    if (request.employessInvitation.Count.Equals(1))
                    {
                        userInvited = _unitOfWork.UserRepository.Get(filter: x => x.UserId.Equals(request.employessInvitation.FirstOrDefault().UserId)).FirstOrDefault()?.FullName;
                    }

                    var roleName = request.FarmRole switch
                    {
                        3 => "nhân viên",
                        4 => "quản lý",
                        5 => "chủ trang trại",
                        _ => "không xác định"
                    };

                    var notiReceive = new Notification
                    {
                        UserId = existUser.UserId,
                        NotificationName = "Thông báo mời tham gia trang trại",
                        NotificationType = "INVITE_FARM",
                        Content = $"Bạn đã mời {request?.employessInvitation.Count switch
                        {
                            1 => userInvited,
                            _ => $"{request.employessInvitation.Count}"
                        }} đảm nhận vị trí {roleName} trong trang trại {existFarm.FarmCode} ({existFarm.FarmName}) thành công",
                        IsRead = 0,
                    };

                    _unitOfWork.NotificationRepository.Insert(notiReceive);
                    await _hubContext.SendMessageToUser(existUser.UserId.ToString(), notiReceive);
                }

                if (method.Equals("enroll") || method.Equals("enrollment"))
                {
                    var existEnrolledUser = existFarm.FarmEmployees.FirstOrDefault(t => existUser.UserId.Equals(t.UserId));

                    if (existEnrolledUser != null)
                    {
                        return BaseResponse<bool>.FailureResponse(message: "Bạn đã tham gia trang trại này rồi");
                    }

                    var enrolledNoti = _unitOfWork.NotificationRepository.GetIncludeMultiLayer(filter: x => x.CreatedByUserId.Equals(existUser.UserId)
                    && x.NotificationType.Equals("ENROLL_FARM")
                    && x.IsDeleted == false,
                        include: x => x
                        .Include(f => f.User)
                        )
                        .OrderByDescending(x => x.CreatedWhen)
                        .Take(10)
                        .ToList();

                    if (enrolledNoti != null)
                    {
                        foreach (var item in enrolledNoti)
                        {
                            var input = item.Content;
                            var match = Regex.Match(input, @"trang trại\s+(\S+)\s+\(");
                            var farmCode = match.Success ? match.Groups[1].Value : "Không xác định";

                            var invitedUser = _unitOfWork.NotificationRepository.GetIncludeMultiLayer(filter: x => x.CreatedByUserId.Equals(existUser.UserId)
                            && existFarm.FarmCode.Equals(farmCode)
                            && x.IsDeleted == false,
                                include: x => x
                                .Include(f => f.User)
                                ).FirstOrDefault();

                            if ((invitedUser != null) && (DateTime.Now.ToLocalTime() - invitedUser.CreatedWhen).TotalMinutes < 3)
                            {
                                return BaseResponse<bool>.FailureResponse(message: $"Bạn đã gửi lời yêu cầu tham gia trang trại {existFarm.FarmCode} ({existFarm.FarmName}) rồi. Hãy đợi phê duyệt");
                            }
                        }
                    }

                    var managerFarms = _unitOfWork.FarmEmployeeRepository.GetIncludeMultiLayer(filter: f => f.FarmId.Equals(existFarm.FarmId) && (f.FarmRole > 4) && f.Status == 1 && f.IsDeleted == false,
                        include: x => x
                        .Include(f => f.User)
                        ).ToList();

                    var sendTasks = managerFarms
                        .Select(mf =>
                        {
                            var notiSend = new Notification
                            {
                                UserId = mf.UserId,
                                NotificationName = "Thông báo yêu cầu tham gia trang trại",
                                NotificationType = "ENROLL_FARM",
                                Content = $"{existUser.FullName} gửi yêu cầu tham gia trang trại {existFarm.FarmCode} ({existFarm.FarmName})",
                                IsRead = 0,
                            };

                            _unitOfWork.NotificationRepository.Insert(notiSend);
                            return _hubContext.SendMessageToUser(mf.UserId.ToString()!, notiSend);
                        });
                    await System.Threading.Tasks.Task.WhenAll(sendTasks);

                    var notiReceive = new Notification
                    {
                        UserId = existUser.UserId,
                        NotificationName = "Thông báo yêu cầu tham gia trang trại",
                        NotificationType = "ENROLL_FARM",
                        Content = $"Bạn đã gửi yêu cầu tham gia trang trại {existFarm.FarmCode} ({existFarm.FarmName}) thành công",
                        IsRead = 0,
                    };

                    _unitOfWork.NotificationRepository.Insert(notiReceive);
                    await _hubContext.SendMessageToUser(existUser.UserId.ToString(), notiReceive);
                }

                return method.Equals("invite") || method.Equals("invitation")
                    ? BaseResponse<bool>.SuccessResponse(message: "Gửi lời mời thành công")
                    : BaseResponse<bool>.SuccessResponse(message: "Gửi yêu cầu tham gia thành công");
            }
            catch (Exception ex)
            {
                return method.Equals("invite") || method.Equals("invitation")
                    ? BaseResponse<bool>.FailureResponse(message: "Gửi lời mời thất bại: " + ex.Message)
                    : BaseResponse<bool>.FailureResponse(message: "Gửi yêu cầu tham gia thất bại: " + ex.Message);
            }
        }
    }
}
