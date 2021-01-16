using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class IRList
    {
        public Guid ID { get; set; }
        public Guid TeamID { get; set; }
        public Guid PlayerID { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
    }
}
