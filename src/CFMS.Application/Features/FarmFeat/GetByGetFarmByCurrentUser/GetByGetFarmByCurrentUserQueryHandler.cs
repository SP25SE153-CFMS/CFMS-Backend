using CFMS.Application.Common;
using CFMS.Application.DTOs.Farm;
using CFMS.Application.Features.FarmFeat.GetFarmByCurrentUserId;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.FarmFeat.GetByGetFarmByCurrentUser
{
    public class GetByGetFarmByCurrentUserQueryHandler : IRequestHandler<GetByGetFarmByCurrentUserQuery, BaseResponse<IEnumerable<FarmWithRoleResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;

        public GetByGetFarmByCurrentUserQueryHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }

        public async Task<BaseResponse<IEnumerable<FarmWithRoleResponse>>> Handle(GetByGetFarmByCurrentUserQuery request, CancellationToken cancellationToken)
        {
            var currentUser = _currentUserService.GetUserId();
            Guid userId = Guid.Parse(_currentUserService.GetUserId());

            var farms = _unitOfWork.FarmRepository
                .GetIncludeMultiLayer(
                    filter: f => f.FarmEmployees.Any(fe => fe.UserId == userId) && !f.IsDeleted,
                    include: f => f.Include(f => f.FarmEmployees)
                )
                .Select(f => new FarmWithRoleResponse
                {
                    FarmId = f.FarmId,
                    FarmName = f.FarmName,
                    FarmCode = f.FarmCode,
                    Address = f.Address,
                    Area = f.Area,
                    AreaUnitId = f.AreaUnitId,
                    Scale = f.Scale,
                    Longitude = f.Longitude,
                    Latitude = f.Latitude,
                    PhoneNumber = f.PhoneNumber,
                    Website = f.Website,
                    ImageUrl = f.ImageUrl,
                    FarmRole = f.FarmEmployees?.FirstOrDefault(fe => fe.UserId.Equals(userId))?.FarmRole
                })
                .ToList();

            if (farms == null)
            {
                return BaseResponse<IEnumerable<FarmWithRoleResponse>>.SuccessResponse(message: "Trang trại không tồn tại");
            }

            return BaseResponse<IEnumerable<FarmWithRoleResponse>>.SuccessResponse(data: farms);
        }
    }
}
