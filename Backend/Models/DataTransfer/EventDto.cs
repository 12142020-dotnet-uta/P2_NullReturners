using Google.Apis.Calendar.v3.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DataTransfer
{
    public class EventDto
    {
        [DisplayName("Event ID")]
        public Guid EventID { get; set; }
        [DisplayName("Description")]
        public string Description { get; set; }
        [DisplayName("Event Location")]
        public string Location { get; set; }
        [DisplayName("Event Message")]
        public string Message { get; set; }
        [DisplayName("Start Time")]
        public EventDateTime StartTime { get; set; }
        [DisplayName("End Time")]
        public EventDateTime EndTime { get; set; }
    }
}
