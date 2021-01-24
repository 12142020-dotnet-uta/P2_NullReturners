using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DataTransfer
{
    public class EditGameDto
    {
        [DisplayName("Winning Team ID")]
        public int WinningTeamID { get; set; }
        [DisplayName("Home Score")]
        public int HomeScore { get; set; }
        [DisplayName("Away Score")]
        public int AwayScore { get; set; }
        [DisplayName("Statistic 1")]
        public string Statistic1 { get; set; }
        [DisplayName("Statistic 2")]
        public string Statistic2 { get; set; }
        [DisplayName("Statistic 3")]
        public string Statistic3 { get; set; }
    }
}
