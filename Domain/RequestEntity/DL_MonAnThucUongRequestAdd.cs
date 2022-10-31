using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RequestEntity
{
    public class DL_MonAnThucUongRequestAdd
    {
       
        public string TenMon { get; set; }
        public int? MaLoai { get; set; }
        public string MoTa { get; set; }
        public int KieuMon { get; set; }
        public bool ThucUong { get; set; }
        public string CachLam { get; set; }
        public string ThanhPhan { get; set; }
        public string KhuyenNghiKhiDung { get; set; }
        public int AmThucID { get; set; }
        public int NguoiDongBo { get; set; }
    }
}
