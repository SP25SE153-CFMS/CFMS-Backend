using CFMS.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.SystemConfigFeat.Create
{
    public class CreateConfigCommand : IRequest<BaseResponse<bool>>
    {
        public CreateConfigCommand(string? settingName, decimal? settingValue, string? description, DateTime? effectedDateFrom, DateTime? effectedDateTo, string? entityType, Guid? entityId, int? status)
        {
            SettingName = settingName;
            SettingValue = settingValue;
            Description = description;
            EffectedDateFrom = effectedDateFrom;
            EffectedDateTo = effectedDateTo;
            EntityType = entityType;
            EntityId = entityId;
            Status = status;
        }

        public string? SettingName { get; set; }

        public decimal? SettingValue { get; set; }

        public string? Description { get; set; }

        public DateTime? EffectedDateFrom { get; set; }

        public DateTime? EffectedDateTo { get; set; }

        public string? EntityType { get; set; }

        public Guid? EntityId { get; set; }

        public int? Status { get; set; }
    }
}
