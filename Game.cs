using System;

namespace LogicCheckersGame
{
    public class Game
    {
        private Player m_CurrentPlayer;
        private Player m_NextPlayer;
        private GameBoard m_Board;

        public Game()
        {
            m_CurrentPlayer = new Player();
            m_NextPlayer = new Player();
            m_Board = new GameBoard();
            m_CurrentPlayer.PlayerToolSign = (char)Tool.eSigns.PlayerO;
            m_NextPlayer.PlayerToolSign = (char)Tool.eSigns.PlayerX;
            m_CurrentPlayer.IsPc = false;
        }

        public enum eGameResult
        {
            PlayerOWin = 1,
            PlayerXWin = 2,
            Tie = 3,
            Unknown = 4
        }

        public Player CurretntPlayer
        {
            get
            {
                return m_CurrentPlayer;
            }

            set
            {
                m_CurrentPlayer = value;
            }
        }

        public Player NextPlayer
        {
            get
            {
                return m_NextPlayer;
            }

            set
            {
                m_NextPlayer = value;
            }
        }

        public GameBoard Board
        {
            get
            {
                return m_Board;
            }
        }

        public void ApplyMoveInGame(Move i_nextMove)
        {
            //bool isValidMove = true; לא באמת השתמשנו בו
            eGameResult gameResult;

            CurretntPlayer.initValidMoveListForTool(m_Board);
            Tool toolToMove = i_nextMove.FindTool(CurretntPlayer.ToolList, i_nextMove.CurrentLocation);
            if (toolToMove != null)
            {
                int direction = toolToMove.GetDirection();
                i_nextMove.UpdateMove(CurretntPlayer.ToolList, NextPlayer.ToolList, m_Board, direction, (int)Tool.eDirection.Left);
                i_nextMove.UpdateMove(CurretntPlayer.ToolList, NextPlayer.ToolList, m_Board, direction, (int)Tool.eDirection.Right);
                if (i_nextMove.IsSkip)
                {
                    Console.WriteLine("Skip move - ");
                    // להוציא לםונקציה, פלוס עדכון שהתור נשאר אצל השחקן הנוכחי
                }
                else
                {
                    ChangePlayerTurn();
                }

            }
            else
            {  ///// להומיא לפונקציה בUI
                Console.WriteLine("invalid move! please enter a valid move :");
            }

            if (CheckIfGameOver(out gameResult))
            {
                Console.WriteLine("Another rund? :");
                // להוציא לפונקציה ששואלתאם להתחיל משחק חדש וממנה לאתחל את המשחק חוץ מאת הניקוד
            }
        }

        public bool CheckIfGameOver(out eGameResult o_GameResult)
        {
            bool isGameOver = true;

            o_GameResult = eGameResult.Unknown;
            if (NextPlayer.ToolList.Count == 0 || !CheckIfPlayerCanMove(NextPlayer))
            {
                if (CurretntPlayer.PlayerToolSign == (char)Tool.eSigns.PlayerO)
                {
                    o_GameResult = eGameResult.PlayerOWin;

                }
                else
                {
                    o_GameResult = eGameResult.PlayerXWin;
                }

                CurretntPlayer.UpdateScore(NextPlayer);
                //לקרוא לפונקציה שמציגה את הנקודות ושם המנצח
            }
            else if (!CheckIfPlayerCanMove(CurretntPlayer) && !CheckIfPlayerCanMove(NextPlayer))
            {
                isGameOver = true;
                o_GameResult = eGameResult.Tie;
            }

            return isGameOver;
        }

        public bool CheckIfPlayerCanMove(Player i_currentPlayer)
        {
            bool isValidMoveLeft = true;

            foreach (Tool currentTool in i_currentPlayer.ToolList)
            {
                if (currentTool.ValidMoveList.Count == 0)
                {
                    isValidMoveLeft = false;
                    break;
                }
            }

            return isValidMoveLeft;
        }

        private void ChangePlayerTurn()
        {
            Player tempPlayer = CurretntPlayer;
            CurretntPlayer = NextPlayer;
            NextPlayer = tempPlayer;
        }

        public void InitNewGame()
        {
            m_Board.initMiddleRowsOnBoard();
            if (m_CurrentPlayer.PlayerToolSign != (char)Tool.eSigns.PlayerO)
            {
                ChangePlayerTurn();
            }

            m_CurrentPlayer.ResetPlayerTool();
            m_NextPlayer.ResetPlayerTool();
            m_Board.InitPlayerToolsOnBoard(m_CurrentPlayer, 1);
            m_Board.InitPlayerToolsOnBoard(m_NextPlayer, 2);
            m_CurrentPlayer.initValidMoveListForTool(m_Board);
            m_NextPlayer.initValidMoveListForTool(m_Board);
        }
    }
}



