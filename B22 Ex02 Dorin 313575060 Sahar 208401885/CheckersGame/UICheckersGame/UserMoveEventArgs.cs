using System;

namespace UICheckersGame
{
    internal class UserMoveEventArgs : EventArgs
    {
        private readonly string r_FromSlotKey;
        private readonly string r_ToSlotKey;

        internal UserMoveEventArgs(string i_FromSlotKey, string i_ToSlotKey)
        {
            r_FromSlotKey = i_FromSlotKey;
            r_ToSlotKey = i_ToSlotKey;
        }

        internal string FromSlotKey
        {
            get
            {
                return r_FromSlotKey;
            }
        }

        internal string ToSlotKey
        {
            get
            {
                return r_ToSlotKey;
            }
        }
    }
}
