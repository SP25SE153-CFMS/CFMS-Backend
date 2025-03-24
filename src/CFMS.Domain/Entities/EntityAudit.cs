using CFMS.Domain.Interfaces;

namespace CFMS.Domain.Entities
{
    public abstract class EntityAudit : ITrackable, ISoftDelete
    {
        public bool IsDeleted { get; set; }

        public DateTime? DeletedWhen { get; set; }

        public Guid CreatedByUserId { get; set; }

        public User CreatedByUser { get; set; }

        public DateTime CreatedWhen { get; set; }

        public Guid LastEditedByUserId { get; set; }

        public User LastEditedByUser { get; set; }

        public DateTime LastEditedWhen { get; set; }
    }
}
