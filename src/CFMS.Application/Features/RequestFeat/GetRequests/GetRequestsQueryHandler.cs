using AutoMapper;
using CFMS.Application.Common;
using CFMS.Application.Features.ShiftFeat.GetShifts;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
            var existRequest = _unitOfWork.RequestRepository.GetIncludeMultiLayer(filter: f => f.IsDeleted == false,
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
                .Include(r => r.InventoryRequests)
                    .ThenInclude(r => r.InventoryReceipts)
                        .ThenInclude(r => r.InventoryReceiptDetails),
                orderBy: q => q.OrderByDescending(x => x.CreatedWhen)
                ).ToList();

            if (existRequest == null)
            {
                return BaseResponse<IEnumerable<Request>>.SuccessResponse(message: "Phiếu yêu cầu không tồn tại");
            }
            return BaseResponse<IEnumerable<Request>>.SuccessResponse(_mapper.Map<IEnumerable<Request>>(existRequest));
        }
    }
}
