using System;
using System.Drawing;
using System.Collections.Generic;

namespace LogicCheckersGame
{
    public struct Tool
    {
        private readonly eSign r_Sign;
        private readonly eType r_Type;
        private readonly int r_Value;
        public enum eSign
        {
            X,
            O,
            K,
            U
        }

        public enum eType
        {
            Regular,
            King,
        }

        public Tool(eSign i_Sign)
        {
            r_Sign = i_Sign;
            r_Type = getType(i_Sign);
            r_Value = getValue(r_Type);
        }

        public eSign Sign
        {
            get
            {
                return r_Sign;
            }
        }

        public eType Type
        {
            get
            {
                return r_Type;
            }
        }

        public int Value
        {
            get
            {
                return r_Value;
            }
        }

        public Tool CheckIfToolChangeToKing()
        {
            Tool requireCheckerMen = this;

            if (r_Type != eType.King)
            {
                if (r_Sign == eSign.O)
                {
                    requireCheckerMen = new Tool(eSign.U);
                }
                else
                {
                    requireCheckerMen = new Tool(eSign.K);
                }
            }

            return requireCheckerMen;
        }

        public Tool CheckIfRegularTool()
        {
            Tool requireCheckerMen = this;

            if (r_Type != eType.Regular)
            {
                if (r_Sign == eSign.U)
                {
                    requireCheckerMen = new Tool(eSign.O);
                }
                else
                {
                    requireCheckerMen = new Tool(eSign.X);
                }
            }

            return requireCheckerMen;
        }

        private static eType getType(eSign i_Sign)
        {
            eType type = eType.Regular;

            if (i_Sign == eSign.U || i_Sign == eSign.K)
            {
                type = eType.King;
            }

            return type;
        }

        private static int getValue(eType i_Type)
        {
            int value = 1;

            if (i_Type == eType.King)
            {
                value = 4;
            }

            return value;
        }
    }
}
