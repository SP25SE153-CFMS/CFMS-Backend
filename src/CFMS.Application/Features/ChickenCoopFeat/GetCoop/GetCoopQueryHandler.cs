using CFMS.Application.Common;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.ChickenCoopFeat.GetCoop
{
    public class GetCoopQueryHandler : IRequestHandler<GetCoopQuery, BaseResponse<ChickenCoop>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCoopQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<ChickenCoop>> Handle(GetCoopQuery request, CancellationToken cancellationToken)
        {
            var existCoop = _unitOfWork.ChickenCoopRepository.Get(filter: c => c.ChickenCoopId.Equals(request.Id) && c.IsDeleted == false, includeProperties: "ChickenBatches,CoopEquipments,DensityUnit,TaskLogs,TaskLogs.Task.TaskHarvests,AreaUnit").FirstOrDefault();
            if (existCoop == null)
            {
                return BaseResponse<ChickenCoop>.SuccessResponse(message: "Chuồng gà không tồn tại");
            }

            existCoop.ChickenBatches = existCoop.ChickenBatches.OrderBy(cb => cb.Status).ToList();
            return BaseResponse<ChickenCoop>.SuccessResponse(data: existCoop);
        }
    }
}
