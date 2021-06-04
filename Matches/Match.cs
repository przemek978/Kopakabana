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
       
        public void AddWins(Team addwon)
        {
            addwon.setWins(addwon.getWins() + 1);
        }

        public abstract void SetWhoWon();
        public Team getWhoWon()
        {
            return WhoWon;
        }

        public Team getTeam1()
        {
            return T1;
        }
        public Team getTeam2()
        {
            return T2;
        }
        public void setTeam1(string name1, Player p1, Player p2, Player p3, Player p4)
        {
            T1.setName(name1);
            T1.setPlayers(p1, p2, p3, p4);
        }
        public void setTeam2(string name2, Player p1, Player p2, Player p3, Player p4)
        {
            T2.setName(name2);
            T2.setPlayers(p1, p2, p3, p4);
        }          
           
        public Referee GetReferee()
        {
            return REF;
        }
        public void SetReferee(Referee main)
        {
            REF = main;
        }

        public int getResult1()
        {
            return Result1;
        }
        public int getResult2()
        {
            return Result2;
        }
        public void setResult1(int score1)
        {
            Result1 = score1;
        }
        public void setResult2(int score2)
        {
            Result2 = score2;
        }

        public override string ToString()
        {
            return T1.getName() + " - " + T2.getName();
        }
    }
}
