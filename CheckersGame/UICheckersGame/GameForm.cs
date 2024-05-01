using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using LogicCheckersGame;

namespace UICheckersGame
{
    // $G$ CSS-016 (-3) Bad class name - The name of classes derived from Form should start with Form.
    internal partial class GameForm : Form
    {
        private Dictionary<string, SlotButton> m_Board;
        private ToolAnimation m_CheckerMenAnimation;
        private SlotButton m_FromSlotButton;
        private SlotButton m_ToSlotButton;

        internal event ComputerPlayerEventHandler ComputerTurn;

        internal event UserMoveEventHandler UserMoveSelected;

        internal event ToolAnimationEventHandler AnimationDone;

        internal GameForm(GameBoard i_DataBoard)
        {
            m_FromSlotButton = null;
            m_ToSlotButton = null;
            ComputerTurn = null;
            UserMoveSelected = null;
            AnimationDone = null;
            InitializeComponent();
            setSize(i_DataBoard.Size);
            initializeBoard(i_DataBoard);
            initializePlayersLabel();
            initializeCheckerMenAnimation();
        }

        private void initializeCheckerMenAnimation()
        {
            m_CheckerMenAnimation = new ToolAnimation();
            m_CheckerMenAnimation.CheckerMenPicture = pictureBoxCheckerMen;
            m_CheckerMenAnimation.ToSlotPicture = pictureBoxToSlotAnimation;
            m_CheckerMenAnimation.Timer = timerCheckerMenAnimation;
            m_CheckerMenAnimation.SlotContentChanged += checkerMenAnimation_SlotContentChanged;
            m_CheckerMenAnimation.Done += checkerMenAnimation_Done;
        }

        private void checkerMenAnimation_Done(object i_Sender, EventArgs i_EventArgs)
        {
            onAnimationDone(i_EventArgs);
        }

        private void onAnimationDone(EventArgs i_EventArgs)
        {
            if(AnimationDone != null)
            {
                AnimationDone.Invoke(this, i_EventArgs);
            }
        }

        private void initializeBoard(GameBoard i_DataBoard)
        {
            int boardDataSize = i_DataBoard.Size;
            Point slotLocation = new Point(this.Left + 15, this.Top + 50);
            Point maxSlotLocation = new Point(
                this.Width - SlotButton.SlotHeightAndWidth,
                this.Height - SlotButton.SlotHeightAndWidth);
            bool slotButtonEnable = false;

            m_Board = new Dictionary<string, SlotButton>(boardDataSize * boardDataSize);
            foreach(KeyValuePair<string, BoardSlot> slotData in i_DataBoard.Dictionary)
            {
                if(slotLocation.X > maxSlotLocation.X)
                {
                    slotLocation.Y += SlotButton.SlotHeightAndWidth;
                    slotLocation.X = this.Left + 15;
                    slotButtonEnable = !slotButtonEnable;
                }

                SlotButton slotButton = new SlotButton(slotData.Value.Key, slotButtonEnable);

                slotButton.Location = slotLocation;
                slotButton.Content = slotData.Value.Content;
                slotButton.Click += this.slotButton_Click;
                this.Controls.Add(slotButton);
                m_Board.Add(slotButton.Key, slotButton);
                slotLocation.X += SlotButton.SlotHeightAndWidth;
                slotButtonEnable = !slotButtonEnable;
            }
        }

        private void initializePlayersLabel()
        {
            labelPlayer1.Location = new Point(
                (this.Width / 2) - (2 * SlotButton.SlotHeightAndWidth + 10),
                this.Top + 15);
            labelPlayer2.Location = new Point(
                (this.Width / 2) + (SlotButton.SlotHeightAndWidth) - 10,
                labelPlayer1.Top);
        }

        private void slotButton_Click(object i_Sender, EventArgs i_EventArgs)
        {
            SlotButton slotButton = i_Sender as SlotButton;

            if(slotButton.Content != null)
            {
                setFromSlotButton(slotButton);
            }
            else
            {
                if(setToSlotButton(slotButton))
                {
                    userMoveSelected();
                }
                else
                {
                    MessageBox.Show("First You Have To Choose Slot With Your Man Checker");
                    slotButton.ChangeSelectedStyle();
                }
            }
        }

        private bool setToSlotButton(SlotButton i_SlotButton)
        {
            bool setSucceeded = false;

            if(m_FromSlotButton != null)
            {
                m_ToSlotButton = i_SlotButton;
                setSucceeded = true;
            }

            return setSucceeded;
        }

        private void setFromSlotButton(SlotButton i_SelectedSlotButton)
        {
            if(i_SelectedSlotButton.Content != null)
            {
                if(m_FromSlotButton != null)
                {
                    if(i_SelectedSlotButton.Key.Equals(m_FromSlotButton.Key))
                    {
                        m_FromSlotButton = null;
                    }
                    else
                    {
                        m_FromSlotButton.ChangeSelectedStyle();
                        m_FromSlotButton = i_SelectedSlotButton;
                    }
                }
                else
                {
                    m_FromSlotButton = i_SelectedSlotButton;
                }
            }
        }

        private void setSize(int i_BoardSize)
        {
            int boardHeightAndWidth = i_BoardSize * SlotButton.SlotHeightAndWidth;

            this.Size = new Size(boardHeightAndWidth + 45, boardHeightAndWidth + 105);
        }

