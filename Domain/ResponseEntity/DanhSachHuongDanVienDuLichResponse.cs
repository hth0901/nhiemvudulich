using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ResponseEntity
{
    public class DanhSachHuongDanVienDuLichResponse
    {
        public List<HuongDanVienDuLichItemResponse> Data { get; set; } = new List<HuongDanVienDuLichItemResponse>();
        public int TotalRows { get; set; }

    }
}
