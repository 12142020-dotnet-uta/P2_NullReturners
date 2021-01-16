using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Game
    {
        public int ID { get; set; }
        public int HomeTeamID { get; set; }
        public int AwayTeamID { get; set; }
        public int WinningTeam { get; set; }
        public int HomeScore { get; set; }
        public int AwayScore { get; set; }
    }
}
