using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DataTransfer
{
    public class CreateGameDto
    {
        [DisplayName("Home Team ID")]
        public int HomeTeamID { get; set; }
        [DisplayName("Away Team ID")]
        public int AwayTeamID { get; set; }
    }
}
