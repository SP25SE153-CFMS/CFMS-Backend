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

            var mappedEmployees = _mapper.Map<List<FarmEmployeeResponse>>(employees)
                    .Select((dto, index) =>
                    {
                        var entity = employees.ElementAt(index);
                        if (dto.User != null && entity.User != null)
                        {
                            dto.User.Mail = entity.Mail;
                            dto.User.PhoneNumber = entity.PhoneNumber;
                        }
                        return dto;
                    }).ToList();

            return BaseResponse<IEnumerable<FarmEmployeeResponse>>.SuccessResponse(data: mappedEmployees);
        }
    }
}
