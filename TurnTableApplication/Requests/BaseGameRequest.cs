namespace TurnTableApplication.Requests
{
    public class BaseGameRequest : BaseRequest
    {
        public string GameCode { get; set; } = string.Empty;
    }
}
