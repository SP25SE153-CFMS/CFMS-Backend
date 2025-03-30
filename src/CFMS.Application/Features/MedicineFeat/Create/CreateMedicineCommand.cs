using CFMS.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.MedicineFeat.Create
{
    public class CreateMedicineCommand : IRequest<BaseResponse<bool>>
    {
        public CreateMedicineCommand(string? usage, string? dosageForm, string? storageCondition, Guid? diseaseId, DateTime? productionDate, DateTime? expiryDate, string? medicineCode, string? medicineName, Guid? unitId, Guid? packageId, decimal? packageSize, Guid wareId)
        {
            Usage = usage;
            DosageForm = dosageForm;
            StorageCondition = storageCondition;
            DiseaseId = diseaseId;
            ProductionDate = productionDate;
            ExpiryDate = expiryDate;
            MedicineCode = medicineCode;
            MedicineName = medicineName;
            UnitId = unitId;
            PackageId = packageId;
            PackageSize = packageSize;
            WareId = wareId;
        }

        public string? MedicineCode { get; set; }

        public string? MedicineName { get; set; }

        public string? Usage { get; set; }

        public string? DosageForm { get; set; }

        public string? StorageCondition { get; set; }

        public Guid? DiseaseId { get; set; }

        public DateTime? ProductionDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public Guid? UnitId { get; set; }

        public Guid? PackageId { get; set; }

        public decimal? PackageSize { get; set; }

        public Guid WareId { get; set; }
    }
}
