using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public class RevokedToken
{
    public Guid RevokedTokenId { get; set; }

    public string Token { get; set; } = null!;

    public int TokenType { get; set; }

    public DateTime? RevokedAt { get; set; }

    public DateTime? ExpiryDate { get; set; }

    public Guid? UserId { get; set; }

    public virtual User? User { get; set; }
}
