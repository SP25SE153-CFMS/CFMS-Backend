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

        public async Task<BaseResponse<IEnumerable<Assignment>>> Handle(GetAssignmentsQuery request, CancellationToken cancellationToken)
        {
            var assignments = _unitOfWork.AssignmentRepository.Get(filter: a => a.IsDeleted == false);
            return BaseResponse<IEnumerable<Assignment>>.SuccessResponse(data: assignments);
        }
    }
}
