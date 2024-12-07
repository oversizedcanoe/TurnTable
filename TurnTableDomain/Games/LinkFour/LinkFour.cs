using TurnTableBase;
using TurnTableDomain.Models;

namespace TurnTableDomain.Games.LinkFour
{
    public class LinkFour : Game
    {
        public override GameType GameType => GameType.LinkFour;

        private const int ROW_COUNT = 6;
        private const int COL_COUNT = 7;

        private int[][] _gameBoard = new int[ROW_COUNT][];

        public LinkFour(string gameCode, Player playerOne, string updateEndpoint) : base(gameCode, playerOne, updateEndpoint)
        {
            for (int i = 0; i < ROW_COUNT; i++)
            {
                int[] row = new int[COL_COUNT];
                Array.Fill(row, 0);
                _gameBoard[i] = row;
            }
        }

        public override MoveResultCode NewMove(int playerNumber, object arg1, object arg2, object arg3)
        {
            int columnNumber = Convert.ToInt32(arg1);

            ProcessMove(playerNumber, columnNumber);

            MoveResultCode resultCode = CheckForWin();
            
            return resultCode;
        }

        private int GetNumberAtPosition(int rowNumber, int colNumber)
        {
            return _gameBoard[rowNumber][colNumber];
        }

        private void SetPosition(int rowNumber, int colNumber, int playerNumber)
        {
            _gameBoard[rowNumber][colNumber] = playerNumber;
        }

        private void ProcessMove(int playerNumber, int columnIndex)
        {
            UpdateLastActiveDate();

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

        private MoveResultCode CheckForWin()
        {
            // TODO
            return MoveResultCode.NextTurn;
        }
    }
}