/*
namespace LogicCheckersGame
{
    public class Game
    {
        private Player m_CurrentPlayer;
        private Player m_NextPlayer;
        private GameBoard m_Board;      

        public Game()
        {
            m_CurrentPlayer = new Player();
            m_NextPlayer = new Player();
            m_Board = new GameBoard();
            m_CurrentPlayer.PlayerToolSign = (char)Tool.eSigns.PlayerO;
            m_NextPlayer.PlayerToolSign = (char)Tool.eSigns.PlayerX;
            m_CurrentPlayer.IsPc = false;

        }
        public enum eGameResult
        {
            PlayerOWin = 1,
            PlayerXWin = 2,
            Tie = 3,
            Unknown = 4
        }
        public Player CurretntPlayer
        {
            get
            {
                return m_CurrentPlayer;
            }

            set
            {
                m_CurrentPlayer = value;
            }
        }
        public Player NextPlayer
        {
            get
            {
                return m_NextPlayer;
            }

            set
            {
                m_NextPlayer = value;
            }
        }

        public GameBoard Board
        {
            get
            {
                return m_Board;
            }

        }

        public void ApplyMoveInGame(Move i_nextMove)
        {
            bool isValidMove = false;
            eGameResult gameResult;

            CurretntPlayer.initValidMoveListForTool(m_Board);
            Tool toolToMove = i_nextMove.FindTool(CurretntPlayer.ToolList, i_nextMove.CurrentLocation);

            if(toolToMove != null)
            {
                isValidMove = true;
                int direction = toolToMove.GetDirection();
                i_nextMove.UpdateMove(CurretntPlayer.ToolList, NextPlayer.ToolList, m_Board, direction, (int)Tool.eDirection.Left);
                i_nextMove.UpdateMove(CurretntPlayer.ToolList, NextPlayer.ToolList, m_Board, direction, (int)Tool.eDirection.Right);

                if(i_nextMove.IsSkip)
                {
                    Console.WriteLine("Skip move - ");
                    // להוציא לםונקציה, פלוס עדכון שהתור נשאר אצל השחקן הנוכחי
                }

                else
                {
                    ChangePlayersTurn();
                }

            }

            else
            {  ///// להומיא לפונקציה בUI
                Console.WriteLine("invalid move! please enter a valid move :");
            }

            if(CheckIfGameOver(out gameResult))
            {
                Console.WriteLine("Another rund? :");
                // להוציא לפונקציה ששואלתאם להתחיל משחק חדש וממנה לאתחל את המשחק חוץ מאת הניקוד
            }
        }

        public bool CheckIfGameOver(out eGameResult o_GameResult)
        {
            bool isGameOver = false;
            o_GameResult = eGameResult.Unknown;

            if (NextPlayer.ToolList.Count == 0 || !CheckIfPlayerCanMove(NextPlayer))
            {
                isGameOver = true;

                if(CurretntPlayer.PlayerToolSign == (char)Tool. eSigns.PlayerO)
                {
                    o_GameResult = eGameResult.PlayerOWin;

                }
                else
                {
                    o_GameResult = eGameResult.PlayerXWin;
                }

                CurretntPlayer.UpdateScore(NextPlayer);
                //לקרוא לפונקציה שמציגה את הנדות ושם המנצח
            }

            else if(!CheckIfPlayerCanMove(CurretntPlayer) && !CheckIfPlayerCanMove(NextPlayer))
            {
                isGameOver = true;
                o_GameResult = eGameResult.Tie;
            }

            return isGameOver;
        }

        public bool CheckIfPlayerCanMove(Player i_currentPlayer)
        {
            bool isValidMoveLeft = false;

            foreach(Tool currentTool in i_currentPlayer.ToolList)
            {
                if(currentTool.ValidMoveList.Count != 0)
                {
                    isValidMoveLeft = true;
                    break;
                }

            }

            return isValidMoveLeft;
        }

        private void ChangePlayersTurn()
        {
            Player tempPlayer = CurretntPlayer;
            CurretntPlayer = NextPlayer;
            NextPlayer = tempPlayer;
        }

        public void InitNewGame()
        {
            m_Board.initMiddleRowsOnBoard();
            if(m_CurrentPlayer.PlayerToolSign != (char)Tool.eSigns.PlayerO)
            {
                ChangePlayersTurn();
            }

            m_CurrentPlayer.ResetPlayerTool();
            m_NextPlayer.ResetPlayerTool();
            m_Board.InitPlayerToolsOnBoard(m_CurrentPlayer, 1);
            m_Board.InitPlayerToolsOnBoard(m_NextPlayer, 2);
            m_CurrentPlayer.initValidMoveListForTool(m_Board);
            m_NextPlayer.initValidMoveListForTool(m_Board);

        }        
    }
}
*/
