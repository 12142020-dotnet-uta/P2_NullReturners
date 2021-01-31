using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DataTransfer
{
    public class CreateGameDto
    {
        [DisplayName("Game Date")]
        [DataType(DataType.DateTime)]
        public DateTime GameDate { get; set; }
        [DisplayName("Home Team ID")]
        public int HomeTeamID { get; set; }
        [DisplayName("Away Team ID")]
        public int AwayTeamID { get; set; }
        [DisplayName("Statistic 1")]
        public string Statistic1 { get; set; }
        [DisplayName("Statistic 2")]
        public string Statistic2 { get; set; }
        [DisplayName("Statistic 3")]
        public string Statistic3 { get; set; }
    }
}
