using TurnTableBase;

namespace TurnTableApplication.Requests
{
    public class NewGameRequest
    {
        public GameType GameType { get; set; }

        public string PlayerOneName { get; set; } = string.Empty;
    }
}
