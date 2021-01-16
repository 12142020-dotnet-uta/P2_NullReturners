using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Play
    {
        public int ID { get; set; }
        public int PlaybookId { get; set; }
        public int PlayCategory { get; set; }
        public byte[] drawnPlay { get; set; } //might change, goal is to have coaches able to draw a play and save it to the playbook
    }
}
