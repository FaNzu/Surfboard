using Microsoft.AspNetCore.SignalR;

namespace SurfboardApi.Hubs
{
    public class LiveChatHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            await Clients.Client(Context.ConnectionId).SendAsync("ReceiveMessage", "Velkommen! Hvad kan vi hjælpe med?");


        }
        public async Task SendMessageAsync(string message)
        {
            await Task.Delay(new TimeSpan(0, 0, 3));
            if (message.Contains("fragt tid", StringComparison.OrdinalIgnoreCase))
            {
                await Clients.Client(Context.ConnectionId)
                    .SendAsync("ReceiveMessage", "Fragt manden kommer når han kommer");

            }
            if (message.Contains("fragt pris", StringComparison.OrdinalIgnoreCase))
            {
                await Clients.Client(Context.ConnectionId)
                    .SendAsync("ReceiveMessage", "Frag prisen er 100 kr.");

            }
        }
    }
}
