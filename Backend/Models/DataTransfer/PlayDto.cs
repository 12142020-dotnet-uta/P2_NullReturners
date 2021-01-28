using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DataTransfer
{
    public class PlayDto
    {
        public int PlaybookID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] DrawnPlay { get; set; }
        public string ImageString { get; set; }
    }
}
