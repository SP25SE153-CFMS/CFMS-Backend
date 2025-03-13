using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Domain.Dictionaries
{
    public class CategoryDictionary
    {
        public static readonly Dictionary<int, string> CategoryType = new()
        {
            { 3, "Vắc-xin" },
            { 2, "Thức ăn" },
            { 1, "Trang thiết bị" }
        };       
        
        public static readonly Dictionary<int, string> CategoryStatus = new()
        {
            { 2, "Có sẵn" },
            { 1, "Ẩn" },
            { 0, "Hết hàng" }
        };
    }
}
