using AutoMapper;
using CFMS.Application.Common;
using CFMS.Application.DTOs.FarmEmployee;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.FarmFeat.GetFarmEmployees
{
    public class GetFarmEmployeesQueryHandler : IRequestHandler<GetFarmEmployeesQuery, BaseResponse<IEnumerable<FarmEmployeeResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetFarmEmployeesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<IEnumerable<FarmEmployeeResponse>>> Handle(GetFarmEmployeesQuery request, CancellationToken cancellationToken)
        {
            var employees = _unitOfWork.FarmEmployeeRepository.Get(filter: e => e.FarmId.Equals(request.FarmId) && e.IsDeleted == false, includeProperties: [e => e.User]);
            return BaseResponse<IEnumerable<FarmEmployeeResponse>>.SuccessResponse(data: _mapper.Map<IEnumerable<FarmEmployeeResponse>>(employees));
        }
    }
}
