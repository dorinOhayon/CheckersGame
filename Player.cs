using System;
using System.Collections.Generic;

namespace LogicCheckersGame
{
    public class Player
    {
        private string m_PlayerName;
        private int m_Score;
        private char m_PlayerToolSign;
        private bool m_IsPc;
        private List<Tool> m_ToolsList = new List<Tool>();

        public string PlayerName
        {
            get
            {
                return m_PlayerName;
            }

            set
            {
                m_PlayerName = value;
            }
        }

        public int Score
        {
            get
            {
                return m_Score;
            }

            set
            {
                m_Score = value;
            }
        }

        public char PlayerToolSign
        {
            get
            {
                return m_PlayerToolSign;
            }

            set
            {
                m_PlayerToolSign = value;
            }
        }

        public bool IsPc
        {
            get
            {
                return m_IsPc;
            }

            set
            {
                m_IsPc = value;
            }
        }

        public List<Tool> ToolList
        {
            get
            {
                return m_ToolsList;
            }

            set
            {
                m_ToolsList = value;
            }
        }

        public void initValidMoveListForTool(GameBoard i_Board)
        {
            foreach (Tool cureentTool in m_ToolsList)
            {
                cureentTool.UpdateValidMoveList(i_Board);
            }
        }

        public bool CheckIfValidName(string i_UserNameInput)
        {
            bool isValid = true;

            if (i_UserNameInput.Contains(" ") || i_UserNameInput.Length > 20)
            {
                isValid = false;
            }

            return isValid;
        }

        public void SetTypeOfPlayer(int i_UserTypeOfPlayer)//איפה אנחנו משתמשות בזה??
        {
            bool isPc;

            isPc = (i_UserTypeOfPlayer == 1);
            /*if(i_UserTypeOfPlayer == 1)
            {
                isPc = true;
            }*/

            this.IsPc = isPc;
        }

        public void UpdateScore(Player i_PlayerLose)
        {
            m_Score = this.CalculatePlayerScore() - i_PlayerLose.CalculatePlayerScore();
        }

        private int CalculatePlayerScore()
        {
            int score = 0;

            foreach (Tool currentTool in m_ToolsList)
            {
                if (currentTool.IsKing)
                {
                    score += 4;
                }
                else
                {
                    score += 1;
                }
            }

            return score;
        }

        public Move GetRandomMoveForPc()
        {
            Move pcMove;
            Random random = new Random();
            int randomToolIndex, randomMoveIndex;

            randomToolIndex = random.Next(this.ToolList.Count);
            while (ToolList[randomToolIndex].ValidMoveList.Count == 0)
            {
                randomToolIndex = random.Next(this.ToolList.Count);
            }

            randomMoveIndex = random.Next(ToolList[randomToolIndex].ValidMoveList.Count);
            pcMove = ToolList[randomToolIndex].ValidMoveList[randomMoveIndex];

            return pcMove;
        }

        public void ResetPlayerTool()
        {
            foreach (Tool currentTool in m_ToolsList)
            {
                currentTool.Sign = m_PlayerToolSign;
            }
        }
    }
}


/*
namespace LogicCheckersGame
{
    public class Player
    {
        private string m_PlayerName;
        private int m_Score;
        private char m_PlayerToolSign;
        private bool m_IsPc;
        private List<Tool> m_ToolsList = new List<Tool>();

        public string PlayerName
        {
            get
            {
                return m_PlayerName;
            }

            set
            {
                m_PlayerName = value;
            }
        }

        public int Score
        {
            get
            {
                return m_Score;
            }

            set
            {
                m_Score = value;
            }
        }
        public char PlayerToolSign
        {
            get
            {
                return m_PlayerToolSign;
            }

            set
            {
                m_PlayerToolSign = value;
            }
        }
        public bool IsPc
        {
            get
            {
                return m_IsPc;
            }

            set
            {
                m_IsPc = value;
            }
        }

        public List<Tool> ToolList
        {
            get
            {
                return m_ToolsList;
            }

            set
            {
                m_ToolsList = value;
            }
        }

        public void initValidMoveListForTool(GameBoard i_Board)
        {
            foreach(Tool cureentTool in m_ToolsList)
            {
                cureentTool.UpdateValidMoveList(i_Board);
            }
        }
        public bool CheckIfValidName(string i_UserNameInput)
        {
            bool isValid = false;

            if(!(i_UserNameInput.Contains(" ")) && i_UserNameInput.Length <= 20)
            {
                isValid = true;
            }

            return isValid;
        }

        public void SetTypeOfPlayer(int i_UserTypeOfPlayer)
        {
            bool isPc = false;

            if(i_UserTypeOfPlayer == 1)
            {
                isPc = true;
            }

            this.IsPc = isPc;
        }

        public void UpdateScore(Player i_PlayerLose)
        {
             m_Score = this.CalculatePlayerScore() - i_PlayerLose.CalculatePlayerScore();       
        }

        private int CalculatePlayerScore()
        {
            int score = 0;
            foreach (Tool currentTool in m_ToolsList)
            {
                if (currentTool.IsKing)
                {
                    score += 4;
                }

                else
                {
                    score += 1;
                }
            }

            return score;
        }

        public Move GetRandomMoveForPc()
        {
            Move pcMove;
            Random random = new Random();

            int randomToolIndex = random.Next(this.ToolList.Count);
            while (ToolList[randomToolIndex].ValidMoveList.Count == 0)
            {
                randomToolIndex = random.Next(this.ToolList.Count);
            }

            int randomMoveIndex = random.Next(ToolList[randomToolIndex].ValidMoveList.Count);

            pcMove = ToolList[randomToolIndex].ValidMoveList[randomMoveIndex];
            return pcMove;

        }

        public void ResetPlayerTool()
        {
            foreach(Tool currentTool in m_ToolsList)
            {
                currentTool.Sign = m_PlayerToolSign;
            }
            
        }

    }
}*/
