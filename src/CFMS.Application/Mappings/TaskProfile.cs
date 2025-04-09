using AutoMapper;
using CFMS.Application.DTOs.Task.ShiftSchedule;
using CFMS.Application.DTOs.Task.TaskResource;
using CFMS.Application.DTOs.Task;
using CFMS.Application.Features.TaskFeat.Create;
using CFMS.Domain.Entities;
using Task = CFMS.Domain.Entities.Task;
using CFMS.Application.DTOs.Task.Assignment;

namespace CFMS.Application.Mappings
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<CreateTaskCommand, Task>()
                .ForMember(dest => dest.StartWorkDate, opt => opt.Ignore())
                .ForMember(dest => dest.TaskResources, opt => opt.Ignore());

            CreateMap<Task, TaskDto>()
                .ForMember(dest => dest.TaskName, opt => opt.MapFrom(src => src.TaskName))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.TaskType, opt => opt.MapFrom(src => src.TaskType.SubCategoryName))
                .AfterMap((src, dest) =>
                {
                    var location = src.TaskLocations.FirstOrDefault();
                    if (location?.LocationType?.Equals("COOP", StringComparison.OrdinalIgnoreCase) is true)
                        dest.TaskLocation = location.Location?.ChickenCoopName ?? "Không xác định";
                    else if (location?.LocationType?.Equals("WARE", StringComparison.OrdinalIgnoreCase) is true)
                        dest.TaskLocation = location.LocationNavigation?.WarehouseName ?? "Không xác định";
                    else
                        dest.TaskLocation = "Không xác định";
                })
                .ForMember(dest => dest.shiftScheduleList, opt => opt.MapFrom(src => src.ShiftSchedules))
                .ForMember(dest => dest.resourceList, opt => opt.MapFrom(src => src.TaskResources))
                .ForMember(dest => dest.assignmentList, opt => opt.MapFrom(src => src.Assignments));

            CreateMap<ShiftSchedule, ShiftScheduleDto>()
                .AfterMap((src, dest) =>
                {
                    dest.ShiftName = src.Shift?.ShiftName ?? "Không xác định";
                    dest.WorkTime = src.Date;
                    dest.StartTime = src.Shift?.StartTime;
                    dest.EndTime = src.Shift?.EndTime;
                });

            CreateMap<TaskResource, TaskResourceDto>()
                .AfterMap((src, dest) =>
                {
                    dest.ResourceName =
                        src.Resource?.Food?.FoodName ??
                        src.Resource?.Medicine?.MedicineName ??
                        src.Resource?.Equipment?.EquipmentName ??
                        "Không xác định";

                    var subType = src.ResourceType?.SubCategoryName?.ToLower();
                    var package = src.Resource?.Package?.SubCategoryName;
                    var unit = src.Resource?.Unit?.SubCategoryName;
                    var packageSize = src.Resource?.PackageSize;

                    dest.ResourceType = subType switch
                    {
                        "food" => "Thực phẩm",
                        "equipment" => "Thiết bị",
                        "medicine" => "Dược phẩm",
                        _ => "Không xác định"
                    };
                    dest.SpecQuantity = $"{src.Quantity} {src.Unit?.SubCategoryName}";
                    dest.UnitSpecification = $"{packageSize} {unit}/{package}";
                });

            CreateMap<Assignment, AssignmentDto>()
                .AfterMap((src, dest) =>
                {
                    dest.AssignedTo = src.AssignedTo?.FullName ?? "Không xác định";
                });
        }
    }
}
