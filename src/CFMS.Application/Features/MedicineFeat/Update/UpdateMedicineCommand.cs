using CFMS.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.MedicineFeat.Update
{
    public class UpdateMedicineCommand : IRequest<BaseResponse<bool>>
    {
        public UpdateMedicineCommand(Guid medicineId, string? medicineCode, string? medicineName, string? usage, string? dosageForm, string? storageCondition, Guid? diseaseId, DateTime? productionDate, DateTime? expiryDate)
        {
            MedicineId = medicineId;
            MedicineCode = medicineCode;
            MedicineName = medicineName;
            Usage = usage;
            DosageForm = dosageForm;
            StorageCondition = storageCondition;
            DiseaseId = diseaseId;
            ProductionDate = productionDate;
            ExpiryDate = expiryDate;
        }

        public Guid MedicineId { get; set; }

        public string? MedicineCode { get; set; }

        public string? MedicineName { get; set; }

        public string? Usage { get; set; }

        public string? DosageForm { get; set; }

        public string? StorageCondition { get; set; }

        public Guid? DiseaseId { get; set; }

        public DateTime? ProductionDate { get; set; }

        public DateTime? ExpiryDate { get; set; }
    }
}
