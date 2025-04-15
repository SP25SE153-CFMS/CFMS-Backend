namespace CFMS.Domain.Entities
{
    public class HarvestProduct : EntityAudit
    {
        public Guid HarvestProductId { get; set; }

        public string HarvestProductName { get; set; }

        public Guid HarvestProductTypeId { get; set; }

        public virtual SubCategory? HarvestProductType { get; set; }
    }
}
