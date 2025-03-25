using AutoMapper;
using CFMS.Application.Common;
using CFMS.Application.DTOs.Auth;
using CFMS.Application.Features.UserFeat.GetUsers;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.SupplierFeat.GetSuppliers
{
    public class GetSuppliersQueryHandler : IRequestHandler<GetSuppliersQuery, BaseResponse<IEnumerable<Supplier>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetSuppliersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<IEnumerable<Supplier>>> Handle(GetSuppliersQuery request, CancellationToken cancellationToken)
        {
            var suppliers = _unitOfWork.SupplierRepository.Get(filter: f => f.IsDeleted == false);
            return BaseResponse<IEnumerable<Supplier>>.SuccessResponse(_mapper.Map<IEnumerable<Supplier>>(suppliers));
        }
    }
}
