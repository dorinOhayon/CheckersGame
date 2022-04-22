using System;
using System.Drawing;

namespace LogicCheckersGame
{
    public class GameBoard
    {
        private char[,] m_Board;
        private int m_BoardSize;

        public enum eValidBoardSize
        {
            Small = 6,
            Medium = 8,
            Large = 10
        }

        public int BoardSize
        {
            get
            {
                return m_BoardSize;
            }

            set
            {
                m_BoardSize = value;
            }
        }

        public char this[int i_Row, int i_Colum]
        {
            get
            {
                return m_Board[i_Row, i_Colum];
            }

            set
            {
                m_Board[i_Row, i_Colum] = value;
            }
        }

        public void CreateGameBoard()
        {
            m_Board = new char[m_BoardSize, m_BoardSize];
            initMiddleRowsOnBoard();
        }

        public bool CheckIfValidBoardSize(string i_SizeInput, out int o_ValidBoardSize)
        {
            bool isValid = true;

            isValid = int.TryParse(i_SizeInput, out o_ValidBoardSize);

            if (isValid)
            {
                if (o_ValidBoardSize == (int)GameBoard.eValidBoardSize.Small || o_ValidBoardSize == (int)GameBoard.eValidBoardSize.Medium || o_ValidBoardSize == (int)GameBoard.eValidBoardSize.Large)
                {
                    isValid = true;
                }
                else
                {
                    isValid = false;
                }
            }

            return isValid;
        }

        public void initMiddleRowsOnBoard()
        {
            int startIndex = (m_BoardSize / 2) - 1;
            int endIndex = (m_BoardSize / 2) + 1;

            for (int i = startIndex; i < endIndex; i++)
            {
                for (int j = 0; j < m_BoardSize; j++)
                {
                    m_Board[i, j] = (char)Tool.eSigns.Empty;
                }
            }
        }

        public void InitPlayerToolsOnBoard(Player i_Player, int i_PlayerNumber)
        {
            int startIndex, endIndex;

            if (i_PlayerNumber == 1)
            {
                startIndex = 0;
                endIndex = (m_BoardSize / 2) - 1;
            }
            else
            {
                startIndex = (m_BoardSize / 2) + 1;
                endIndex = m_BoardSize;
            }

            for (int i = startIndex; i < endIndex; i++)
            {
                for (int j = 0; j < m_BoardSize; j++)
                {
                    if ((i % 2 != 0 && j % 2 == 0) || (i % 2 == 0 && j % 2 != 0))
                    {
                        m_Board[i, j] = i_Player.PlayerToolSign;
                        i_Player.ToolList.Add(new Tool(m_Board[i, j], new Point(i, j)));
                    }
                    else
                    {
                        m_Board[i, j] = (char)Tool.eSigns.Empty;
                    }
                }
            }
        }

        //להשלים את הפונקציה לפי סוג צעד- עדכון נוסף של הלוח אם אוכלים וכו
        public void UpdatePlayerMoveOnBoard(Tool i_ToolToUpdate, Move i_CurrentMove)
        {
            m_Board[i_CurrentMove.CurrentLocation.X, i_CurrentMove.CurrentLocation.Y] = (char)Tool.eSigns.Empty;
            m_Board[i_CurrentMove.NextLocation.X, i_CurrentMove.NextLocation.Y] = i_ToolToUpdate.Sign;
        }
    }
}

/*
namespace LogicCheckersGame
{
    public class GameBoard
    {
        public enum eValidBoardSize
        {
            Small = 6,
            Medium = 8,
            Large = 10
        }

        private char[,] m_Board;
        private int m_BoardSize;

        public int BoardSize
        {
            get
            {
                return m_BoardSize;
            }

            set
            {
                m_BoardSize = value;
            }
        }

        public char this[int i_Row, int i_Colum]
        {
            get
            {
                return m_Board[i_Row, i_Colum];
            }

            set
            {
                m_Board[i_Row, i_Colum] = value;
            }
        }

        public void CreateGameBoard()
        {
            m_Board = new char[m_BoardSize, m_BoardSize];
            initMiddleRowsOnBoard();
        }

        public bool CheckIfValidBoardSize(string i_SizeInput, out int o_ValidBoardSize)
        {
            bool isValid = false;
            isValid = int.TryParse(i_SizeInput, out o_ValidBoardSize);

            if (isValid)
            {
                if (o_ValidBoardSize == (int)GameBoard.eValidBoardSize.Small || o_ValidBoardSize == (int)GameBoard.eValidBoardSize.Medium || o_ValidBoardSize == (int)GameBoard.eValidBoardSize.Large)
                {
                    isValid = true;
                }

                else
                {
                    isValid = false;
                }
            }

            return isValid;
        }

        public void initMiddleRowsOnBoard()
        {
            int startIndex = (m_BoardSize / 2) - 1;
            int endIndex = (m_BoardSize / 2) + 1;

            for (int i = startIndex; i < endIndex; i++)
            {
                for (int j = 0; j < m_BoardSize; j++)
                {
                    m_Board[i, j] = (char)Tool.eSigns.Empty;
                }
            }
        }

        public void InitPlayerToolsOnBoard(Player i_Player, int i_PlayerNumber)
        {
            int startIndex, endIndex;

            if(i_PlayerNumber == 1)
            {
                startIndex = 0;
                endIndex = (m_BoardSize/2)-1;
            }
            else
            {
                startIndex= (m_BoardSize / 2) + 1;
                endIndex = m_BoardSize;
            }

            
            for (int i = startIndex; i < endIndex; i++)
            {
                for (int j = 0; j < m_BoardSize; j++)
                {
                    if ((i % 2 != 0 && j % 2 == 0) || (i % 2 == 0 && j % 2 != 0))
                    {
                        m_Board[i, j] = i_Player.PlayerToolSign;
                       i_Player.ToolList.Add(new Tool(m_Board[i, j], new Point(i, j)));
                    }

                    else
                    {
                        m_Board[i, j] = (char)Tool.eSigns.Empty;
                    }

                }
            }
        }
        //להשלים את הפונקציה לפי סוג צעד- עדכון נוסף של הלוח אם אוכלים וכו
        public void UpdatePlayerMoveOnBoard(Tool i_ToolToUpdate, Move i_CurrentMove)
        {
            m_Board[i_CurrentMove.CurrentLocation.X, i_CurrentMove.CurrentLocation.Y] = (char)Tool.eSigns.Empty;
            m_Board[i_CurrentMove.NextLocation.X, i_CurrentMove.NextLocation.Y] = i_ToolToUpdate.Sign;

        }

    }
}*/








