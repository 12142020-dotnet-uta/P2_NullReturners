using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DataTransfer
{
    public class EditTeamDto
    {
        [DisplayName("Team Name")]
        public string Name { get; set; }
        [DisplayName("Wins")]
        public int Wins { get; set; }
        [DisplayName("Losses")]
        public int Losses { get; set; }
    }
}
