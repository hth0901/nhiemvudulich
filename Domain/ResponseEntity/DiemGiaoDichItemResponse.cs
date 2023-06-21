using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ResponseEntity
{
    public class DiemGiaoDichItemResponse
    {
        public int ID { get; set; }
        public string TenDiaDiem { get; set; }
        public double? ToaDoX { get; set; }
        public double? ToaDoY { get; set; }
        public int TotalRows { get; set; }
    }

    public class DiemGiaoDichTrinhDien
    {
        public int ID { get; set; }
        public string TenDiaDiem { get; set; }
        public int Loai { get; set; }
        public string TenLoai { get; set; }
        public string DienThoai { get; set; }
        public string GioPhucVu { get; set; }
        public string DiaChi { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public int PhuongXaId { get; set; }
        public int QuanHuyenId { get; set; }
        public string TenPhuongXa { get; set; }
        public string TenQuanHuyen { get; set; }
    }
}
