using TurnTableBase;
using TurnTableDomain.Models;

namespace TurnTableDomain.Games.LinkFour
{
    public class WordGolf: Game
    {
        public override GameType GameType => GameType.WordTrain;
        public override object GameState => _words;
        public override int MaxPlayers => 1;

        private const int WORD_COUNT = 6;
        private const int CHAR_COUNT = 8;

        private string[] _words = new string[WORD_COUNT];

        public WordGolf(Player playerOne) : base(playerOne)
        {
            this._words = GenerateWords();
        }

        public override void NewMove(int playerNumber, object arg1, object arg2, object arg3)
        {
            throw new Exception("This game is single player offline; this method should not be called.");
        }

        public string[] GenerateWords()
        {
            return new string[]
            {
                "TEST",
                "TUBE",
                "TOP",
                "SECRET",
                "SANTA",
                "HAT"
            };
        }
    }
}
