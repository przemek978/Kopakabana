using Kopakabana;
using People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matches
{
    public abstract class Match //Ready 
    {
        protected Team T1, T2;
        protected Team WhoWon;
        protected Referee REF;
        public bool WhatSemi = false, WhatFinal = false;
        protected int Result1, Result2;

        /*public void AddWins(Team addwon)
        {
            addwon.SetWins(addwon.GetWins() + 1);
        }*/
        public abstract bool Equals(Match M);
        //Dostep do Whowon
        public virtual void SetWhoWon()
        {

            if (Result1 > Result2 && Result1 == 1)
            {
                WhoWon = T1;
                /*if (WhatFinal != true && WhatSemi != true)
                    AddWins(T1);*/
            }
            else if (Result1 < Result2 && Result2 == 1)
            {
                WhoWon = T2;
                /*if (WhatFinal != true && WhatSemi != true)
                    AddWins(T2);*/
            }
            else
                WhoWon = null;

        }
        public Team GetWhoWon()
        {
            return WhoWon;
        }
        //Dostep do druzyn
        public Team GetTeam1()
        {
            return T1;
        }
        public Team GetTeam2()
        {
            return T2;
        }
        public void SetTeam1(string name1, Player p1, Player p2, Player p3, Player p4)
        {
            T1.SetName(name1);
            T1.SetPlayers(p1, p2, p3, p4);
        }
        public void SetTeam2(string name2, Player p1, Player p2, Player p3, Player p4)
        {
            T2.SetName(name2);
            T2.SetPlayers(p1, p2, p3, p4);
        }
        //Dostep do sedziego  
        public Referee GetReferee()
        {
            return REF;
        }
        public void SetReferee(Referee main)
        {
            REF = main;
        }
        //Dostep do wyniku
        public int GetResult1()
        {
            return Result1;
        }
        public int GetResult2()
        {
            return Result2;
        }
        public void SetResult1(int score1)
        {
            Result1 = score1;
        }
        public void SetResult2(int score2)
        {
            Result2 = score2;
        }
        /////////////////////////////////////////////////////////////////////////////////////////
        public override string ToString()
        {
            return T1.GetName() + " - " + T2.GetName();
        }
    }
}
