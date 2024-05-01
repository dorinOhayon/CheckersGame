using System;

namespace UICheckersGame
{
    internal class ToolAnimationSelectedEventArgs : EventArgs
    {
        private readonly string r_SlotKey;

        internal ToolAnimationSelectedEventArgs(string i_SlotKey)
        {
            r_SlotKey = i_SlotKey;
        }

        internal string SlotKey
        {
            get
            {
                return r_SlotKey;
            }
        }
    }
}
