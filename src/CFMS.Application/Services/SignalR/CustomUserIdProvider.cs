using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Services.SignalR
{
    using Microsoft.AspNetCore.SignalR;
    using System.Security.Claims;

    public class CustomUserIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            return connection?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }

}
