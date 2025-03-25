using AutoMapper;
using CFMS.Application.Common;
using CFMS.Application.Features.SupplierFeat.GetSuppliers;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.ResourceFeat.GetResources
{
    public class GetResourcesQueryHandler : IRequestHandler<GetResourcesQuery, BaseResponse<IEnumerable<Resource>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetResourcesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<IEnumerable<Resource>>> Handle(GetResourcesQuery request, CancellationToken cancellationToken)
        {
            var resources = _unitOfWork.ResourceRepository.Get(filter: f => f.IsDeleted == false, includeProperties: "Food,Equipment,Medicine");
            return BaseResponse<IEnumerable<Resource>>.SuccessResponse(_mapper.Map<IEnumerable<Resource>>(resources));
        }
    }
}
