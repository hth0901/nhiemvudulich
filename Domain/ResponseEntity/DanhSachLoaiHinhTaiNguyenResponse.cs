using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ResponseEntity
{
    public class DanhSachLoaiHinhTaiNguyenResponse
    {
        public List<LoaiHinhTaiNguyenItemResponse> Data { get; set; } = new List<LoaiHinhTaiNguyenItemResponse>();
        public int TotalRows { get; set; }
    }
}
