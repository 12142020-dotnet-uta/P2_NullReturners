using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    class EquipmentItem
    {
        [Key]
        [DisplayName("Equipment ID")]
        public int EquipmentID { get; set; }
        [DisplayName("Description")]
        public string Description { get; set; }
    }
}
