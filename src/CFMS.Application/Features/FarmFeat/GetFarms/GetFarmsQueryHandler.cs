using CFMS.Application.Common;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.FarmFeat.GetFarms
{
    public class GetFarmsQueryHandler : IRequestHandler<GetFarmsQuery, BaseResponse<IEnumerable<Farm>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetFarmsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<IEnumerable<Farm>>> Handle(GetFarmsQuery request, CancellationToken cancellationToken)
        {
            var farms = _unitOfWork.FarmRepository.Get(filter: f => f.IsDeleted == false);
            return BaseResponse<IEnumerable<Farm>>.SuccessResponse(data: farms);
        }
    }
}
