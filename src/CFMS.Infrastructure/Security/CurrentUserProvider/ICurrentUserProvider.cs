using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Infrastructure.Security.CurrentUserProvider
{
    public interface ICurrentUserProvider
    {
        CurrentUser GetCurrentUser();
    }
}
