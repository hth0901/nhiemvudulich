using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class DiemVeSinh
    {
        public int Id { get; set; }
        public string MoTa { get; set; }
        public bool IsStatus { get; set; }
        public bool IsDelete { get; set; }
        public string Ten { get; set; } 
        public int DiemVeSinhID { get; set; }
        public string DonVi { get; set; }   
        public string HienTrang { get; set; }
        public string GhiChu { get; set; }  
        public float X { get; set; }    
        public float Y { get; set; }    

    }
}
