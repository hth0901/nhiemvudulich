using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.HueCit
{
    public class SYS_PhanQuyen
    {
        public int ID { get; set; }
        public int RoleID { get; set; }
        public int MenuID { get; set; }
    }

    public class SYS_PhanQuyenRequest
    {
        public int RoleID { get; set; }
        public string MenuList { get; set; }
    }

    public class SYS_PhanQuyenTrinhDien
    {
        public int ID { get; set; }
        public int RoleID { get; set; }
        public int MenuID { get; set; }
        public string Ten { get; set; }
    }
}
