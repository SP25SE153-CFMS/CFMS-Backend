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
            var existCoop = _unitOfWork.ChickenCoopRepository.Get(filter: c => c.ChickenCoopId.Equals(request.Id) && c.IsDeleted == false, includeProperties: [c => c.ChickenBatches, c => c.CoopEquipments, c => c.DensityUnit, c => c.TaskLogs, c => c.AreaUnit]).FirstOrDefault();
            if (existCoop == null)
            {
                return BaseResponse<ChickenCoop>.FailureResponse(message: "Chuồng gà không tồn tại");
            }

            existCoop.ChickenBatches.OrderBy(cb => cb.Status);
            return BaseResponse<ChickenCoop>.SuccessResponse(data: existCoop);
        }
    }
}
