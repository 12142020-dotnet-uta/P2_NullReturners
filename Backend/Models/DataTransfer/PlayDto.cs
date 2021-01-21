﻿using System;
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
        public byte[] DrawnPlay { get; set; } //let's just call it a byte array for now until we figure that part out
    }                                           //or we can just comment the whole thing out for now
}