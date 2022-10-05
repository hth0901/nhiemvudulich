using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class DL_SuKien
    {
        public int ID { get; set; }
        public int MaChuDe { get;set; }
        public string NoiDung { get; set; } 
        public DateTime BatDau { get;set;}
        public DateTime KetThuc { get; set; }
        public string DiaDiem { get; set; } 
        public float X { get; set; }    
        public float Y { get; set; }
        public bool TrangThai { get; set; } 
      

    }
}
