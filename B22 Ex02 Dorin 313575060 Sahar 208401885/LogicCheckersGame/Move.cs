using System;
using System.Drawing;
using System.Collections.Generic;

namespace LogicCheckersGame
{
    public class Move
    {
        private Point m_CurrentLocation;
        private Point m_NextLocation;
        private bool m_IsSkip;

        public Move(Point currentLocation, Point nextLocation, bool isSkip)
        {
            m_CurrentLocation = currentLocation;
            m_NextLocation = nextLocation;
            m_IsSkip = isSkip;
        }

        public Point CurrentLocation
        {
            get
            {
                return m_CurrentLocation;
            }

            set
            {
                m_CurrentLocation = value;
            }
        }

        public Point NextLocation
        {
            get
            {
                return m_NextLocation;
            }

            set
            {
                m_NextLocation = value;
            }
        }

        public bool IsSkip
        {
            get
            {
                return m_IsSkip;
            }

            set
            {
                m_IsSkip = value;
            }
        }

        public void UpdateMove(List<Tool> i_CurrentPlayerList, List<Tool> i_SecondPlayerList, GameBoard i_Board, int i_Direction, int i_DiagonalDirection)
        {
            Tool toMove, toDelete;

            toMove = FindTool(i_CurrentPlayerList, m_CurrentLocation);
            toMove.Location = m_NextLocation;
            i_Board.UpdatePlayerMoveOnBoard(toMove, this);
            if (m_IsSkip)
            {
                Point toDeleteLocation = toMove.GetEatenToolLocation(i_Direction, i_DiagonalDirection);
                toDelete = FindTool(i_SecondPlayerList, toDeleteLocation);
                i_Board[toDeleteLocation.X, toDeleteLocation.Y] = (char)Tool.eSigns.Empty;
                i_SecondPlayerList.Remove(toDelete);
            }

            toMove.CheckIfToolChangeToKing(i_Board.BoardSize);
        }

        public Tool FindTool(List<Tool> i_ToolList, Point i_ToolLocation)
        {
            Tool toolToReturn = null;

            foreach (Tool currentTool in i_ToolList)
            {
                if (currentTool.Location == i_ToolLocation)
                {
                    toolToReturn = currentTool;
                    break;
                }
            }

            return toolToReturn;
        }
    }
}


/*
namespace LogicCheckersGame
{
    public class Move
    {
        private Point m_CurrentLocation;
        private Point m_NextLocation;
        private bool m_IsSkip;

        public Move(Point currentLocation, Point nextLocation, bool isSkip)
        {
            m_CurrentLocation = currentLocation;
            m_NextLocation = nextLocation;
            m_IsSkip = isSkip;
        }
        public Point CurrentLocation
        {
            get
            {
                return m_CurrentLocation;
            }
            
            set
            {
                m_CurrentLocation = value;
            }
        }

        public Point NextLocation
        {
            get
            {
                return m_NextLocation;
            }

            set
            {
                m_NextLocation = value;
            }
        }

        public bool IsSkip
        {
            get
            {
                return m_IsSkip;
            }

            set
            {
                m_IsSkip = value;
            }
        }

        public void UpdateMove(List<Tool> i_CurrentPlayerList, List<Tool> i_SecondPlayerList, GameBoard i_Board ,int i_Direction, int i_DiagonalDirection)
        {
            Tool toMove, toDelete;
            toMove = FindTool(i_CurrentPlayerList, m_CurrentLocation);
            toMove.Location = m_NextLocation;
            i_Board.UpdatePlayerMoveOnBoard(toMove, this);

            if(m_IsSkip)
            {
                Point toDeleteLocation = toMove.GetEatenToolLocation(i_Direction, i_DiagonalDirection);
                toDelete = FindTool(i_SecondPlayerList, toDeleteLocation);
                i_Board[toDeleteLocation.X, toDeleteLocation.Y] = (char)Tool.eSigns.Empty;
                i_SecondPlayerList.Remove(toDelete);
            }

            toMove.CheckIfToolChangeToKing(i_Board.BoardSize);

        }
        
        public Tool FindTool(List<Tool> i_ToolList, Point i_ToolLocation)
        {
            Tool toolToReturn = null;

            foreach(Tool currentTool in i_ToolList)
            {
                if(currentTool.Location == i_ToolLocation)
                {
                    toolToReturn = currentTool;
                    break;
                }
            }

            return toolToReturn;

        }
    }  
}*/
