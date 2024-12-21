using TurnTableBase;

namespace TurnTableApplication.Requests
{
    public class NewGameRequest : BaseRequest
    {
        public GameType GameType { get; set; }

        public string PlayerOneName { get; set; } = string.Empty;
    }
}
