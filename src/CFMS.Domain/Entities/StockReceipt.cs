namespace CFMS.Domain.Entities
{
    public class StockReceipt : EntityAudit
    {
        public Guid StockReceiptId { get; set; }

        public string? StockReceiptCode { get; set; }

        public Guid? ReceiptTypeId { get; set; }

        public Guid? FarmId { get; set; }

        public virtual SubCategory? ReceiptType { get; set; }

        public virtual ICollection<StockReceiptDetail> StockReceiptDetails { get; set; } = new List<StockReceiptDetail>();
    }
}
