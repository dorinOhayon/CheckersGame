using System;
using System.Drawing;
using System.Collections.Generic;

namespace LogicCheckersGame
{
    public class Tool
    {
        private char m_SignType;
        private bool m_IsKing;
        private Point m_ToolLocation;
        private List<Move> m_ValidMoves;

        public enum eSigns
        {
            PlayerO = 'O',
            PlayerX = 'X',
            KingO = 'U',
            KingX = 'K',
            Empty = ' '
        }

        public enum eDirection
        {
            Down = 1,
            Up = -1,
            Left = -1,
            Right = 1
        }

        public Tool(char toolSign, Point toolLocation)
        {
            m_SignType = toolSign;
            m_ToolLocation = toolLocation;
            m_IsKing = false;
            m_ValidMoves = new List<Move>();
        }

        public char Sign
        {
            get
            {
                return m_SignType;
            }

            set
            {
                m_SignType = value;
            }
        }

        public bool IsKing
        {
            get
            {
                return m_IsKing;
            }

            set
            {
                m_IsKing = value;
            }
        }

        public Point Location
        {
            get
            {
                return m_ToolLocation;
            }

            set
            {
                m_ToolLocation = value;
            }
        }

        public List<Move> ValidMoveList
        {
            get
            {
                return m_ValidMoves;
            }

            set
            {
                m_ValidMoves = value;
            }
        }

        public bool CheckIfValidLocation(GameBoard i_Board, Point i_NextMove)
        {

            return (i_NextMove.X < i_Board.BoardSize && i_NextMove.X >= 0 &&
                    i_NextMove.Y < i_Board.BoardSize && i_NextMove.Y >= 0 && i_Board[i_NextMove.X, i_NextMove.Y] == (char)eSigns.Empty);
        }

        public void UpdateValidMoveList(GameBoard m_Board)
        {
            int playerDirection = GetDirection();

            InsertValidRegularMovesToList(m_Board, playerDirection, (int)eDirection.Right);
            InsertValidRegularMovesToList(m_Board, playerDirection, (int)eDirection.Left);
            InsertValidSkipMovesToList(m_Board, playerDirection, (int)eDirection.Right);
            InsertValidSkipMovesToList(m_Board, playerDirection, (int)eDirection.Left);
            if (m_IsKing)
            {
                playerDirection *= -1;
                InsertValidRegularMovesToList(m_Board, playerDirection, (int)eDirection.Right);
                InsertValidRegularMovesToList(m_Board, playerDirection, (int)eDirection.Left);
                InsertValidSkipMovesToList(m_Board, playerDirection, (int)eDirection.Right);
                InsertValidSkipMovesToList(m_Board, playerDirection, (int)eDirection.Left);
            }
        }

        public void InsertValidRegularMovesToList(GameBoard i_Board, int i_Direction, int i_DiagonalDirection)
        {
            Point nextLocation = new Point(m_ToolLocation.X + i_Direction, m_ToolLocation.Y + i_DiagonalDirection);

            if (CheckIfValidLocation(i_Board, nextLocation))
            {
                Move regularMove = new Move(m_ToolLocation, nextLocation, false);
                m_ValidMoves.Add(regularMove);
            }
        }

        public void InsertValidSkipMovesToList(GameBoard i_Board, int i_Direction, int i_DiagonalDirection)
        {
            Point nextLocation = new Point(m_ToolLocation.X + i_Direction, m_ToolLocation.Y + i_DiagonalDirection);

            if (CheckIfValidLocation(i_Board, nextLocation))
            {
                if (CheckIfRivalTool(nextLocation, i_Board) && i_Board[nextLocation.X, nextLocation.Y] != (char)eSigns.Empty)
                {
                    Point nextSkipLocationRight = new Point(m_ToolLocation.X + 2 * i_Direction, m_ToolLocation.Y + 2 * i_DiagonalDirection);
                    if (CheckIfValidLocation(i_Board, nextSkipLocationRight))
                    {
                        Move skipMove = new Move(m_ToolLocation, nextSkipLocationRight, true);
                        m_ValidMoves.Add(skipMove);
                    }
                }
            }
        }

