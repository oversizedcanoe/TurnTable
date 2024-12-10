using Microsoft.AspNetCore.SignalR;

namespace TurnTableDomain.Hubs
{
    public class GameHub : Hub
    {
        public async Task JoinGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task GameStateChanged(string gameCode)
        {
            await Clients.Group(gameCode).SendAsync("GameStateChanged");
        }
    }
}