        private void userMoveSelected()
        {
            string fromSlotKey = m_FromSlotButton.Key;
            string toSlotKey = m_ToSlotButton.Key;

            m_FromSlotButton.ChangeSelectedStyle();
            m_ToSlotButton.ChangeSelectedStyle();
            m_FromSlotButton = null;
            m_ToSlotButton = null;
            onUserMoveSelected(new UserMoveEventArgs(fromSlotKey, toSlotKey));
        }

        private void onUserMoveSelected(UserMoveEventArgs i_EventArgs)
        {
            if(UserMoveSelected != null)
            {
                UserMoveSelected.Invoke(this, i_EventArgs);
            }
        }
        private void checkerMenAnimation_SlotContentChanged(
            object i_Sender,
            SlotContentEventArgs i_SlotContentEventArgs)
        {
            SetSlotButtonContent(i_SlotContentEventArgs.Key, i_SlotContentEventArgs.Content);
        }

        internal void SetSlotButtonsEnable(
            bool i_DisableAll,
            Tool? i_RegularCheckersMen = null,
            Tool? i_KingCheckersMen = null)
        {
            if(i_DisableAll)
            {
                disableAllSlotButtons();
            }
            else
            {
                enableSlotButtonsByCheckersMen(i_RegularCheckersMen, i_KingCheckersMen);
            }
        }

        private void enableSlotButtonsByCheckersMen(Tool? i_RegularCheckersMen, Tool? i_KingCheckersMen)
        {
            Tool regularCheckersMen;
            Tool kingCheckerMen;

            if(i_RegularCheckersMen == null || i_KingCheckersMen == null)
            {
                getFromSlotButtonCheckersMen(out regularCheckersMen, out kingCheckerMen);
            }
            else
            {
                regularCheckersMen = i_RegularCheckersMen.Value;
                kingCheckerMen = i_KingCheckersMen.Value;
            }

            foreach(KeyValuePair<string, SlotButton> keyValuePair in m_Board)
            {
                SlotButton slotButton = keyValuePair.Value;

                if(slotButton.Content != null)
                {
                    Tool.eSign slotButtonContent = slotButton.Content.Value.Sign;
                    bool enableButton = slotButtonContent == regularCheckersMen.Sign
                                        || slotButtonContent == kingCheckerMen.Sign;

                    slotButton.Enabled = enableButton;
                }
                else if(!slotButton.Name.Equals("null"))
                {
                    slotButton.Enabled = true;
                }
            }
        }

        private void disableAllSlotButtons()
        {
            foreach(KeyValuePair<string, SlotButton> keyValuePair in m_Board)
            {
                SlotButton slotButton = keyValuePair.Value;

                slotButton.Enabled = false;
            }
        }

        private void getFromSlotButtonCheckersMen(out Tool o_RegularCheckersMen, out Tool o_KingCheckersMen)
        {
            Tool fromSlotButtonCheckerMen = m_FromSlotButton.Content.Value;

            if(fromSlotButtonCheckerMen.Type == Tool.eType.Regular)
            {
                o_RegularCheckersMen = fromSlotButtonCheckerMen;
                o_KingCheckersMen = fromSlotButtonCheckerMen.CheckIfToolChangeToKing();
            }
            else
            {
                o_RegularCheckersMen = fromSlotButtonCheckerMen.CheckIfRegularTool();
                o_KingCheckersMen = fromSlotButtonCheckerMen;
            }
        }

        internal void SetSlotButtonContent(string i_SlotKey, Tool? i_Content)
        {
            m_Board[i_SlotKey].Content = i_Content;
        }

        internal void SetPlayers(string i_Player1Name, int i_Player1Score, string i_Player2Name, int i_Player2Score)
        {
            labelPlayer1.Text = string.Format("{0}: {1}", i_Player1Name, i_Player1Score);
            labelPlayer2.Text = string.Format("{0}: {1}", i_Player2Name, i_Player2Score);
        }

        internal void ChangeTurn(Tool i_RegularCheckersMen, Tool i_KingCheckersMen, bool i_PlayerComputer)
        {
            SetSlotButtonsEnable(i_PlayerComputer, i_RegularCheckersMen, i_KingCheckersMen);
            if(i_PlayerComputer)
            {
                timerComputerPlay.Start();
            }
        }

        internal void SetCheckMenAnimation(Move i_PlayerMove, bool i_ShowSelctedAnimation)
        {
            SlotButtonPlayerMove slotButtonPlayerMove;

            SetSlotButtonsEnable(true);
            if(i_PlayerMove.Type == LogicCheckersGame.Move.eMoveType.Eat)
            {
                slotButtonPlayerMove = new SlotButtonPlayerMove(
                    m_Board[i_PlayerMove.FromSlotKey],
                    m_Board[i_PlayerMove.ToSlotKey],
                    m_Board[i_PlayerMove.SlotKeyToEat]);
            }
            else
            {
                slotButtonPlayerMove = new SlotButtonPlayerMove(
                    m_Board[i_PlayerMove.FromSlotKey],
                    m_Board[i_PlayerMove.ToSlotKey]);
            }

            m_CheckerMenAnimation.SlotButtonPlayerMove = slotButtonPlayerMove;
            m_CheckerMenAnimation.ShowSelectAnimation = i_ShowSelctedAnimation;
            m_CheckerMenAnimation.Start();
        }

        private void timerComputerPlay_Tick(object sender, EventArgs e)
        {
            timerComputerPlay.Stop();
            ComputerTurn.Invoke();
        }

        internal void GameOver()
        {
            this.Visible = false;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
