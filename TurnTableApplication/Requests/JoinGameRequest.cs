namespace TurnTableApplication.Requests
{
    public class JoinGameRequest : BaseGameRequest
    {
        public string PlayerName { get; set; } = string.Empty;
    }
}
