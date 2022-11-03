using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.HueCit
{
    public class CN_QuanTracMoiTruong
    {
        public int ID { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public string Node { get; set; }
        public string TenNode { get; set; }
        public string TenThongSo { get; set; }
        public float GiaTri { get; set; }
        public string DonViTinh { get; set; }
        public DateTime ThoiDiem { get; set; }
        public Int16 TrangThai { get; set; }
        public string _id { get; set; }
    }
}
