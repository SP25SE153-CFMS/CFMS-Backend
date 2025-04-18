using CFMS.Application.Common;
using CFMS.Application.Features.ShiftFeat.GetShift;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.RequestFeat.GetRequest
{
    public class GetRequestQueryHandler : IRequestHandler<GetRequestQuery, BaseResponse<Request>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetRequestQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<Request>> Handle(GetRequestQuery request, CancellationToken cancellationToken)
        {
            var existRequest = _unitOfWork.RequestRepository.GetIncludeMultiLayer(filter: f => f.RequestId.Equals(request.Id) && f.IsDeleted == false,
            include: x => x
                .Include(r => r.InventoryRequests)
                    .ThenInclude(r => r.InventoryRequestDetails)
                .Include(r => r.InventoryRequests)
                    .ThenInclude(r => r.WareFrom)
                        .ThenInclude(r => r.Farm)
                .Include(r => r.InventoryRequests)
                    .ThenInclude(r => r.WareTo)
                        .ThenInclude(r => r.Farm)
                .Include(r => r.TaskRequests)
                ).FirstOrDefault();
            if (existRequest == null)
            {
                return BaseResponse<Request>.FailureResponse(message: "Phiếu yêu cầu không tồn tại");
            }

            return BaseResponse<Request>.SuccessResponse(data: existRequest);
        }
    }
}
