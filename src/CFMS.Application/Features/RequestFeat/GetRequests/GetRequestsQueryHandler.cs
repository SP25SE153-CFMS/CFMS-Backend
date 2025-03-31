using AutoMapper;
using CFMS.Application.Common;
using CFMS.Application.Features.ShiftFeat.GetShifts;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.RequestFeat.GetRequests
{
    public class GetRequestsQueryHandler : IRequestHandler<GetRequestsQuery, BaseResponse<IEnumerable<Request>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetRequestsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<IEnumerable<Request>>> Handle(GetRequestsQuery request, CancellationToken cancellationToken)
        {
            var reqs = _unitOfWork.RequestRepository.Get(filter: f => f.IsDeleted == false);
            return BaseResponse<IEnumerable<Request>>.SuccessResponse(_mapper.Map<IEnumerable<Request>>(reqs));
        }
    }
}
