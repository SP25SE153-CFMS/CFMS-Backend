namespace CFMS.Domain.Interfaces
{
    public interface ICurrentUserService
    {
        string? GetUserId();
        string? GetUserRole();
        string? GetUserEmail();
    }
}
