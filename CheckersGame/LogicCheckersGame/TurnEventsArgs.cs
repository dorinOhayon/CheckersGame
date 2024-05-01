using System;

namespace LogicCheckersGame
{
    public class TurnEventsArgs : EventArgs
    {
        private readonly Tool r_RegularCheckersMen;
        private readonly Tool r_KingCheckersMen;
        private readonly bool r_PlayerTurnComputer;

        public TurnEventsArgs(Tool i_RegularCheckersMen, Tool i_KingCheckersMen, bool i_PlayerTurnComputer)
        {
            r_RegularCheckersMen = i_RegularCheckersMen;
            r_KingCheckersMen = i_KingCheckersMen;
            r_PlayerTurnComputer = i_PlayerTurnComputer;
        }

        public Tool RegularCheckersMen
        {
            get
            {
                return r_RegularCheckersMen;
            }
        }

        public Tool KingCheckersMen
        {
            get
            {
                return r_KingCheckersMen;
            }
        }

        public bool PlayerTurnComputer
        {
            get
            {
                return r_PlayerTurnComputer;
            }
        }
    }
}
