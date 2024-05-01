using System;

namespace LogicCheckersGame
{
    public class PlayerMustToEatAgainException : Exception
    {
        public override string ToString()
        {
            return "You must eat again!";
        }
    }
}
