using CFMS.Application.Common;
using CFMS.Application.Features.FoodFeat.GetFood;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.SystemConfigFeat.GetConfig
{
    public class GetConfigQueryHandler : IRequestHandler<GetConfigQuery, BaseResponse<SystemConfig>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetConfigQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<SystemConfig>> Handle(GetConfigQuery request, CancellationToken cancellationToken)
        {
            var existSystemConfig = _unitOfWork.SystemConfigRepository.Get(filter: f => f.SystemConfigId.Equals(request.Id) && f.IsDeleted == false).FirstOrDefault();
            if (existSystemConfig == null)
            {
                return BaseResponse<SystemConfig>.FailureResponse(message: "Cấu hình hệ thống không tồn tại");
            }

            return BaseResponse<SystemConfig>.SuccessResponse(data: existSystemConfig);
        }
    }
}
