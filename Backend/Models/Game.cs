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
        public int ID { get; set; }
        [DisplayName("Home Team ID")]
        public int HomeTeamID { get; set; }
        [DisplayName("Away Team ID")]
        public int AwayTeamID { get; set; }
        [DisplayName("Winning Team")]
        public int WinningTeam { get; set; }
        [DisplayName("Home Team Score")]
        public int HomeScore { get; set; }
        [DisplayName("Away Team Score")]
        public int AwayScore { get; set; }
    }
}
