using CFMS.Application.Common;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.ChickenFeat.GetChickenByBatchId
{
    public class GetChickenByBatchIdQueryHandler : IRequestHandler<GetChickenByBatchIdQuery, BaseResponse<Chicken>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetChickenByBatchIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<Chicken>> Handle(GetChickenByBatchIdQuery request, CancellationToken cancellationToken)
        {
            var chickenBatch = _unitOfWork.ChickenBatchRepository.Get(filter: c => c.IsDeleted == false && c.ChickenBatchId.Equals(request.ChickenBatchId)).FirstOrDefault();
            if (chickenBatch == null)
                return BaseResponse<Chicken>.SuccessResponse(message: "Lứa nuôi không tồn tại");

            var chicken = _unitOfWork.ChickenRepository.Get(filter: c => c.IsDeleted == false && chickenBatch.ChickenId.Equals(c.ChickenId), includeProperties: [c => c.ChickenDetails]).FirstOrDefault();
            return BaseResponse<Chicken>.SuccessResponse(data: chicken);
        }
    }
}
