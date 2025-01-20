using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Contracts.Tokens
{
    public record GenerateTokenRequest(
        Guid? Id,
        string FirstName,
        string LastName,
        string Email,
        List<string> Roles);
}
