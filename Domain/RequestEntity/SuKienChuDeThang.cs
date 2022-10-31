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
        //nếu không nhập trường thì sẽ  được set giá trị initial
        public int Month { get; set; }=DateTime.Now.Month;
        public int Year { get; set; }=DateTime.Now.Year;
        public int pagesize { get; set; } = 10;
        public int pageindex { get; set; } = 1;
        public int? IDChuDeThang { get; set; }
    }
}
