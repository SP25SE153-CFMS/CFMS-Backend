using CFMS.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.HarvestProductFeat.Update
{
    public class UpdateHarvestProductCommand : IRequest<BaseResponse<bool>>
    {
        public UpdateHarvestProductCommand(Guid harvestProductId, string? harvestProductCode, string? harvestProductName, Guid? harvestProductTypeId)
        {
            HarvestProductId = harvestProductId;
            HarvestProductCode = harvestProductCode;
            HarvestProductName = harvestProductName;
            HarvestProductTypeId = harvestProductTypeId;
        }

        public Guid HarvestProductId { get; set; }

        public string? HarvestProductCode { get; set; }

        public string? HarvestProductName { get; set; }

        public Guid? HarvestProductTypeId { get; set; }
    }
}
