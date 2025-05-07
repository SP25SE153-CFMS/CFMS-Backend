using AutoMapper;
using CFMS.Application.Common;
using CFMS.Application.Features.TaskFeat.CompleteTask;
using CFMS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.TaskFeat.CancelTask
{
    public class CancelTaskCommandHandler : IRequestHandler<CancelTaskCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CancelTaskCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<bool>> Handle(CancelTaskCommand request, CancellationToken cancellationToken)
        {
            var existTask = _unitOfWork.TaskRepository.Get(x => x.TaskId == request.TaskId && x.IsDeleted == false).FirstOrDefault();
            if (existTask == null)
            {
                return BaseResponse<bool>.FailureResponse("Công việc không tồn tại");
            }

            existTask.Status = 3;

            _unitOfWork.TaskRepository.Update(existTask);
            await _unitOfWork.SaveChangesAsync();
            return BaseResponse<bool>.SuccessResponse(true, "Hủy công việc thành công");
        }
    }
}
