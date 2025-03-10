using CFMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Domain.Entities
{
    public class RevokedToken
    {
        public Guid RevokedTokenId { get; set; }
        public string Token { get; set; } = string.Empty;
        public TokenType TokenType { get; set; }
        public DateTime RevokedAt { get; set; } = DateTime.UtcNow;
        public DateTime ExpiryDate { get; set; }
        public Guid? UserId { get; set; }

        public virtual User? User { get; set; }
    }
}
