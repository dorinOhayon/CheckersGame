using System;

namespace LogicCheckersGame
{
    public class PlayerMoveEventArgs : EventArgs
    {
        private readonly Move r_PlayerMove;

        public PlayerMoveEventArgs(Move i_PlayerMove)
        {
            r_PlayerMove = i_PlayerMove;
        }

        public Move PlayerMove
        {
            get
            {
                return r_PlayerMove;
            }
        }
    }
}
