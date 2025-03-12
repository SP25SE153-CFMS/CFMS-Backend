using CFMS.Domain.Enums.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Services.Impl
{
    public class UtilityService : IUtilityService
    {
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

        private Dictionary<EntityType, int> _counters = new()
        {
            { EntityType.Farm, 0 },
            { EntityType.Product, 0 },
            { EntityType.ChickenCoop, 0 },
            { EntityType.Equipment, 0 },
            { EntityType.BreedingArea, 0 }
        };

        public string GenerateCode(EntityType type)
        {
            _counters[type]++;

            string prefix = type switch
            {
                EntityType.Farm => "FARM",
                EntityType.Product => "PROD",
                EntityType.ChickenCoop => "COOP",
                EntityType.Equipment => "EQUIP",
                EntityType.BreedingArea => "BREED",
                _ => "DEF"
            };

            return $"{prefix}{_counters[type].ToString("D2")}";
        }
    }
}
