using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ResponseEntity
{
    public class MonAnThucUongItemResponse
    {
        int ID { get; set; }
        string TenMon { get; set; }
        public int TotalRows { get; set; }
    }
}
