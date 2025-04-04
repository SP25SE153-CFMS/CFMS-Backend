using AutoMapper;
using CFMS.Application.Common;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.AssignmentFeat.AssignEmployee
{
    public class AssignEmployeeCommandHandler : IRequestHandler<AssignEmployeeCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public AssignEmployeeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<bool>> Handle(AssignEmployeeCommand request, CancellationToken cancellationToken)
        {
            var task = _unitOfWork.TaskRepository.Get(filter: t => t.TaskId.Equals(request.TaskId) && t.IsDeleted == false, includeProperties: [t => t.FrequencySchedules]).FirstOrDefault();
            if (task == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Task không tồn tại");
            }

            try
            {
                foreach (var assignedToId in request.AssignedToIds)
                {
                    var assignment = new Assignment
                    {
                        TaskId = request.TaskId,
                        AssignedDate = DateTime.Now.ToLocalTime(),
                        AssignedToId = assignedToId,
                        Note = request.Note,
                        Status = request.Status,
                    };

                    _unitOfWork.AssignmentRepository.Insert(assignment);
                }

                var result = await _unitOfWork.SaveChangesAsync();
                if (result > 0)
                {
                    return BaseResponse<bool>.SuccessResponse(message: "Tạo thành công");
                }
                return BaseResponse<bool>.FailureResponse(message: "Tạo không thành công");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse(message: "Có lỗi xảy ra");
            }
        }
    }
}
