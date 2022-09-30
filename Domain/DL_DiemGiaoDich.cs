using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class DL_DiemGiaoDich
    {
        public int ID { get; set; } 
        public int Loai { get; set; }
        public string TenDiaDiem { get; set; }  
        public string DienThoai { get; set; }
        public string GioPhucVu { get; set; }   
        public string DiaChi { get; set; }  
        public float X { get; set; }
        public float Y { get; set; }
        public int DiemGiaoDichID { get; set; }
    }
}
