using Domain.HueCit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RequestEntity
{
    public class SuKienChuDeThang
    {

        public int Month { get; set; }
        public int Year { get; set; }
        public int pagesize { get; set; } = 10;
        public int pageindex { get; set; } = 1;
        public int? IDChuDeThang { get; set; }
    }
}
