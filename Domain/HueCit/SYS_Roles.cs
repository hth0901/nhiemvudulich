using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.HueCit
{
    public class SYS_Roles
    {
        public int ID { get; set; }
        public string Ten { get; set; }
        public string MoTa { get; set; }
        public int TrangThai { get; set; }
    }

    public class RoleRequest
    {
        public string TuKhoa { get; set; }
        public int? Used { get; set; }
    }
}
