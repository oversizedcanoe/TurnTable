﻿using Microsoft.AspNetCore.SignalR;
using System.Text;
using TurnTableBase;
using TurnTableDomain.Games.LinkFour;
using TurnTableDomain.Hubs;
using TurnTableDomain.Models;

namespace TurnTableDomain.Services
{
    public class GameManager
    {
        public Dictionary<string, Game> Games { get; set; } = new();

        private const string GAME_CODE_CHARS = "ABCDEFGHJKMNPQRSTUVWXYZ";
        private readonly IHubContext<GameHub> _gameHub;

        public GameManager(IHubContext<GameHub> gameHub)
        {
            this._gameHub = gameHub;
        }

        public string StartNewGame(GameType gameType, string playerOneName)
        {
            Game game;

            string gameCode = GenerateGameCode();

            switch (gameType)
            {
                case GameType.LinkFour:
                    Player player = new Player(1, playerOneName);
                    game = new LinkFour(player);
                    break;
                case GameType.WordTrain:
                    game = new WordGolf(Player.SinglePlayer);
                    break;
                default:
                    throw new Exception($"Unknown GameType passed: {gameType}");
            }

            Games[gameCode] = game;

            return gameCode;
        }

        public async Task<int> JoinGame(string gameCode, string playerName)
        {
            Game game = FindGame(gameCode);

            // Allow user to "re-join" game
            Player? player = game.Players.FirstOrDefault(p => p.Name == playerName);

            if (player != null)
            {
                return player.PlayerNumber;
            }

            if (game.Players.Count >= game.MaxPlayers)
            {
                throw new Exception("Game is full!");
            }

            Player addedPlayer = game.AddPlayer(playerName);

            await SendGameStateChanged(gameCode);

            return addedPlayer.PlayerNumber;
        }

        public async Task Move(string gameCode, int playerNumber, object arg1, object arg2, object arg3)
        {
            Game game = FindGame(gameCode);

            game.NewMove(playerNumber, arg1, arg2, arg3);

            await SendGameStateChanged(gameCode);
        }

        public Game FindGame(string gameCode)
        {
            Game? game = FindGameOrDefault(gameCode);

            if (game == null)
            {
                throw new GameNotFoundException();
            }

            return game;
        }

        public Game? FindGameOrDefault(string gameCode)
        {
            Games.TryGetValue(gameCode.ToUpper(), out Game? game);

            return game;
        }

        public async Task PlayAgain(string gameCode, int playerNumber)
        {
            Game game = FindGame(gameCode);

            game.Restart();

            await SendGameStateChanged(gameCode);
        }

        private string GenerateGameCode()
        {
            Random random = new Random();
            int length = GAME_CODE_CHARS.Length;
            string gameCode = string.Empty;

            do
            {
                StringBuilder sb = new StringBuilder();

                sb.Append(GAME_CODE_CHARS[random.Next(0, length)]);
                sb.Append(GAME_CODE_CHARS[random.Next(0, length)]);
                sb.Append(GAME_CODE_CHARS[random.Next(0, length)]);
                sb.Append(GAME_CODE_CHARS[random.Next(0, length)]);

                gameCode = sb.ToString();
            }
            while (Games.ContainsKey(gameCode));
            // In the future, could have seperate service with an available list of game codes that 
            // generates more on the fly when it gets low.

            return gameCode;
        }

        private string GenerateUpdateEndpoint(string baseBackendUrl, string gameCode)
        {
            Uri fullUri = new Uri(new Uri(baseBackendUrl, UriKind.Absolute), "/hub" + "/" + "code");
            return fullUri.ToString();
        }

        private async Task SendGameStateChanged(string gameCode)
        {
            await this._gameHub.Clients.Group(gameCode).SendAsync("GameStateChanged");
        }
    }
}
