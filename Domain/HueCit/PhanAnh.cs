using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.HueCit
{
    public class PhanAnhModel
    {
        public int Id { get; set; }
        public int PhanAnhId { get; set; }
        public string TieuDe { get; set; }
        public int MaLinhVuc { get; set; }
        public string NoiDung { get; set; }
        public int? ObjectId { get; set; }
        public int? MaDiaDiem { get; set; }
        public int? MaLoaiDuLieu { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public string DiaChiSuKien { get; set; }
        public string NguoiGui { get; set; }
        public DateTime? NgayGui { get; set; }
        public string MaCoQuanXuLy { get; set; }
        public string TenCoQuan { get; set; }
        public string YKienXuLy { get; set; }
        public DateTime? NgayXuLy { get; set; }
        public bool LoaiXuLy { get; set; }
        public DateTime NgayTao { get; set; }
    }
}
