using AutoMapper;
using CFMS.Application.Common;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.TaskFeat.GetTasks
{
    public class GetTasksQueryHandler : IRequestHandler<GetTasksQuery, BaseResponse<IEnumerable<Domain.Entities.Task>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetTasksQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<IEnumerable<Domain.Entities.Task>>> Handle(GetTasksQuery request, CancellationToken cancellationToken)
        {
            var tasks = _unitOfWork.TaskRepository.Get(filter: t => t.IsDeleted == false);
            return BaseResponse<IEnumerable<Domain.Entities.Task>>.SuccessResponse(data: tasks);
        }
    }
}
