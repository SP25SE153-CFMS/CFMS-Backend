using CFMS.Application.Common;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Commands.FarmFeat.Create
{
    public class CreateFarmCommandHandler : IRequestHandler<CreateFarmCommand, BaseResponse<string>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateFarmCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<BaseResponse<string>> Handle(CreateFarmCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
