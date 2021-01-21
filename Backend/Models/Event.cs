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
    public class Event
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Event ID")]
        public int EventID { get; set; }
        [DisplayName("Team ID")]
        [ForeignKey("TeamID")]
        public int TeamID { get; set; }
        [DisplayName("Description")]
        public string Description { get; set; }
        [DisplayName("Event Date")]
        public DateTime EventDate { get; set; }
        [DisplayName("Event Location")]
        public string Location { get; set; }
        [DisplayName("Event Message")]
        public string Message { get; set; }
    }
}
