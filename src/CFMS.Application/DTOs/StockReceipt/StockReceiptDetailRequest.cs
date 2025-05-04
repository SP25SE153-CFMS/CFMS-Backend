namespace CFMS.Application.DTOs.StockReceipt
{
    public class StockReceiptDetailRequest
    {
        public decimal Quantity { get; set; }

        public Guid UnitId { get; set; }

        public Guid ToWareId { get; set; }

        public Guid ResourceId { get; set; }

        public Guid ResourceSupplierId { get; set; }
    }
}
