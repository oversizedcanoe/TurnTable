﻿using Newtonsoft.Json;
using TurnTableBase;
using TurnTableDomain.Models;

namespace TurnTableDomain.Games.LinkFour
{
    public class LinkFour : Game
    {
        public override GameType GameType => GameType.LinkFour;
        public override object GameState => _gameBoard;

        private const int ROW_COUNT = 6;
        private const int COL_COUNT = 7;

        private int[][] _gameBoard = new int[ROW_COUNT][];

        public LinkFour(Player playerOne) : base(playerOne)
        {
            for (int i = 0; i < ROW_COUNT; i++)
            {
                int[] row = new int[COL_COUNT];
                Array.Fill(row, 0);
                _gameBoard[i] = row;
            }
        }

        public override void NewMove(int playerNumber, object arg1, object arg2, object arg3)
        {
            int columnNumber = DeserializeArg<int>(arg1);

            ProcessMove(playerNumber, columnNumber);
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

            if (BoardIsFull())
            {
                this.GameOver = true;
                return;
            }

            bool win = CheckForWin(out int winnerPlayer);

            if (win)
            {
                this.PlayerWinner = winnerPlayer;
                this.GameOver = true;
            }

            if (win == false)
            {
                SetNextPlayerTurn();
            }
        }

        private bool BoardIsFull()
        {
            bool hasOpenSpace = false;

            foreach (var row in _gameBoard)
            {
                foreach (var col in row)
                {
                    if (col == 0)
                    {
                        hasOpenSpace = true;
                        break;
                    }
                }
            }

            return hasOpenSpace == false;
        }

        private bool CheckForWin(out int playerNumber)
        {
            Console.WriteLine("Check for win");
            for (int rowIndex = 0; rowIndex < ROW_COUNT; rowIndex++)
            {
                for(int colIndex =  0; colIndex < COL_COUNT; colIndex++)
                {
                    int token = _gameBoard[rowIndex][colIndex];

                    if (token == 0)
                    {
                        continue;
                    }

                    playerNumber = token;

                    // First check in a row (all in the same column)
                    // But no need to check if this index is greater than COL_COUNT - 4, as there is not enough room for four in a row then
                    if (colIndex <= COL_COUNT - 4)
                    {
                        int sameTokenQuantityInARow = 1;
                        int innerColIndex = colIndex + 1;

                        while (innerColIndex < COL_COUNT)
                        {
                            if (_gameBoard[rowIndex][innerColIndex] == token)
                            {
                                sameTokenQuantityInARow += 1;

                                if (sameTokenQuantityInARow == 4)
                                {
                                    return true;
                                }
                            }

                            innerColIndex++;
                        }
                    }

                }
            }

            playerNumber = -1;
            return false;
        }

        private void SetNextPlayerTurn()
        {
            int playerCount = this.Players.Count;

            CurrentPlayerTurn += 1;

            if (CurrentPlayerTurn > playerCount)
            {
                CurrentPlayerTurn = 1;
            }
        }
    }
}
