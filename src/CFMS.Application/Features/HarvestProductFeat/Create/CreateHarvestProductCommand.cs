using CFMS.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.HarvestProductFeat.Create
{
    public class CreateHarvestProductCommand : IRequest<BaseResponse<bool>>
    {
        public CreateHarvestProductCommand(Guid harvestProductTypeId, string? harvestProductName, string? harvestProductCode, Guid? unitId, Guid? packageId, decimal? packageSize, Guid wareId)
        {
            HarvestProductTypeId = harvestProductTypeId;
            HarvestProductName = harvestProductName;
            HarvestProductCode = harvestProductCode;
            UnitId = unitId;
            PackageId = packageId;
            PackageSize = packageSize;
            WareId = wareId;
        }
        public string? HarvestProductCode { get; set; }

        public string? HarvestProductName { get; set; }

        public Guid? HarvestProductTypeId { get; set; }

        public Guid? UnitId { get; set; }

        public Guid? PackageId { get; set; }

        public decimal? PackageSize { get; set; }

        public Guid WareId { get; set; }
    }
}
