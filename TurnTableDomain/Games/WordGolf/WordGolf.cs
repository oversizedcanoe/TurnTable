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
                //["BOOK", "MARK", "ET", "CHING", "OUT", "LOOK"],
                ["LIGHT", "HOUSE", "PLANT", "KINGDOM", "COME", "HERE"],
                ["STAR", "FISH", "TANK", "TOP", "SECRET", "CODE"],
                ["CROSS", "WALK", "WAY", "POINT", "BLANK", "CHECK"],
                //["FIRE", "PLACE", "HOLDER", "FIELD", "GOAL", "KEEPER"],
                //["WATER", "FALL", "BACK", "STROKE", "PLAY", "GROUND"],
                //["SUN", "RISE", "SHINE", "BEAM", "LIGHT", "HOUSE"],
                ["BLACK", "ICE", "BOX", "OFFICE", "SPACE", "BAR"],
                ["SAND", "CASTLE", "WALL", "PAPER", "CLIP", "BOARD"],
                ["STONE", "WALL", "FLOWER", "BED", "POST", "CARD"],
                ["BOARD", "GAME", "READY", "MADE", "MAN", "HOLE"],
                ["CAT", "NAP", "TIME", "ZONE", "OUT", "SIDE"],
                //["FRONT", "STEP", "LADDER", "CLIMB", "MOUNTAIN", "PEAK"],
                ["ROOM", "MATE", "TEA", "CUP", "BOARD", "ROOM"],
                ["PLANE", "CRASH", "LANDING", "STRIP", "TEASE", "OUT"],
                ["BIRD", "CAGE", "DOOR", "STOP", "SIGN", "AGE"],
                //["ROAD", "BLOCK", "CHAIN", "LINK", "PAGE", "NUMBER"],
                //["TRAIN", "TRACK", "FIELD", "GOAL", "KEEPER", "BOARD"],


                ["COLD", "FRONT", "LINE", "OUT", "LOOK", "UP"],
                ["HOT", "SPOT", "LIGHT", "HOUSE", "PLANT", "POT"],
                ["BLACK", "BOARD", "GAME", "PLAN", "ET", "AL"],
                ["WHITE", "BOARD", "GAME", "ROOM", "MATE", "RATE"],
                ["GREEN", "HOUSE", "PLANT", "POT", "HOLE", "SHOT"],
                ["RED", "CARD", "GAME", "PLAN", "BOARD", "ROOM"],
                ["YELLOW", "BRICK", "ROAD", "BLOCK", "CHAIN", "LINK"],
                ["BLUE", "PRINT", "LINE", "OUT", "LOOK", "UP"],
                ["SILVER", "LINING", "CLOUD", "COVER", "STORY", "TIME"],
                ["GOLD", "MINE", "FIELD", "GOAL", "KEEPER", "BOARD"],
                ["IRON", "MAN", "POWER", "PLANT", "POT", "HOLE"],
                ["WOOD", "CHIP", "CARD", "GAME", "PLAN", "ET"],
                ["PAPER", "CLIP", "BOARD", "GAME", "ROOM", "MATE"],
                ["GLASS", "HOUSE", "KEEPER", "BOARD", "ROOM", "MATE"],
                ["PLASTIC", "WRAP", "UP", "GRADE", "POINT", "BLANK"],
                ["COTTON", "CANDY", "CANE", "FIELD", "TRIP", "WIRE"],
                ["SUGAR", "CANE", "FIELD", "GOAL", "KEEPER", "BOARD"],
                ["SALAD", "DRESSING", "ROOM", "MATE", "RATE", "CARD"],
                ["BUTTER", "FLY", "TRAP", "DOOR", "STOP", "SIGN"],
                ["FISH", "TANK", "TOP", "FLOOR", "PLAN", "ET"],
                ["HORSE", "SHOE", "LACE", "STRING", "BEAN", "BAG"],
                ["BAG", "PIPE", "LINE", "OUT", "LOOK", "UP"],
                ["FIELD", "GOAL", "KEEPER", "BOARD", "GAME", "PLAN"],
                ["FARM", "HOUSE", "KEEPER", "BOARD", "GAME", "ROOM"],
                ["BEACH", "BALL", "ROOM", "MATE", "RATE", "CARD"],
                ["BOAT", "DOCK", "YARD", "WORK", "SHOP", "PING"],
                ["PLANT", "POT", "HOLE", "SHOT", "GUN", "METAL"],
                ["CAR", "WASH", "ROOM", "MATE", "RATE", "CARD"],
                ["BIKE", "RACK", "ROOM", "MATE", "RATE", "CARD"],
                ["SHIP", "YARD", "WORK", "SHOP", "PING", "BALL"],
                ["CUP", "HOLDER", "FIELD", "GOAL", "KEEPER", "BOARD"],
                ["CHAIN", "LINK", "PAGE", "NUMBER", "LINE", "OUT"],
                ["TOWN", "SQUARE", "ROOT", "BEER", "MUG", "SHOT"],
                ["CITY", "BLOCK", "CHAIN", "LINK", "PAGE", "NUMBER"],
                ["MARK", "ET", "CHING", "OUT", "LOOK", "UP"],
                ["DOOR", "STOP", "SIGN", "BOARD", "GAME", "ROOM"],
                ["WINDOW", "FRAME", "WORK", "SHOP", "PING", "BALL"],
                ["SCHOOL", "BUS", "STOP", "SIGN", "BOARD", "GAME"],
                ["BRAIN", "STORM", "CLOUD", "COVER", "STORY", "TIME"],
                ["EARTH", "WORM", "HOLE", "SHOT", "GUN", "METAL"],
                ["MOON", "LIGHT", "HOUSE", "PLANT", "POT", "HOLE"],
                ["SPACE", "BAR", "CODE", "WORD", "SEARCH", "LIGHT"],
                ["TIME", "MACHINE", "SHOP", "PING", "BALL", "ROOM"],
                ["LIFE", "LINE", "OUT", "LOOK", "UP", "GRADE"],
                ["LOVE", "BIRD", "CAGE", "DOOR", "STOP", "SIGN"],
                ["HAPPY", "HOUR", "GLASS", "HOUSE", "KEEPER", "BOARD"],
                ["SAD", "FACE", "BOOK", "MARK", "ET", "CHING"],
                ["FAST", "TRACK", "FIELD", "GOAL", "KEEPER", "BOARD"],
                ["SLOW", "MOTION", "PICTURE", "FRAME", "WORK", "SHOP"],
                ["UP", "GRADE", "POINT", "BLANK", "CHECK", "LIST"],
                ["DOWN", "FALL", "LINE", "OUT", "LOOK", "UP"],
                ["RIGHT", "ANGLE", "IRON", "MAN", "POWER", "PLANT"],
                ["LEFT", "TURN", "TABLE", "TOP", "FLOOR", "PLAN"],
                ["NORTH", "POLE", "VAULT", "DOOR", "STOP", "SIGN"],
                ["SOUTH", "PAW", "PRINT", "LINE", "OUT", "LOOK"],
                ["EAST", "WIND", "MILL", "STONE", "WALL", "FLOWER"],
                ["WEST", "ERN", "UNION", "JACK", "POT", "HOLE"],
                ["FAMILY", "TREE", "HOUSE", "KEEPER", "BOARD", "ROOM"],
                ["FRIEND", "SHIP", "YARD", "WORK", "SHOP", "PING"],
                ["WORK", "SHOP", "PING", "BALL", "ROOM", "MATE"],
                ["PLAY", "GROUND", "ZERO", "POINT", "BLANK", "CHECK"],
                ["GROUND", "ZERO", "POINT", "BLANK", "CHECK", "LIST"],
                ["ZERO", "POINT", "BLANK", "CHECK", "LIST", "LINE"],
                ["FINAL", "ROUND", "ABOUT", "FACE", "BOOK", "MARK"],
                ["ROUND", "ABOUT", "FACE", "BOOK", "MARK", "ET"]];

            return availableWords[new Random().Next(0, availableWords.Count)];
        }
    }
}
