using CFMS.Application.Common;
using CFMS.Application.DTOs.HarvestProduct;
using CFMS.Application.DTOs.TaskResource;
using CFMS.Domain.Entities;
using MediatR;

namespace CFMS.Application.Features.TaskFeat.CompleteTask
{
    public class CompleteTaskCommand : IRequest<BaseResponse<bool>>
    {
        public CompleteTaskCommand(Guid taskId, string note, IEnumerable<TaskResourceRequest>? taskResources)
        {
            TaskId = taskId;
            Note = note;
            TaskResources = taskResources;
        }

        public Guid TaskId { get; set; }

        public string? Reason { get; set; }

        public string? Note { get; set; }

        public IEnumerable<TaskResourceRequest>? TaskResources { get; set; }

        public IEnumerable<HarvestProductRequest>? HarvestProducts { get; set; }
    }
}
