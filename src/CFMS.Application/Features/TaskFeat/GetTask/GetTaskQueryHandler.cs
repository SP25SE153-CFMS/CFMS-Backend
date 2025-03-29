using CFMS.Application.Common;
using CFMS.Application.Features.FoodFeat.GetFood;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.TaskFeat.GetTask
{
    public class GetTaskQueryHandler : IRequestHandler<GetTaskQuery, BaseResponse<Domain.Entities.Task>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTaskQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<Domain.Entities.Task>> Handle(GetTaskQuery request, CancellationToken cancellationToken)
        {
            var existTask = _unitOfWork.TaskRepository.Get(filter: f => f.TaskId.Equals(request.Id) && f.IsDeleted == false).FirstOrDefault();
            if (existTask == null)
            {
                return BaseResponse<Domain.Entities.Task>.FailureResponse(message: "Công việc không tồn tại");
            }

            return BaseResponse<Domain.Entities.Task>.SuccessResponse(data: existTask);
        }
    }
}
