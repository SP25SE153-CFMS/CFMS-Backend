using CFMS.Application.Common;
using CFMS.Application.DTOs.Farm;
using CFMS.Application.DTOs.Task;
using CFMS.Application.Features.FarmFeat.GetFarm;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.FarmFeat.GetFarmByCurrentEmployeeByFarmId
{
    public class GetFarmByCurrentEmployeeByFarmIdQueryHandler : IRequestHandler<GetFarmByCurrentEmployeeByFarmIdQuery, BaseResponse<FarmResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;

        public GetFarmByCurrentEmployeeByFarmIdQueryHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }

        public async Task<BaseResponse<FarmResponse>> Handle(GetFarmByCurrentEmployeeByFarmIdQuery request, CancellationToken cancellationToken)
        {
            var currentEmployee = _currentUserService.GetUserId();
            Guid user = Guid.Parse(currentEmployee);

            var existFarm = _unitOfWork.FarmRepository.Get(
                filter: f => f.FarmId.Equals(request.Id) && !f.IsDeleted
            ).FirstOrDefault();

            if (existFarm == null)
            {
                return BaseResponse<FarmResponse>.FailureResponse(message: "Trang trại không tồn tại");
            }

            var tasks = _unitOfWork.TaskRepository.Get(
                filter: t => t.FarmId.Equals(existFarm.FarmId) && t.Assignments.Any(x => x.AssignedToId.Equals(user)) && !t.IsDeleted
            ).ToList();

            var taskTypeIds = tasks.Select(t => t.TaskTypeId).Where(id => id.HasValue).Distinct().ToList();

            var subCategoryIds = taskTypeIds.Append(existFarm.AreaUnitId).Where(id => id.HasValue).Distinct().ToList();

            var subCategories = _unitOfWork.SubCategoryRepository.Get(
                filter: s => subCategoryIds.Contains(s.SubCategoryId)
            ).ToList();

            var taskDtos = tasks.Select(t => new TaskDto
            {
                TaskName = t.TaskName,
                TaskType = subCategories.FirstOrDefault(s => s.SubCategoryId.Equals(t.TaskTypeId))?.SubCategoryName,
                Description = t.Description,
                Status = t.Status
            }).ToList();

            var areaUnitName = subCategories.FirstOrDefault(s => s.SubCategoryId.Equals(existFarm.AreaUnitId))?.SubCategoryName;

            var farmResponse = new FarmResponse
            {
                FarmName = existFarm.FarmName,
                FarmCode = existFarm.FarmCode,
                Address = existFarm.Address,
                Area = $"{existFarm.Area} {subCategories.FirstOrDefault(s => s.SubCategoryId.Equals(existFarm.AreaUnitId))?.SubCategoryName}",
                Scale = existFarm.Scale,
                Longitude = existFarm.Longitude,
                Latitude = existFarm.Latitude,
                PhoneNumber = existFarm.PhoneNumber,
                Website = existFarm.Website,
                ImageUrl = existFarm.ImageUrl,
                TaskList = taskDtos
            };

            return BaseResponse<FarmResponse>.SuccessResponse(farmResponse);

        }
    }
}
