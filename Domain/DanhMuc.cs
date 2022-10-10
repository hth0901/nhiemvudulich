using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class DanhMuc
    {
        public int Id { get; set; }
        public string Ten { get; set; } 
        public int LoaiId { get; set; } 
        public string MoTa { get; set; }    
        public bool Status { get; set; }    
        public bool IsDelete { get; set; }

    }
}
