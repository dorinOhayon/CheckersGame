using System;

namespace LogicCheckersGame
{
    public class GameMode
    {
        public enum eGameMode
        {
            HumanVsHuman = 1,
            HumanVsComputer
        }

        private static bool checkIfNumberInRange(int i_Number)
        {
            return i_Number > 0 && i_Number < 3;
        }

        public static bool TryParse(string i_StrNum, out eGameMode? o_GameMode)
        {
            o_GameMode = null;
            bool parseSucceeded = int.TryParse(i_StrNum, out int num);

            if(parseSucceeded && checkIfNumberInRange(num))
            {
                o_GameMode = (eGameMode)num;
            }
            else
            {
                parseSucceeded = false;
            }

            return parseSucceeded;
        }
    }
}
