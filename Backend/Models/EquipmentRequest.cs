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
    public class EquipmentRequest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Request ID")]
        public int RequestID { get; set; }
        [DisplayName("User ID")]
        [ForeignKey("UserID")]
        public Guid UserID { get; set; }
        [DisplayName("Team ID")]
        [ForeignKey("TeamID")]
        public int TeamID { get; set; }
        [DisplayName("Request Date")]
        [DataType(DataType.DateTime)]
        public DateTime RequestDate { get; set; }
        [DisplayName("Request Message")]
        public string Message { get; set; }  // optional
        [DisplayName("Item ID")]
        [ForeignKey("EquipmentID")]
        public int ItemId { get; set; }
        [DisplayName("Request Status")]
        public string Status { get; set; }
    }
}
