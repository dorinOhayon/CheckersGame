using System;

namespace LogicCheckersGame
{
    public class SlotKeyIsNotInRangeException : Exception
    {
        private readonly string r_SlotKey;

        public SlotKeyIsNotInRangeException(string i_SlotKey)
        {
            r_SlotKey = i_SlotKey;
        }

        public string SlotKey
        {
            get
            {
                return r_SlotKey;
            }
        }

        public override string ToString()
        {
            return string.Format("Index: {0} is not in board rang", r_SlotKey);
        }
    }
}
