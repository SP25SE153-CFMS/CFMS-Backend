using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Domain.Dictionaries
{
    public class RoleDictionary
    {
        public static readonly Dictionary<int, string> SystemRole = new()
        {
            { 2, "Chủ trang trại" },
            { 1, "Quản lý" },
            { 0, "Nhân viên" }
        };

        public static readonly Dictionary<int, string> FarmRole = new()
        {
            { 1, "Admin" },
            { 0, "User" }
        };
    }
}
