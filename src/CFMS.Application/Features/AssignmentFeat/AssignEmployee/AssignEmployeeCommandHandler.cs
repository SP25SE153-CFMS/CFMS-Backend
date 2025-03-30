using AutoMapper;
using CFMS.Application.Common;
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

            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse(message: "Có lỗi xảy ra");
            }
        }
    }
}
