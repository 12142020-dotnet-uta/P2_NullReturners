using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class PlayerPosition
    {
        public Guid ID { get; set; }
        public string Side { get; set; } //Off / Def / ST
        public string Position { get; set; }

    }
}
