using CFMS.Domain.Entities;

namespace CFMS.Domain.Interfaces
{
    public interface ITrackable
    {
        public Guid CreatedByUserId { get; set; }
        public User CreatedByUser { get; set; }
        public DateTime CreatedWhen { get; set; }

        public Guid LastEditedByUserId { get; set; }
        public User LastEditedByUser { get; set; }
        public DateTime LastEditedWhen { get; set; }
    }
}
