using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Team
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string NickName { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
    }
}
