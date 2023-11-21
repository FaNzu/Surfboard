using Microsoft.AspNetCore.SignalR;

namespace SurfboardApi.Hubs
{
    public class LiveChatHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            await Clients.Client(Context.ConnectionId).SendAsync("ReceiveMessage", "Welcome. Please enter your message");
        }
    }
}
