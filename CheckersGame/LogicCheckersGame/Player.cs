﻿using System;
using System.Drawing;
using System.Collections.Generic;

namespace LogicCheckersGame
{
    public class Player
    {
        private readonly string r_Name;
        private readonly int r_PlayerNumber;
        private readonly eType r_Type;
        private readonly Tool r_RegularCheckersMen;
        private readonly Tool r_KingCheckersMen;
        private readonly Direction r_Direction;
        private readonly Random r_RandomNumberGenerator;
        private readonly List<Move> r_PossibleMoves;
        private readonly List<Move> r_LastMovesPlayed;
        private readonly char r_BaseLineRowKey;
        private bool m_CanEatAgain;
        private int m_AmountOfMenOnBoard;
        private int m_Score;

        public enum eType
        {
            Human,
            Computer
        }

        public Player(string i_Name, int i_PlayerNumber, eType i_Type, Tool.eSign i_RegularSign, Tool.eSign i_KingSign, char i_BaseLineRowKey, int i_m_AmountOfMenOnBoard)
        {
            r_Name = i_Name;
            r_Type = i_Type;
            r_PlayerNumber = i_PlayerNumber;
            r_RegularCheckersMen = new Tool(i_RegularSign);
            r_KingCheckersMen = new Tool(i_KingSign);
            r_PossibleMoves = new List<Move>(0);
            r_LastMovesPlayed = new List<Move>(0);
            r_RandomNumberGenerator = new Random();
            m_CanEatAgain = false;
            r_BaseLineRowKey = i_BaseLineRowKey;
            m_AmountOfMenOnBoard = i_m_AmountOfMenOnBoard;
            m_Score = 0;

            if (i_RegularSign == Tool.eSign.O)
            {
                r_Direction = new Direction(1, -1, 1, -1);
            }
            else
            {
                r_Direction = new Direction(-1, 1, -1, 1);
            }
        }

        public int PlayerNumber
        {
            get
            {
                return r_PlayerNumber;
            }
        }

        public int Score
        {
            get
            {
                return m_Score;
            }
            set
            {
                m_Score = value;
            }
        }

        public List<Move> PossibleMoves
        {
            get
            {
                return r_PossibleMoves;
            }
        }

        public List<Move> LastMovesPlayed
        {
            get
            {
                return r_LastMovesPlayed;
            }
        }

        public char BaseLineRowKey
        {
            get
            {
                return r_BaseLineRowKey;
            }
        }

        public bool CanEatAgain
        {
            get
            {
                return m_CanEatAgain;
            }
            set
            {
                m_CanEatAgain = value;
            }
        }

        public string Name
        {
            get
            {
                return r_Name;
            }
        }

        public eType Type
        {
            get
            {
                return r_Type;
            }
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

        public int AmountOfMenOnBoard
        {
            get
            {
                return m_AmountOfMenOnBoard;
            }
            set
            {
                m_AmountOfMenOnBoard = value;
            }
        }

        public Direction Direction
        {
            get
            {
                return r_Direction;
            }
        }

        public void AddPossibleMoves(List<Move> i_PossibleMoves)
        {
            foreach (Move possibleMove in i_PossibleMoves)
            {
                r_PossibleMoves.Add(possibleMove);
            }
        }

        public void ClearPossibleMoves()
        {
            r_PossibleMoves.Clear();
        }

        public void ClearLastMovesPlayed()
        {
            r_LastMovesPlayed.Clear();
        }

        public void AddLastMovePlayed(Move i_PlayerMove)
        {
            r_LastMovesPlayed.Add(i_PlayerMove);
        }

        internal int GetIndexOfRandomPlayMove()
        {
            return r_RandomNumberGenerator.Next((r_PossibleMoves.Count));
        }
    }
}
