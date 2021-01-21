using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class UserInbox
    {
        [DisplayName("User ID")]
        [ForeignKey("UserID")]
        public Guid UserID { get; set; }
        [DisplayName("Message ID")]
        [ForeignKey("MessageID")]
        public Guid MessageID { get; set; }
        public bool IsRead { get; set; }
    }
}
