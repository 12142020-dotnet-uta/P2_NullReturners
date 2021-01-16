using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class EquipmentRequest
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int TeamID { get; set; }
        public DateTime RequestDate { get; set; }
        public string Description { get; set; }
    }
}
