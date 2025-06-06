﻿using System;
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
            { 3, "Chủ trang trại" },
            { 2, "Quản lý" },
            { 1, "Nhân viên" }
        };

        public static readonly Dictionary<int, string> FarmRole = new()
        {
            { 2, "Admin" },
            { 1, "User" }
        };
    }
}
