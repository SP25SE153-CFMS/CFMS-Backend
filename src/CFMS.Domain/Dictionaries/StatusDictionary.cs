using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Domain.Dictionaries
{
    public static class StatusDictionary
    {
        public static readonly Dictionary<int, string> UserStatus = new()
    {
        { 2, "Đang hoạt động" },
        { 1, "Không hoạt động" },
        { 0, "Đã sa thải" }
    };

        public static readonly Dictionary<int, string> FarmStatus = new()
    {
        { 2, "Đang hoạt động" },
        { 1, "Bảo trì" },
        { 0, "Đã đóng cửa" }
    };
    }
}
