using CFMS.Application.Common;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.ChickenFeat.GetChicken
{
    public class GetChickenQueryHandler : IRequestHandler<GetChickenQuery, BaseResponse<Chicken>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetChickenQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<Chicken>> Handle(GetChickenQuery request, CancellationToken cancellationToken)
        {
            var existChicken = _unitOfWork.ChickenRepository.Get(filter: c => c.ChickenId.Equals(request.Id) && c.IsDeleted == false).FirstOrDefault();
            if (existChicken == null)
            {
                return BaseResponse<Chicken>.FailureResponse(message: "Gà không tồn tại");
            }
            return BaseResponse<Chicken>.SuccessResponse(data: existChicken);
        }
    }
}
