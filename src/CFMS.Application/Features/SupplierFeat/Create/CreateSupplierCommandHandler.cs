using CFMS.Application.Common;
using CFMS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.SupplierFeat.Create
{
    public class CreateSupplierCommandHandler : IRequestHandler<CreateSupplierCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateSupplierCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
        {
            throw new Exception();
            //var existCategory = _unitOfWork.CategoryRepository.Get(x => x.CategoryType.Equals());
        }
    }
}
