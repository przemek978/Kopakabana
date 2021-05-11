using Matches;
using People;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tournament
{
    class Tournament
    {
        private List<Referee> Referees;
        private List<Team>  Teams;
        private List<Match> Matches;
        public Tournament()
        {
            StreamReader Ref= new StreamReader("Referees.txt");
            StreamReader T = new StreamReader("Teams.txt");
            StreamReader Mat = new StreamReader("Matches.txt");
        }
    }
}
