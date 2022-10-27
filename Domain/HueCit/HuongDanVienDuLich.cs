using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.HueCit
{
    public class HuongDanVienDuLich
    {
        public int? Id { get; set; }
        public string HoVaTen { get; set; }
        public bool GioiTinh { get; set; }
        public string CMND { get; set; }
        public DateTime NgayCapCMND { get; set; }
        public string NoiCapCMND { get; set; }
        public string SoDienThoai { get; set; }
        public string Email { get; set; }
        public string DiaChi { get; set; }
        public string HoKhau { get; set; }
        public string SoTheHDV { get; set; }
        public int LoaiTheId { get; set; }
        public DateTime NgayCapThe { get; set; }
        public DateTime NgayHetHan { get; set; }
        public bool IsStatus { get; set; }
        public bool IsDelete { get; set; }
        public int CongTyLuHanhId { get; set; }
        public string NoiCapThe { get; set; }
        public DateTime NgaySinh { get; set; }
    }
}
