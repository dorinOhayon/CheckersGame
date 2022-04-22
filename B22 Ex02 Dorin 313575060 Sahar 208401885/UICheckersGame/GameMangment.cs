using System;
using LogicCheckersGame;

namespace UICheckersGame
{
    public class GameMangment
    {
        private Game m_GameDetails = new Game();

        public void RunGame()
        {
            InitPlayerName(m_GameDetails.CurretntPlayer);
            InitBoardSize();
            InitSecondPlayer();
            PrintBoard();
        }
        private void InitPlayerName(Player i_Player)
        {
            string playerName;
            bool isValidName;
            Console.WriteLine("Please enter player name(maximum 20 chars, without spaces) :");
            playerName = Console.ReadLine();

            isValidName = i_Player.CheckIfValidName(playerName);
            while(!isValidName)
            {
                Console.WriteLine("Invalid name! Please enter a valid name :");
                playerName = Console.ReadLine();
                isValidName = i_Player.CheckIfValidName(playerName);
            }

            i_Player.PlayerName = playerName;

        }

        private void InitBoardSize()
        {
            string sizeInput;
            int validSize;
            bool isValidSize = false;

            Console.WriteLine("Please choose board size (6 or 8 or 10) :");
            sizeInput = Console.ReadLine();
            isValidSize = m_GameDetails.Board.CheckIfValidBoardSize(sizeInput, out validSize);
            while(!isValidSize)
            {
                Console.WriteLine("Invalid size! Please choose again :");
                sizeInput = Console.ReadLine();
                isValidSize = m_GameDetails.Board.CheckIfValidBoardSize(sizeInput, out validSize);
            }

            m_GameDetails.Board.BoardSize = validSize;
            m_GameDetails.Board.CreateGameBoard();
            m_GameDetails.Board.InitPlayerToolsOnBoard(m_GameDetails.CurretntPlayer, 1);
            m_GameDetails.Board.InitPlayerToolsOnBoard(m_GameDetails.NextPlayer, 2);

        }

        private void InitSecondPlayer()
        {
            string playerChoice;
            Console.WriteLine("Please choose your rival :"+ Environment.NewLine+ "1. PC"+ Environment.NewLine +"2. human player");
            playerChoice = Console.ReadLine();

            if((int.Parse(playerChoice) == 1))
            {
                m_GameDetails.NextPlayer.IsPc = true;
            }

            else
            {
                m_GameDetails.NextPlayer.IsPc = false;
                InitPlayerName(m_GameDetails.NextPlayer);
            }

        }

        private void PrintBoard()
        {
            Ex02.ConsoleUtils.Screen.Clear();
            string stringToPrint = String.Empty;
            int i, j;
            for( i=0; i< m_GameDetails.Board.BoardSize; i++)
            {
                stringToPrint+= String.Format("   {0}", (char)(i + 65));             
            }

            Console.WriteLine(stringToPrint);
            stringToPrint = String.Empty;
            for (i = 0; i < m_GameDetails.Board.BoardSize*4 +2; i++)
            {
                stringToPrint += "=";           
            }

            Console.WriteLine(stringToPrint);
            stringToPrint = String.Empty;
            for (i = 0; i < m_GameDetails.Board.BoardSize; i++)
            {
                stringToPrint += String.Format("{0}|", (char)(i + 97));
                for ( j = 0; j < m_GameDetails.Board.BoardSize; j++)
                {
                    stringToPrint += String.Format(" {0} |", m_GameDetails.Board[i, j]);
                }

                Console.WriteLine(stringToPrint);
                stringToPrint = String.Empty;
                for (j = 0; j < m_GameDetails.Board.BoardSize * 4 +2; j++)
                {
                    stringToPrint += "=";
                }

                Console.WriteLine(stringToPrint);
                stringToPrint = String.Empty;
            }

        }
    }

}
