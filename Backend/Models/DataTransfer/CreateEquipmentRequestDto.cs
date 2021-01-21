using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DataTransfer
{
    public class CreateEquipmentRequestDto
    {
        [DisplayName("User ID")]
        public Guid UserID { get; set; }
        [DisplayName("Team ID")]
        public int TeamID { get; set; }
        [DisplayName("Request Date")]
        public DateTime RequestDate { get; set; }
        [DisplayName("Message")]
        public string Message { get; set; }
        [DisplayName("Item ID")]
        public int ItemID { get; set; }
        [DisplayName("Status")]
        public string Status { get; set; }
    }
}
