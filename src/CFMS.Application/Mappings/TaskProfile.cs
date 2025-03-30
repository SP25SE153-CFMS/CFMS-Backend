using AutoMapper;
using CFMS.Application.Features.TaskFeat.Create;
using Task = CFMS.Domain.Entities.Task;

namespace CFMS.Application.Mappings
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<CreateTaskCommand, Task>();
        }
    }
}
