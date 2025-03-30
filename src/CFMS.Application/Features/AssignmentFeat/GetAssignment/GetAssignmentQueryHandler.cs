using CFMS.Application.Common;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.AssignmentFeat.GetAssignment
{
    public class GetAssignmentQueryHandler : IRequestHandler<GetAssignmentQuery, BaseResponse<Assignment>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAssignmentQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<BaseResponse<Assignment>> Handle(GetAssignmentQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
