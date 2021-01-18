using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Message
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Message ID")]
        public Guid ID { get; set; }

        [DisplayName("Sender ID")]
        public int SenderID { get; set; }

        [DisplayName("Recipient ID")]
        public int RecipientID { get; set; }

        [DisplayName("Message Text")]
        public string MessageText { get; set; }
    }
}
