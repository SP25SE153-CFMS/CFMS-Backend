using AutoMapper;
using CFMS.Application.Common;
using CFMS.Application.DTOs.FarmEmployee;
using CFMS.Application.Features.FarmFeat.GetFarmEmployee;
using CFMS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.FarmFeat.GetFarmEmployeeByUserId
{
    public class GetFarmEmployeeByUserIdQueryHandler : IRequestHandler<GetFarmEmployeeByUserIdQuery, BaseResponse<FarmEmployeeResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetFarmEmployeeByUserIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<FarmEmployeeResponse>> Handle(GetFarmEmployeeByUserIdQuery request, CancellationToken cancellationToken)
        {
            var existFarmEmployee = _unitOfWork.FarmEmployeeRepository.Get(filter: f => f.UserId.Equals(request.UserId) && f.FarmId.Equals(request.FarmId) && f.IsDeleted == false, includeProperties: [e => e.User]).FirstOrDefault();
            if (existFarmEmployee == null)
            {
                return BaseResponse<FarmEmployeeResponse>.FailureResponse(message: "Nhân viên không làm việc trong trang trại này");
            }

            var employee = _mapper.Map<FarmEmployeeResponse>(existFarmEmployee);
            employee.User.Mail = existFarmEmployee.Mail;
            employee.User.PhoneNumber = existFarmEmployee.PhoneNumber;

            return BaseResponse<FarmEmployeeResponse>.SuccessResponse(data: employee);
        }
    }
}
