using CFMS.Application.Common;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CFMS.Application.Features.RequestFeat.GetRequestByFarmId
{
    public class GetRequestByFarmIdQueryHandler : IRequestHandler<GetRequestByFarmIdQuery, BaseResponse<IEnumerable<Request>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetRequestByFarmIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<IEnumerable<Request>>> Handle(GetRequestByFarmIdQuery request, CancellationToken cancellationToken)
        {
            var existFarm = _unitOfWork.FarmRepository.Get(filter: f => f.FarmId.Equals(request.FarmId) && !f.IsDeleted).FirstOrDefault();
            if (existFarm == null)
            {
                return BaseResponse<IEnumerable<Request>>.FailureResponse(message: "Trang trại không tồn tại");
            }

            var existRequest = _unitOfWork.RequestRepository.GetIncludeMultiLayer(filter: f => f.FarmId.Equals(request.FarmId) && f.IsDeleted == false,
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
            return BaseResponse<IEnumerable<Request>>.SuccessResponse(data: existRequest);
        }
    }
}
