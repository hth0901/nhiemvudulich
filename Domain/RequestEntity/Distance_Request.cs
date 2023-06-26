using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RequestEntity
{
    public class Distance_Request
    {
        public double x { get; set; }
        public double y { get; set; }
        public float distance { get; set; } = 500;
        public int pagesize { get; set; } = 10;
        public int pageindex { get; set; } = 1; 
    }
}
