using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Tokens.Queries
{
    public record GenerateTokenResult(
        Guid Id,
        string FirstName,
        string LastName,
        string Email,
        string Token);
}
