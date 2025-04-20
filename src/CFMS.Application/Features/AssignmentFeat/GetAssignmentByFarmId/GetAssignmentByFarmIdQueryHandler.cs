using CFMS.Application.Common;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.AssignmentFeat.GetAssignmentByFarmId
{
    public class GetAssignmentByFarmIdQueryHandler : IRequestHandler<GetAssignmentByFarmIdQuery, BaseResponse<IEnumerable<Assignment>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAssignmentByFarmIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<IEnumerable<Assignment>>> Handle(GetAssignmentByFarmIdQuery request, CancellationToken cancellationToken)
        {
            var assignments = _unitOfWork.AssignmentRepository.Get(filter: a => a.IsDeleted == false && a.Task.FarmId.Equals(request.FarmId), includeProperties: "Task");
            return BaseResponse<IEnumerable<Assignment>>.SuccessResponse(data: assignments);
        }
    }
}
