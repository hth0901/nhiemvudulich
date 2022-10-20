using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RequestEntity
{
    public class Distance_Request
    {
        public float x { get; set; }
        public float y { get; set; }
        public float distance { get; set; }
        public int pagesize { get; set; } = 10;
        public int pageindex { get; set; } = 1;
    }
}