        private bool CheckIfRivalTool(Point i_ToolLocation, GameBoard i_Board)
        {
            bool isRival = true;

            if (m_SignType == (char)eSigns.PlayerX || m_SignType == (char)eSigns.KingX)
            {
                if (i_Board[i_ToolLocation.X, i_ToolLocation.Y] == (char)eSigns.PlayerX || i_Board[i_ToolLocation.X, i_ToolLocation.Y] == (char)eSigns.KingX)
                {
                    isRival = false;
                }
            }
            else
            {
                if (i_Board[i_ToolLocation.X, i_ToolLocation.Y] == (char)eSigns.PlayerO || i_Board[i_ToolLocation.X, i_ToolLocation.Y] == (char)eSigns.KingO)
                {
                    isRival = false;
                }
            }

            return isRival;
        }

        public Point GetEatenToolLocation(int i_Direction, int i_DiagonalDirection)
        {
            Point eaten = new Point(m_ToolLocation.X + i_Direction, m_ToolLocation.Y + i_DiagonalDirection);

            return eaten;
        }

        private int GetDirection()
        {
            int direction;
            if (m_SignType == (char)eSigns.PlayerX || m_SignType == (char)eSigns.KingX)
            {
                direction = (int)eDirection.Up;
            }
            else
            {
                direction = (int)eDirection.Down;
            }

            return direction;
        }

        public void CheckIfToolChangeToKing(int i_BoardSize)
        {
            if (m_ToolLocation.X == 0 && m_SignType == (char)eSigns.PlayerX && !m_IsKing)
            {
                m_SignType = (char)eSigns.KingX;
            }
            else if (m_ToolLocation.X == i_BoardSize - 1 && m_SignType == (char)eSigns.PlayerO && !m_IsKing)
            {
                m_SignType = (char)eSigns.KingO;
            }
        }
    }
}

