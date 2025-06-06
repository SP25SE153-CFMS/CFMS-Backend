﻿using AutoMapper;
using CFMS.Application.Common;
using CFMS.Application.DTOs.Image;
using CFMS.Application.DTOs.Request;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CFMS.Application.Features.RequestFeat.Create
{
    public class CreateRequestCommandHandler : IRequestHandler<CreateRequestCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly IGoogleDriveService _googleDriveService;

        public CreateRequestCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ICurrentUserService currentUserService, IGoogleDriveService googleDriveService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _googleDriveService = googleDriveService;
        }

        public async Task<BaseResponse<bool>> Handle(CreateRequestCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = _currentUserService.GetUserId();

                //var lastRequest = _unitOfWork.RequestRepository
                //    .Get(filter: r => r.CreatedByUser.UserId.ToString().Equals(user))
                //    .OrderByDescending(r => r.CreatedWhen)
                //    .FirstOrDefault();

                var requestType = request.IsInventoryRequest
                    ? _unitOfWork.SubCategoryRepository.Get(filter: x => x.SubCategoryName.Contains("Xuất/Nhập")).FirstOrDefault()
                    : _unitOfWork.SubCategoryRepository.Get(filter: x => x.SubCategoryName.Contains("Báo cáo")).FirstOrDefault();

                //if (lastRequest != null && (DateTime.Now.ToLocalTime() - lastRequest.CreatedWhen).TotalSeconds < 10)
                //{
                //    return BaseResponse<bool>.FailureResponse("Bạn không thể tạo yêu cầu quá nhanh. Vui lòng thử lại sau");
                //}
                
                var newRequest = _mapper.Map<Request>(request);
                newRequest.RequestTypeId = requestType?.SubCategoryId;
                _unitOfWork.RequestRepository.Insert(newRequest);

                await _unitOfWork.SaveChangesAsync();

                if (request.IsInventoryRequest)
                {
                    var inventoryRequest = new InventoryRequest
                    {
                        RequestId = newRequest.RequestId,
                        InventoryRequestTypeId = request.InventoryRequestTypeId,
                        WareFromId = request.WareFromId ?? null,
                        WareToId = request.WareToId ?? null
                    };

                    var wareId = request.WareFromId != null
                                    ? request.WareFromId
                                    : request.WareToId != null
                                        ? request.WareToId
                                        : null;

                    _unitOfWork.InventoryRequestRepository.Insert(inventoryRequest);
                    await _unitOfWork.SaveChangesAsync();

                    var ware = _unitOfWork.WarehouseRepository.GetIncludeMultiLayer(filter: x => x.WareId.Equals(wareId) && x.IsDeleted == false,
                        include: x => x
                        .Include(t => t.Farm)
                        ).FirstOrDefault();

                    foreach (var detail in request.InventoryDetails)
                    {
                        var inventoryRequestDetail = new InventoryRequestDetail
                        {
                            InventoryRequestId = inventoryRequest.InventoryRequestId,
                            ResourceId = detail.ResourceId,
                            ResourceSupplierId = detail.ResourceSupplierId,
                            ExpectedQuantity = detail.ExpectedQuantity,
                            UnitId = detail.UnitId,
                            Reason = request?.Reason,
                            ExpectedDate = request?.ExpectedDate.Value.ToLocalTime(),
                            Note = request?.Note
                        };
                        _unitOfWork.InventoryRequestDetailRepository.Insert(inventoryRequestDetail);
                    }

                    newRequest.FarmId = ware?.FarmId;
                    _unitOfWork.RequestRepository.Update(newRequest);
                }
                else
                {
                    var taskRequest = new TaskRequest
                    {
                        RequestId = newRequest.RequestId,
                        Title = request?.TaskRequestRequest?.Title,
                        Priority = request?.TaskRequestRequest?.Priority,
                        Description = request?.TaskRequestRequest?.Description,
                        ImageUrl = request?.TaskRequestRequest?.ImageUrl,
                    };

                    _unitOfWork.TaskRequestRepository.Insert(taskRequest);

                    newRequest.FarmId = request?.TaskRequestRequest?.FarmId;
                    _unitOfWork.RequestRepository.Update(newRequest);
                }

                var result = await _unitOfWork.SaveChangesAsync();

                return result > 0
                    ? BaseResponse<bool>.SuccessResponse("Tạo yêu cầu thành công")
                    : BaseResponse<bool>.FailureResponse("Thêm thất bại");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse("Có lỗi xảy ra: " + ex.Message);
            }
        }
    }
}