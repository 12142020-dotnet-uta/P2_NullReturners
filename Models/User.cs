using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class User
    {
        public Guid ID { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public Guid TeamID { get; set; }
        public int RoleID { get; set; }
        public Guid PlayerPositionID { get; set; } // 0 if not a player
    }
}
