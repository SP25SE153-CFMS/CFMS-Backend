using CFMS.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.SystemConfigFeat.Update
{
    public class UpdateConfigCommand : IRequest<BaseResponse<bool>>
    {
        public UpdateConfigCommand(Guid systemConfigId, string? settingName, decimal? settingValue, string? description, DateTime? effectedDateFrom, DateTime? effectedDateTo, string? entityType, Guid? entityId, int? status)
        {
            SystemConfigId = systemConfigId;
            SettingName = settingName;
            SettingValue = settingValue;
            Description = description;
            EffectedDateFrom = effectedDateFrom;
            EffectedDateTo = effectedDateTo;
            EntityType = entityType;
            EntityId = entityId;
            Status = status;
        }

        public Guid SystemConfigId { get; set; }

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
