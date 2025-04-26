using System.Text.Json.Serialization;

namespace CFMS.Domain.Entities
{
    public class QuantityLogDetail
    {
        public Guid QuantityLogDetailId { get; set; }

        public Guid QuantityLogId { get; set; }

        public int? Quantity { get; set; }

        public int? Gender { get; set; }

        [JsonIgnore]
        public virtual QuantityLog? QuantityLog { get; set; }
    }
}
