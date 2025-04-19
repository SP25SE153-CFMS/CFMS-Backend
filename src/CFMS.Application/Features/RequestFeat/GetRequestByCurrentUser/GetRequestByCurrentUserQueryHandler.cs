using CFMS.Application.Common;
using CFMS.Application.Features.RequestFeat.GetRequest;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.RequestFeat.GetRequestByCurrentUser
{
    public class GetRequestByCurrentUserQueryHandler : IRequestHandler<GetRequestByCurrentUserQuery, BaseResponse<IEnumerable<Request>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;

        public GetRequestByCurrentUserQueryHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }

        public async Task<BaseResponse<IEnumerable<Request>>> Handle(GetRequestByCurrentUserQuery request, CancellationToken cancellationToken)
        {
            var currentUserId = _currentUserService.GetUserId();

            var existRequest = _unitOfWork.RequestRepository.GetIncludeMultiLayer(filter: f => f.CreatedByUser.UserId.ToString().Equals(currentUserId) && f.IsDeleted == false,
                include: x => x
                .Include(r => r.InventoryRequests)
                    .ThenInclude(r => r.InventoryRequestDetails)
                .Include(r => r.InventoryRequests)
                    .ThenInclude(r => r.WareFrom)
                        .ThenInclude(r => r.Farm)
                .Include(r => r.InventoryRequests)
                    .ThenInclude(r => r.WareTo)
                        .ThenInclude(r => r.Farm)
                .Include(r => r.TaskRequests), 
                orderBy: q => q.OrderByDescending(x => x.CreatedWhen)
                ).ToList();
            if (existRequest == null)
            {
                return BaseResponse<IEnumerable<Request>>.FailureResponse(message: "Phiếu yêu cầu không tồn tại");
            }

            return BaseResponse<IEnumerable<Request>>.SuccessResponse(data: existRequest);
        }
    }
}
