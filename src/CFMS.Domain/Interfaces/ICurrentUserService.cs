using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Domain.Interfaces
{
    public interface ICurrentUserService
    {
        string? GetUserId();
        string? GetUserRole();
        string? GetUserEmail();
        bool? IsOwner();
        bool? IsManager();
        bool? IsUser();
    }
}
