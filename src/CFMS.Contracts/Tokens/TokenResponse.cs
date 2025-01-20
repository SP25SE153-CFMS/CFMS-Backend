using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Contracts.Tokens
{
    public record TokenResponse(
        Guid Id,
        string FirstName,
        string LastName,
        string Email,
        string Token);
}
