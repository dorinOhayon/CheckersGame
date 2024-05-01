﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UICheckersGame
{
    // $G$ CSS-016 (-3) Bad class name - The name of classes derived from Form should start with Form.
    internal partial class GameSettings : Form
    {
        // $G$ CSS-999 (-5) This member should be readonly member
        private RadioButtonBoardSize m_BoardSize;

        internal GameSettings()
        {
            InitializeComponent();
            m_BoardSize = null;
        }

        internal int BoardSize
        {
            get
            {
                return m_BoardSize.Value;
            }
        }

        internal bool Player2IsHuman
        {
            get
            {
                return checkBoxPlayer2Human.Checked;
            }
        }

        internal string Player1
        {
            get
            {
                return textBoxPlayerName1.Value;
            }
        }

        internal string Player2
        {
            get
            {
                return textBoxPlayerName2.Value;
            }
        }

        private void buttonDone_Click(object sender, EventArgs e)
        {
            if(m_BoardSize != null)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("You Have To Choose Board Size");
            }
        }

        private void radioButtonBoardSize_CheckedChanged(object sender, EventArgs e)
        {
            RadioButtonBoardSize radioButtonBoarderSize = sender as RadioButtonBoardSize;

            if(radioButtonBoarderSize != null)
            {
                m_BoardSize = radioButtonBoarderSize;
            }
        }

        private void checkBoxPlayer2Human_CheckedStateChanged(object sender, EventArgs e)
        {
            textBoxPlayerName2.Enabled = checkBoxPlayer2Human.Checked;
        }

    }
}
