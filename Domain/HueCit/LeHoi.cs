using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.HueCit
{
    public enum CapQuanLyLeHoi : int
    {
        QuocGia = 1,
        KhuVuc = 2,
        Tinh = 3,
        Huyen = 4,
        Xa = 5,
    }

    public class LeHoiModel
    {
        public Guid ID { get; set; }
        public string TenLeHoi { get; set; }
        public int Loai { get; set; }
        public CapQuanLyLeHoi Cap { get; set; }
        public string LoaiLeHoi { get; set; }
        public string CapQuanLy { get; set; }
        public string NoiDung { get; set; }
        public DateTime BatDau { get; set; }
        public DateTime KetThuc { get; set; }
        public string DiaDiem { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public List<FileUpload> Files { get; set; }
        public int LeHoiID { get; set; }
        public int PhuongXaId { get; set; }
        public int QuanHuyenId { get; set; }
        public string TenPhuongXa { get; set; }
        public string TenQuanHuyen { get; set; }
    }
}
