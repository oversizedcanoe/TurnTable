using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurnTableDomain.Services
{
    public class GameHub : Hub
    {
        public async Task JoinGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task JoinGame(string gameCode, string playerName, int playerNumber)
        {
            await Clients.Group(gameCode).SendAsync("JoinGame", playerName, playerNumber);
        }
    }
}
