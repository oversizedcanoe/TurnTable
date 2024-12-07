namespace TurnTableBase
{
    public enum GameType
    {
        LinkFour = 1,
        Scat = 2
    }

    public enum MoveResultCode
    {
        NextTurn = 1,
        GameOver = 2,
        Player1Win = 3,
        Player2Win = 4,
    }
}
