using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Common.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(
            Guid id,
            string firstName,
            string lastName,
            string email,
            List<string> roles);
    }
}
