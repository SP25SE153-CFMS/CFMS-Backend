using Microsoft.AspNetCore.SignalR;

namespace CFMS.Application.Services.SignalR
{
    public class NotiHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
