using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    class UserInbox
    {
        [DisplayName("User ID")]
        [ForeignKey("UserID")]
        public Guid UserID { get; set; }
        [DisplayName("User ID")]
        [ForeignKey("UserID")]
        public Guid MessageID { get; set; }
    }
}
