using AutoMapper;
using CFMS.Application.Common;
using CFMS.Application.DTOs.FarmEmployee;
using CFMS.Application.Features.FarmFeat.GetFarm;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.FarmFeat.GetFarmEmployee
{
    public class GetFarmEmployeeQueryHandler : IRequestHandler<GetFarmEmployeeQuery, BaseResponse<FarmEmployeeResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetFarmEmployeeQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<FarmEmployeeResponse>> Handle(GetFarmEmployeeQuery request, CancellationToken cancellationToken)
        {
            var existFarmEmployee = _unitOfWork.FarmEmployeeRepository.Get(filter: f => f.FarmEmployeeId.Equals(request.Id) && f.IsDeleted == false, includeProperties: [e => e.User]).FirstOrDefault();
            if (existFarmEmployee == null)
            {
                return BaseResponse<FarmEmployeeResponse>.SuccessResponse(message: "Nhân viên không làm việc trong trang trại này");
            }

            var employee = _mapper.Map<FarmEmployeeResponse>(existFarmEmployee);
            employee.User.Mail = existFarmEmployee.Mail;
            employee.User.PhoneNumber = existFarmEmployee.PhoneNumber;

            return BaseResponse<FarmEmployeeResponse>.SuccessResponse(data: employee);
        }
    }
}
