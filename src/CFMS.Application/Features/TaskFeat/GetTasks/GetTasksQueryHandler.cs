using AutoMapper;
using CFMS.Application.Common;
using CFMS.Application.Features.FoodFeat.GetFoods;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var foods = _unitOfWork.FoodRepository.Get(filter: f => f.IsDeleted == false);
            return BaseResponse<IEnumerable<Domain.Entities.Task>>.SuccessResponse(_mapper.Map<IEnumerable<Domain.Entities.Task>>(foods));
        }
    }
}
