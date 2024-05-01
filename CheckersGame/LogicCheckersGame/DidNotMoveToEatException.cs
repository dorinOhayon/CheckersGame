using System;

namespace LogicCheckersGame
{
    public class DidNotMoveToEatException : Exception
    {
        public override string ToString()
        {
            return "You must pick a move that eat enemy!";
        }
    }
}
