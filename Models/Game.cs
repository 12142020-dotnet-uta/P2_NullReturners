using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Game
    {
        public Guid ID { get; set; }
        public Guid HomeTeamID { get; set; }
        public Guid AwayTeamID { get; set; }
        public Guid WinningTeam { get; set; }
        public int HomeScore { get; set; }
        public int AwayScore { get; set; }
    }
}
