using System;

namespace LogicCheckersGame
{
    public class SlotContentEventArgs : EventArgs
    {
        private readonly string r_Key;
        private readonly Tool? r_Content;

        public SlotContentEventArgs(string i_SlotKey, Tool? i_SlotContent)
        {
            r_Content = i_SlotContent;
            r_Key = i_SlotKey;
        }

        public string Key
        {
            get
            {
                return r_Key;
            }
        }

        public Tool? Content
        {
            get
            {
                return r_Content;
            }
        }
    }
}
