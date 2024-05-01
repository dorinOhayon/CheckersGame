using System;
using System.Text;
using System.Windows.Forms;
using LogicCheckersGame;

namespace UICheckersGame
{
    public class WindowsFormUI
    {
        private GameForm m_GameForm;
        private GameLogicManagment m_CheckersLogic;
        private readonly GameSettings r_GameSettings;
        private DataGameOver? m_DataGameOver;

        public WindowsFormUI()
        {
            r_GameSettings = new GameSettings();
            m_CheckersLogic = null;
            m_GameForm = null;
            m_DataGameOver = null;
        }

        public void LaunchGame()
        {
            DialogResult dialogResult = r_GameSettings.ShowDialog();

            if(dialogResult == DialogResult.OK)
            {
                startGame();
            }
        }

        private void initializeGame(bool i_NewGame = true)
        {
            m_DataGameOver = null;
            initializeGameLogic(i_NewGame);
            initializeGameForm();
        }

        private void initializeGameLogic(bool i_NewGame = true)
        {
            if(i_NewGame)
            {
                string[] playerNames = { r_GameSettings.Player1, r_GameSettings.Player2 };
                GameMode.eGameMode gameMode = getGameMode();

                m_CheckersLogic = new GameLogicManagment();
                m_CheckersLogic.KingSet += gameLogicKingSet;
                m_CheckersLogic.PlayerMoveSet += checkersLogicPlayerMoveSet;
                m_CheckersLogic.TurnChanged += gameLogicTurnChanged;
                m_CheckersLogic.GameOver += checkersLogicGameOver;
                m_CheckersLogic.InitNewGame(gameMode, r_GameSettings.BoardSize, playerNames);
            }
            else
            {
                m_CheckersLogic.InitNewGame();
            }
        }

        private void checkersLogicPlayerMoveSet(object i_Sender, PlayerMoveEventArgs i_PlayerMoveEventArgsEventArgs)
        {
            bool computerPlayer = m_CheckersLogic.PlayerTurn.Type == Player.eType.Computer;

            m_GameForm.SetCheckMenAnimation(i_PlayerMoveEventArgsEventArgs.PlayerMove, computerPlayer);
        }

        private void checkersLogicGameOver(object i_Sender, GameStatusEventArgs i_GameStatusEventArgs)
        {
            if(i_GameStatusEventArgs.DataGameOver != null)
            {
                m_DataGameOver = i_GameStatusEventArgs.DataGameOver;
            }

            m_GameForm.GameOver();
        }

        private void showMessageBoxGameOver(DataGameOver i_DataGameOver)
        {
            StringBuilder messageBoxText = new StringBuilder();

            if(i_DataGameOver.Draw)
            {
                messageBoxText.AppendLine("Tie!");
            }
            else
            {
                messageBoxText.AppendFormat("{0} Won!", i_DataGameOver.WinnerName).AppendLine();
            }

            messageBoxText.Append("Another Round?");
            DialogResult dialogResult = MessageBox.Show(
                messageBoxText.ToString(),
                "Damka",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            playAgain(dialogResult);
        }

        private void initializeGameForm()
        {
            m_GameForm = new GameForm(m_CheckersLogic.Board);
            m_GameForm.ComputerTurn += gameFormComputerTurn;
            m_GameForm.UserMoveSelected += gameFormUserMoveSelected;
            m_GameForm.AnimationDone += gameFormAnimationDone;
            setPlayersLabel();
            setPlayerTurnSlotButtonsEnable(
                m_CheckersLogic.PlayerTurn.RegularCheckersMen,
                m_CheckersLogic.PlayerTurn.KingCheckersMen);
        }

        private void gameFormAnimationDone(object i_Sender, EventArgs i_EventArgs)
        {
            m_CheckersLogic.EndTurn();
        }

        private void setPlayerQuitDataGameOver()
        {
            int score = m_CheckersLogic.PlayerEnemy.Score - m_CheckersLogic.PlayerTurn.Score;
            m_CheckersLogic.PlayerEnemy.Score += Math.Abs(score);
            m_DataGameOver = new DataGameOver(m_CheckersLogic.PlayerEnemy.Name, m_CheckersLogic.PlayerTurn.Name, m_CheckersLogic.PlayerEnemy.Score, false, true);
        }

        private void gameFormComputerTurn()
        {
            computerPlayerPlay();
        }

        private void gameFormUserMoveSelected(object i_Sender, UserMoveEventArgs i_UserMoveEventArgs)
        {
            humanPlayerPlay(i_UserMoveEventArgs.FromSlotKey, i_UserMoveEventArgs.ToSlotKey);
        }

        private void computerPlayerPlay()
        {
            m_CheckersLogic.ComputerPlay();
        }

        private void gameLogicTurnChanged(TurnEventsArgs i_TurnEventArgs)
        {
            m_GameForm.ChangeTurn(i_TurnEventArgs.RegularCheckersMen, i_TurnEventArgs.KingCheckersMen, i_TurnEventArgs.PlayerTurnComputer);
        }

        private void humanPlayerPlay(string i_FromSlotKey, string i_ToSlotKey)
        {
            try
            {
                m_CheckersLogic.ProcessUserMove(i_FromSlotKey, i_ToSlotKey);
            }
            catch(Exception i_Exception)
            {
                MessageBox.Show(i_Exception.ToString());
            }
        }

        private void gameLogicKingSet(object i_Sender, SlotContentEventArgs i_SlotContentEventArgs)
        {
            m_GameForm.SetSlotButtonContent(i_SlotContentEventArgs.Key, i_SlotContentEventArgs.Content);
        }

        private void playAgain(DialogResult i_DialogResult)
        {
            if(i_DialogResult == DialogResult.Yes)
            {
                startGame(false);
            }
        }

        private void startGame(bool i_NewGame = true)
        {
            initializeGame(i_NewGame);
            displayGame();
        }

        private void setPlayersLabel()
        {
            if(m_CheckersLogic.PlayerTurn.PlayerNumber == 1)
            {
                m_GameForm.SetPlayers(m_CheckersLogic.PlayerTurn.Name, m_CheckersLogic.PlayerTurn.Score, m_CheckersLogic.PlayerEnemy.Name, m_CheckersLogic.PlayerEnemy.Score);
            }
            else
            {
                m_GameForm.SetPlayers(m_CheckersLogic.PlayerEnemy.Name, m_CheckersLogic.PlayerEnemy.Score, m_CheckersLogic.PlayerTurn.Name, m_CheckersLogic.PlayerTurn.Score);
            }
        }

        private void setPlayerTurnSlotButtonsEnable(Tool i_PlayerTurnRegularCheckersMen, Tool i_PlayerTurnKingCheckersMen)
        {
            bool playerTurnIsComputer = m_CheckersLogic.PlayerTurn.Type == Player.eType.Computer;

            m_GameForm.SetSlotButtonsEnable(playerTurnIsComputer, i_PlayerTurnRegularCheckersMen, i_PlayerTurnKingCheckersMen);
        }

        private void displayGame()
        {
            m_GameForm.ShowDialog();
            if(m_DataGameOver == null)
            {
                setPlayerQuitDataGameOver();
            }

            showMessageBoxGameOver(m_DataGameOver.Value);
        }

        private GameMode.eGameMode getGameMode()
        {
            return (r_GameSettings.Player2IsHuman ? GameMode.eGameMode.HumanVsHuman : GameMode.eGameMode.HumanVsComputer);
        }
    }
}