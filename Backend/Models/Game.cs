using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Game
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Game ID")]
        public int GameID { get; set; }
        [DisplayName("Home Team ID")]
        [ForeignKey("TeamID")]
        public int HomeTeamID { get; set; }
        [DisplayName("Away Team ID")]
        [ForeignKey("TeamID")]
        public int AwayTeamID { get; set; }
        [DisplayName("Game Date")]
        [DataType(DataType.DateTime)]
        public DateTime GameDate { get; set; }
        [DisplayName("Winning Team")]
        public int WinningTeam { get; set; }
        [DisplayName("Home Team Score")]
        public int HomeScore { get; set; }
        [DisplayName("Away Team Score")]
        public int AwayScore { get; set; }
        [DisplayName("Statistic 1")]
        public string Statistic1 { get; set; }
        [DisplayName("Statistic 2")]
        public string Statistic2 { get; set; }
        [DisplayName("Statistic 3")]
        public string Statistic3 { get; set; }
    }
}
