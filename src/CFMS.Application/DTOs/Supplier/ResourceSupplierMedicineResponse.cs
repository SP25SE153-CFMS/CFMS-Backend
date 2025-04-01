using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.DTOs.Supplier
{
    public class ResourceSupplierMedicineResponse : ResourceSupplierResponseBase
    {
        public string? MedicineCode { get; set; }

        public string? MedicineName { get; set; }

        public string? Usage { get; set; }

        public string? DosageForm { get; set; }

        public string? StorageCondition { get; set; }

        public string? Disease { get; set; }

        public DateTime? ProductionDate { get; set; }

        public DateTime? ExpiryDate { get; set; }
    }
}