/*
namespace LogicCheckersGame
{
    public class Tool
    {
        private char m_SignType;
        private bool m_IsKing;
        private Point m_ToolLocation;
        private List<Move> m_ValidMoves;

        public enum eSigns
        {
            PlayerO = 'O',
            PlayerX = 'X',
            KingO = 'U',
            KingX = 'K',
            Empty = ' '
        }

        public enum eDirection
        {
            Down = 1,
            Up = -1,
            Left = -1,
            Right = 1
        }
        public Tool(char toolSign, Point toolLocation)
        {
            m_SignType = toolSign;
            m_ToolLocation = toolLocation;
            m_IsKing = false;
            m_ValidMoves = new List<Move>();
        }

        public char Sign
        {
            get
            {
                return m_SignType;
            }

            set
            {
                m_SignType = value;
            }
        }

        public bool IsKing
        {
            get
            {
                return m_IsKing;
            }

            set
            {
                m_IsKing = value;
            }
        }

        public Point Location
        {
            get
            {
                return m_ToolLocation;
            }

            set
            {
                m_ToolLocation = value;
            }
        }
        
        public List<Move> ValidMoveList
        {
            get
            {
                return m_ValidMoves;
            }
            set
            {
                m_ValidMoves = value;
            }
        }
        public bool CheckIfValidLocation(GameBoard i_Board, Point i_NextMove)
        {
            return (i_NextMove.X < i_Board.BoardSize && i_NextMove.X >= 0 &&
                    i_NextMove.Y < i_Board.BoardSize && i_NextMove.Y >= 0 && i_Board[i_NextMove.X, i_NextMove.Y]== (char)eSigns.Empty);
                
        }

        public void UpdateValidMoveList(GameBoard m_Board)
        {
            int playerDirection = GetDirection();

            InsertValidRegularMovesToList(m_Board, playerDirection, (int)eDirection.Right);
            InsertValidRegularMovesToList(m_Board, playerDirection, (int)eDirection.Left);
            InsertValidSkipMovesToList(m_Board, playerDirection, (int)eDirection.Right);
            InsertValidSkipMovesToList(m_Board, playerDirection, (int)eDirection.Left);

            if(m_IsKing)
            {
                playerDirection *= -1;
                InsertValidRegularMovesToList(m_Board, playerDirection, (int)eDirection.Right);
                InsertValidRegularMovesToList(m_Board, playerDirection, (int)eDirection.Left);
                InsertValidSkipMovesToList(m_Board, playerDirection, (int)eDirection.Right);
                InsertValidSkipMovesToList(m_Board, playerDirection, (int)eDirection.Left);
            }

        }
        public void InsertValidRegularMovesToList(GameBoard i_Board, int i_Direction, int i_DiagonalDirection)
        {
            Point nextLocation = new Point(m_ToolLocation.X + i_Direction, m_ToolLocation.Y + i_DiagonalDirection);
            
            if(CheckIfValidLocation(i_Board, nextLocation))
            {
                Move regularMove = new Move(m_ToolLocation, nextLocation, false);
                m_ValidMoves.Add(regularMove);
            }
        }

        public void InsertValidSkipMovesToList(GameBoard i_Board, int i_Direction, int i_DiagonalDirection)
        {
            Point nextLocation = new Point(m_ToolLocation.X + i_Direction, m_ToolLocation.Y + i_DiagonalDirection);

            if (CheckIfValidLocation(i_Board, nextLocation))
            {
                if (CheckIfRivalTool(nextLocation, i_Board) && i_Board[nextLocation.X, nextLocation.Y] != (char)eSigns.Empty)
                {
                    Point nextSkipLocationRight= new Point(m_ToolLocation.X + 2*i_Direction, m_ToolLocation.Y + 2* i_DiagonalDirection);
                    if(CheckIfValidLocation(i_Board, nextSkipLocationRight))
                    {
                        Move skipMove = new Move(m_ToolLocation, nextSkipLocationRight, true);
                        m_ValidMoves.Add(skipMove);
                    }

                }              
            }
        }

        private bool CheckIfRivalTool(Point i_ToolLocation, GameBoard i_Board)
        {
            bool isRival = false;
            if(m_SignType == (char)eSigns.PlayerX || m_SignType == (char)eSigns.KingX)
            {
                if (i_Board[i_ToolLocation.X, i_ToolLocation.Y] != (char)eSigns.PlayerX && i_Board[i_ToolLocation.X, i_ToolLocation.Y] != (char)eSigns.KingX)
                {
                    isRival = true;
                }
            }

            else
            {
                if (i_Board[i_ToolLocation.X, i_ToolLocation.Y] != (char)eSigns.PlayerO && i_Board[i_ToolLocation.X, i_ToolLocation.Y] != (char)eSigns.KingO)
                {
                    isRival = true;
                }

            }

            return isRival;
        }

        public Point GetEatenToolLocation(int i_Direction, int i_DiagonalDirection)
        {
            Point eaten = new Point(m_ToolLocation.X + i_Direction, m_ToolLocation.Y + i_DiagonalDirection);
            return eaten;

        }
        public int GetDirection()
        {
            int direction;
            if(m_SignType == (char)eSigns.PlayerX ||m_SignType == (char)eSigns.KingX)
            {
                direction = (int)eDirection.Up;
            }

            else
            {
                direction = (int)eDirection.Down;
            }

            return direction;
        }

        public void CheckIfToolChangeToKing(int i_BoardSize)
        {
            if(m_ToolLocation.X == 0 && m_SignType == (char)eSigns.PlayerX && !m_IsKing)
            {
                m_SignType = (char)eSigns.KingX;
            }
            else if(m_ToolLocation.X == i_BoardSize -1 && m_SignType == (char)eSigns.PlayerO && !m_IsKing)
            {
                m_SignType = (char)eSigns.KingO;
            }
        }
    }
}
*/
