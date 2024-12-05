using TurnTableBase;

namespace TurnTableDomain.Games.LinkFour
{
    public class LinkFour : Game
    {
        public override GameType GameType => GameType.LinkFour;

        private const int ROW_COUNT = 6;
        private const int COL_COUNT = 7;

        private int[][] _gameBoard = new int[ROW_COUNT][];

        public LinkFour(string gameCode, Player playerOne) : base(gameCode, playerOne)
        {
            for (int i = 0; i < ROW_COUNT; i++)
            {
                int[] row = new int[COL_COUNT];
                Array.Fill(row, 0);
                _gameBoard[i] = row;
            }
        }

        public override bool NewMove(int playerNumber, object arg1, object arg2, object arg3)
        {
            int columnNumber = Convert.ToInt32(arg1);

            ProcessMove(playerNumber, columnNumber);

            // todo, need to figure out how to send back a win result and not just "true/false"

            return true;
        }

        private int GetNumberAtPosition(int rowNumber, int colNumber)
        {
            return _gameBoard[rowNumber][colNumber];
        }

        private void SetPosition(int rowNumber, int colNumber, int playerNumber)
        {
            _gameBoard[rowNumber][colNumber] = playerNumber;

            CheckForWin();
        }

        private void ProcessMove(int playerNumber, int columnIndex)
        {
            // Get largest row index (closest row to the bottom) that is 0 for the given columnIndex
            int largestRowIndex = -1;

            for (int i = 0; i < ROW_COUNT; i++)
            {
                int columnValue = GetNumberAtPosition(i, columnIndex);

                if (columnValue == 0)
                {
                    largestRowIndex = i;
                }
            }

            if (largestRowIndex == -1)
            {
                // Column full
                return;
            }

            SetPosition(largestRowIndex, columnIndex, playerNumber);
        }

        private void CheckForWin()
        {

        }
    }
}
