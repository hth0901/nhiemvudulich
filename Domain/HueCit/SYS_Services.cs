using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.HueCit
{
    public class SYS_Services
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int LinhVucKinhDoanhId { get; set; }
        public string ParentID { get; set; }
    }
}
