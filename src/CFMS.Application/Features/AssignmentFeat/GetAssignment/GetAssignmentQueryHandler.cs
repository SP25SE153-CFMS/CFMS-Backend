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

        public async Task<BaseResponse<Assignment>> Handle(GetAssignmentQuery request, CancellationToken cancellationToken)
        {
            var existAssignment = _unitOfWork.AssignmentRepository.Get(filter: a => a.AssignmentId.Equals(request.AssignmentId) && a.IsDeleted == false, includeProperties: "AssignedTo").FirstOrDefault();
            if (existAssignment == null)
            {
                return BaseResponse<Assignment>.SuccessResponse(message: "Phiên giao việc không tồn tại");
            }
            return BaseResponse<Assignment>.SuccessResponse(data: existAssignment);
        }
    }
}
