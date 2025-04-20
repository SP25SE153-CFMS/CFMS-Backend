namespace CFMS.Application.DTOs.Assignment
{
    public class AssignmentRequest
    {
        public Guid AssignedToId { get; set; }

        public int Status { get; set; }
    }
}
