using System;

namespace LogicCheckersGame
{
    public class GameStatusEventArgs : EventArgs
    {
        private readonly object r_Data;

        public GameStatusEventArgs() : base()
        {
            r_Data = null;
        }

        public GameStatusEventArgs(DataGameOver i_DataGameOver)
        {
            r_Data = i_DataGameOver;
        }

        public DataGameOver? DataGameOver
        {
            get
            {
                return r_Data as DataGameOver?;
            }
        }

        public object Data
        {
            get
            {
                return r_Data;
            }
        }
    }
}
