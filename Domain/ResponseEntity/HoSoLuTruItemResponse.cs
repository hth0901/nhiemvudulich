﻿using Domain.TechLife;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ResponseEntity
{
    public class HoSoLuTruItemResponse 
    { 
        public int Id { get; set; }
        public string Ten { get;set; }
        public int TotalRows { get; set; }
    }
}
