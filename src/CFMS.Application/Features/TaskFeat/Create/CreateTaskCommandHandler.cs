﻿using AutoMapper;
using CFMS.Application.Common;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.TaskFeat.Create
{
    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public CreateTaskCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<bool>> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var taskType = _unitOfWork.SubCategoryRepository.Get(filter: t => t.SubCategoryId.Equals(request.TaskTypeId) && t.IsDeleted == false).FirstOrDefault();

                foreach (var date in request.StartWorkDate)
                {
                    foreach (var shiftId in request.ShiftIds)
                    {
                        var task = _mapper.Map<Domain.Entities.Task>(request);

                        task.StartWorkDate = date;
                        task.FarmId = request.FarmId;
                        task.Status = 0;
                        task.IsHarvest = taskType.ToString().ToLower().Equals("harvest") ? 1 : 0;

                        var existShift = _unitOfWork.ShiftRepository.Get(noTracking: true, filter: s => s.ShiftId.Equals(shiftId) && s.IsDeleted == false).FirstOrDefault();
                        if (existShift == null)
                        {
                            return BaseResponse<bool>.FailureResponse(message: "Ca làm việc không tồn tại");
                        }

                        task.ShiftSchedules.Add(new ShiftSchedule
                        {
                            ShiftId = existShift.ShiftId,
                            Date = DateOnly.FromDateTime(DateTime.Now.ToLocalTime()),
                        });

                        foreach (var taskResource in request.TaskResources)
                        {
                            var existResource = _unitOfWork.ResourceRepository.Get(filter: r => r.ResourceId.Equals(taskResource.ResourceId) && r.IsDeleted == false).FirstOrDefault();
                            if (existResource == null)
                            {
                                return BaseResponse<bool>.FailureResponse(message: "Hàng hoá không tồn tại");
                            }

                            task.TaskResources.Add(new TaskResource
                            {
                                ResourceId = existResource.ResourceId,
                                ResourceTypeId = existResource.ResourceTypeId,
                                UnitId = existResource.UnitId,
                                Quantity = taskResource.SuppliedQuantity,
                            });
                        }

                        if (request.LocationType?.ToUpper() == "COOP")
                        {
                            var coopExists = _unitOfWork.ChickenCoopRepository
                                .Get(filter: c => c.ChickenCoopId.Equals(request.LocationId) && c.BreedingArea.FarmId.Equals(request.FarmId) && !c.IsDeleted)
                                .FirstOrDefault();

                            if (coopExists == null)
                                return BaseResponse<bool>.FailureResponse("Chuồng gà không tồn tại");

                            task.TaskLocations.Add(new TaskLocation
                            {
                                CoopId = request.LocationId,
                                LocationType = request.LocationType
                            });
                        }
                        else if (request.LocationType?.ToUpper() == "WARE")
                        {
                            var wareExists = _unitOfWork.WarehouseRepository
                                .Get(filter: w => w.WareId.Equals(request.LocationId) && !w.IsDeleted)
                                .FirstOrDefault();

                            if (wareExists == null)
                                return BaseResponse<bool>.FailureResponse("Kho không tồn tại");

                            task.TaskLocations.Add(new TaskLocation
                            {
                                WareId = request.LocationId,
                                LocationType = request.LocationType
                            });
                        }
                        else
                        {
                            return BaseResponse<bool>.FailureResponse("Loại vị trí không hợp lệ");
                        }

                        _unitOfWork.TaskRepository.Insert(task);
                    }
                }

                var result = await _unitOfWork.SaveChangesAsync();
                if (result > 0)
                {
                    return BaseResponse<bool>.SuccessResponse(message: "Tạo thành công");
                }
                return BaseResponse<bool>.FailureResponse(message: "Tạo không thành công");
            }
            catch (Exception e)
            {
                return BaseResponse<bool>.FailureResponse(message: "Có lỗi xảy ra");
            }
        }
    }
}
