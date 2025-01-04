using TurnTableBase;
using TurnTableDomain.Models;

namespace TurnTableDomain.Games.LinkFour
{
    public class WordGolf : Game
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
            List<string[]> availableWords = [
                ["CHAIN", "REACTION", "TIME", "ZONE", "OUT", "BREAK"] ,
                ["HAND", "SHAKE", "DOWN", "FALL", "GUY", "WIRE"],
                ["LIGHT", "HOUSE", "PLANT", "KINGDOM", "COME", "HERE"],
                ["STAR", "FISH", "TANK", "TOP", "SECRET", "CODE"],
                ["CROSS", "WALK", "WAY", "POINT", "BLANK", "CHECK"],
                ["CROSS", "HAIR", "BRUSH", "FIRE", "PLACE", "HOLDER"],
                ["CROSS", "OVER" ,"UNDER", "WATER", "FALL", "BACK"],
                ["FOR", "EVER", "AFTER", "SUN", "RISE", "UP"],
                ["BLACK", "ICE", "BOX", "OFFICE", "SPACE", "BAR"],
                ["SAND", "CASTLE", "WALL", "PAPER", "CLIP", "BOARD"],
                ["STONE", "WALL", "FLOWER", "BED", "POST", "CARD"],
                ["BOARD", "GAME", "READY", "MADE", "MAN", "HOLE"],
                ["CAT", "NAP", "TIME", "ZONE", "OUT", "SIDE"],
                ["ROOM", "MATE", "TEA", "CUP", "BOARD", "ROOM"],
                ["PLANE", "CRASH", "LANDING", "STRIP", "TEASE", "OUT"],
                ["BIRD", "CAGE", "DOOR", "STOP", "SIGN", "AGE"],
                ["TAKE", "OFF","ROAD", "BLOCK", "CHAIN", "LINK"],
                ["COLD", "FRONT", "LINE", "COOK", "UP", "BEAT"],
                ["RED", "CARD", "GAME", "TIME", "OUT", "SIDE"],
                ["BLUE", "PRINT", "OUT", "BACK", "SIDE", "WAYS"],
                ["PAPER", "CLIP", "BOARD", "GAME", "ROOM", "MATE"],
                ["PLASTIC", "WRAP", "UP", "GRADE", "POINT", "BLANK"],
                ["COTTON", "CANDY", "CANE", "SUGAR", "MAMA", "BEAR"],
                ["BLOODY", "CAESER", "SALAD", "DRESSING", "ROOM", "MATE"],
                ["BUTTER", "FLY", "TRAP", "DOOR", "STOP", "SIGN"],
                ["FISH", "TANK", "TOP", "FLOOR", "PLAN", "B"],
                ["BIG", "FOOT", "RACE", "HORSE", "SHOE", "LACE"],
                ["CELL", "PHONE", "BOOK", "BAG", "PIPE", "CLEANER"],
                ["BOAT", "DOCK", "YARD", "WORK", "SHOP", "LIFTER"],
                ["SHOT", "CLOCK", "WISE", "CRACK", "POT", "HOLE"],
                ["SHIP", "YARD", "WORK", "SHOP", "PING", "BALL"],
                ["TOWN", "SQUARE", "ROOT", "BEER", "MUG", "SHOT"],
                ["CITY", "BLOCK", "CHAIN", "LINK", "PAGE", "NUMBER"],
                ["SCHOOL", "BUS", "STOP", "SIGN", "HERE", "FORTH"],
                ["BRAIN", "STORM", "CLOUD", "COVER", "STORY", "TIME"],
                ["EARTH", "WORM", "HOLE", "UP", "SIDE", "WAYS"],
                ["PULL", "OVER", "TIME", "MACHINE", "SHOP", "TALK"],
                ["LIFE", "LINE", "OUT", "LOOK", "UP", "GRADE"],
                ["SAD", "FACE", "BOOK", "MARK", "DOWN", "LOAD"],
                ["SLOW", "MOTION", "PICTURE", "FRAME", "WORK", "SHOP"],
                ["UP", "GRADE", "POINT", "BLANK", "CHECK", "LIST"],
                ["COME", "DOWN", "FALL", "OUT", "LOOK", "UP"],
                ["LEFT", "TURN", "TABLE", "TOP", "FLOOR", "PLAN"],
                ["NORTH", "POLE", "VAULT", "DOOR", "STOP", "WATCH"],
                ["DOWN", "SOUTH", "PAW", "PRINT", "SCREEN", "SAVER"],
                ["FINAL", "ROUND", "ABOUT", "FACE", "BOOK", "MARK"]];

            return availableWords[new Random().Next(0, availableWords.Count)];
        }
    }
}
