using CFMS.Domain.Enums.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Domain.Interfaces
{
    public interface IUtilityService
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string hashedPassword);
        //string GenerateCode(EntityType type);
        DateTime? ToVietnamTime(DateTime utcDateTime);
    }
}
