using CFMS.Application.Common;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.ChickenBatchFeat.AddVaccineLog
{
    public class AddVaccineLogCommandHandler : IRequestHandler<AddVaccineLogCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddVaccineLogCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<BaseResponse<bool>> Handle(AddVaccineLogCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
