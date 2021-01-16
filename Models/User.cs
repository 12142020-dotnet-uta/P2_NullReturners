using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class User
    {
        public int ID { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int TeamID { get; set; }
        public int RoleID { get; set; }
        public int PlayerPositionID { get; set; } // 0 if not a player
    }
}
