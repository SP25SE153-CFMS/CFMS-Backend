using CFMS.Application.Common;
using CFMS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.ChickenCoopFeat.Create
{
    public class CreateCoopCommandHandler : IRequestHandler<CreateCoopCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateCoopCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<BaseResponse<bool>> Handle(CreateCoopCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
