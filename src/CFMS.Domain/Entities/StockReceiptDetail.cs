using System.Text.Json.Serialization;

namespace CFMS.Domain.Entities
{
    public class StockReceiptDetail
    {
        public Guid StockReceiptDetailId { get; set; }

        public Guid StockReceiptId { get; set; }

        public decimal Quantity { get; set; }

        public Guid? UnitId { get; set; }

        public Guid ToWareId { get; set; }

        public Guid? ResourceId { get; set; }

        public Guid? ResourceSupplierId { get; set; }

        public virtual SubCategory? Unit { get; set; }

        public virtual Warehouse? Warehouse { get; set; }

        public virtual Resource? Resource { get; set; }

        public virtual ResourceSupplier? ResourceSupplier { get; set; }

        [JsonIgnore]
        public virtual StockReceipt? StockReceipt { get; set; }
    }
}
