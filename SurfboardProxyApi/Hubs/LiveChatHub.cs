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

            string responseMessage;

            switch (message.ToLowerInvariant())
            {
                case var m when m.Contains("fragt tid"):
                    responseMessage = "Fragt manden kommer når han kommer";
                    break;

                case var m when m.Contains("fragt pris"):
                    responseMessage = "Frag prisen er 100 kr.";
                    break;

                case var m when m.Contains("kage tid"):
                    responseMessage = "Altid tid";
                    break;
                case var m when m.Contains("retur ret"):
                case var mn when mn.Contains("fortrydelses ret"):
                    responseMessage = "Du kan fortryde dit køb inden for 14 dage";
                    break;
                case var m when m.Contains("vejrudsigt"):
                case var mn when mn.Contains("aktie"):
                    responseMessage = "Dette er uden for mine kompetancer";
                    break;
                case var m when m.Contains("kontakt"):
                    responseMessage = "Du kan skrive en mail os, eller ringe til os på +45 88 88 88 88";
                    break;
                case var m when m.Contains("hej"):
                    responseMessage = "Hej med dig, er der noget jeg kan hjælpe med?";
                    break;
                case var m when m.Contains("joke"):
                    responseMessage = "Hvorfor har en programmør altid briller? ........ fordi de skal kunne C-Sharp";
                    break;

                default:
                    responseMessage = "Forstod ikke besked, prøv igen eller henvend dig på telefon +45 88 88 88 88";
                    return;
            }

            await Clients.Client(Context.ConnectionId)
                .SendAsync("ReceiveMessage", responseMessage);
        }
    }
}
