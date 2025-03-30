using CFMS.Application.Common;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.AssignmentFeat.GetAssignments
{
    public class GetAssignmentsQueryHandler : IRequestHandler<GetAssignmentsQuery, BaseResponse<IEnumerable<Assignment>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAssignmentsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<BaseResponse<IEnumerable<Assignment>>> Handle(GetAssignmentsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
