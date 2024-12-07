namespace TurnTableApplication.DTOs
{
    public class NewGameDTO
    {
        public string GameCode { get; set; } = string.Empty;

        public NewGameDTO(string gameCode)
        {
            this.GameCode = gameCode;
        }
    }
}
