using TurnTableDomain.Models;

namespace TurnTableApplication.DTOs
{
    public record PlayerDTO
    {
        public required string PlayerName { get; set; }
        public int PlayerNumber { get; set; }

        public static PlayerDTO FromPlayer(Player player)
        {
            return new PlayerDTO()
            {
                PlayerName = player.Name,
                PlayerNumber = player.PlayerNumber
            };
        }
    }
}
