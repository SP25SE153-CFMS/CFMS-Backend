using AutoMapper;
using CFMS.Application.DTOs.Task.ShiftSchedule;
using CFMS.Application.DTOs.Task.TaskResource;
using CFMS.Application.DTOs.Task;
using CFMS.Application.Features.TaskFeat.Create;
using CFMS.Domain.Entities;
using Task = CFMS.Domain.Entities.Task;
using CFMS.Application.DTOs.Task.Assignment;
using CFMS.Application.DTOs.Task.TaskLocation;

namespace CFMS.Application.Mappings
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<Task, TaskResponse>()
            .ForMember(dest => dest.ShiftSchedule, opt => opt.MapFrom(src =>
                src.ShiftSchedules.FirstOrDefault()))
            .ForMember(dest => dest.TaskLocation, opt => opt.MapFrom(src =>
                src.TaskLocations.FirstOrDefault()))
            .ForMember(dest => dest.Assignments, opt => opt.MapFrom(src =>
                src.Assignments))
            .ForMember(dest => dest.TaskResources, opt => opt.MapFrom(src =>
                src.TaskResources));

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

            CreateMap<TaskLocation, TaskLocationDto>()
                .ForMember(dest => dest.TaskLocationId, opt => opt.MapFrom(src => src.TaskLocationId))
                .ForMember(dest => dest.TaskId, opt => opt.MapFrom(src => src.TaskId))
                .ForMember(dest => dest.LocationType, opt => opt.MapFrom(src => src.LocationType))
                .ForMember(dest => dest.CoopId, opt => opt.MapFrom(src => src.CoopId))
                .ForMember(dest => dest.WareId, opt => opt.MapFrom(src => src.WareId));
            // Bỏ qua navigation properties để tránh lỗi và không cần trong DTO
            //.ForMember(dest => dest.Location, opt => opt.Ignore())
            //.ForMember(dest => dest.LocationNavigation, opt => opt.Ignore());

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
