using AutoMapper;
using CFMS.Application.DTOs.Task.ShiftSchedule;
using CFMS.Application.DTOs.Task.TaskResource;
using CFMS.Application.DTOs.Task;
using CFMS.Application.Features.TaskFeat.Create;
using CFMS.Domain.Entities;
using Task = CFMS.Domain.Entities.Task;

namespace CFMS.Application.Mappings
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<CreateTaskCommand, Task>();

            CreateMap<Task, TaskDto>()
                .ForMember(dest => dest.TaskName, opt => opt.MapFrom(src => src.TaskName))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.TaskType, opt => opt.MapFrom(src => src.TaskType.SubCategoryName))
                .ForMember(dest => dest.TaskLocation, opt => opt.MapFrom(src => src.TaskLocations.FirstOrDefault().LocationType))
                .ForMember(dest => dest.shiftScheduleList, opt => opt.MapFrom(src => src.ShiftSchedules))
                .ForMember(dest => dest.resourceList, opt => opt.MapFrom(src => src.TaskResources));

            CreateMap<ShiftSchedule, ShiftScheduleDto>()
                .ForMember(dest => dest.ShiftName, opt => opt.MapFrom(src => src.Shift.ShiftName))
                .ForMember(dest => dest.WorkTime, opt => opt.MapFrom(src => src.Date));

            CreateMap<TaskResource, TaskResourceDto>()
                .AfterMap((src, dest) =>
                {
                    dest.ResourceName =
                        src.Resource?.Food?.FoodName ??
                        src.Resource?.Medicine?.MedicineName ??
                        src.Resource?.Equipment?.EquipmentName ??
                        "Không xác định";

                    var subType = src.ResourceType?.SubCategoryName?.ToLower();

                    dest.ResourceType = subType switch
                    {
                        "food" => "Thực phẩm",
                        "equipment" => "Thiết bị",
                        "medicine" => "Dược phẩm",
                        _ => "Không xác định"
                    };
                });

        }
    }
}
