﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ResponseEntity
{
    public class DanhSachMonAnThucUongResponse
    {
        public List<MonAnThucUongItemResponse> Data { get; set; } = new List<MonAnThucUongItemResponse>();
        public int TotalRows { get; set; }


    }
}
