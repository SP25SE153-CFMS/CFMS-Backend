using CFMS.Application.Common;
using CFMS.Application.Features.ChickenFeat.GetChickenByBatchId;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.ChickenFeat.GetChickens
{
    public class GetChickensQueryHandler : IRequestHandler<GetChickensQuery, BaseResponse<IEnumerable<Chicken>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetChickensQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<IEnumerable<Chicken>>> Handle(GetChickensQuery request, CancellationToken cancellationToken)
        {
            var chickens = _unitOfWork.ChickenRepository.Get(filter: c => c.IsDeleted == false, includeProperties: "ChickenBatches").ToList();
            return BaseResponse<IEnumerable<Chicken>>.SuccessResponse(data: chickens);
        }
    }
}
