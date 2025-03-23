using CFMS.Application.Common;
using CFMS.Application.Features.FarmFeat.GetFarm;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.FarmFeat.GetFarmByUserId
{
    public class GetFarmByUserIdQueryHandler : IRequestHandler<GetFarmByUserIdQuery, BaseResponse<IEnumerable<Farm>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetFarmByUserIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<IEnumerable<Farm>>> Handle(GetFarmByUserIdQuery request, CancellationToken cancellationToken)
        {
            var existUser = _unitOfWork.UserRepository.Get(filter: u => u.UserId.Equals(request.UserId)).FirstOrDefault();
            var existFarm = _unitOfWork.FarmRepository.Get(filter: f => f.CreatedByUserId.Equals(existUser.UserId) && f.IsDeleted == false, includeProperties: "BreedingAreas").ToList();
            if (existFarm == null)
            {
                return BaseResponse<IEnumerable<Farm>>.FailureResponse(message: "Farm không tồn tại");
            }

            return BaseResponse<IEnumerable<Farm>>.SuccessResponse(data: existFarm);
        }
    }
}
