using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DataTransfer
{
    public class ReplyDto
    {
        [DisplayName("Sender ID")]
        public Guid SenderID { get; set; }
        [DisplayName("Recipient List ID")]
        public Guid RecipientListID { get; set; }
        [DisplayName("Message Text")]
        public string MessageText { get; set; }
    }
}
