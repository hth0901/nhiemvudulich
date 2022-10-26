using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ResponseEntity
{
    public class DanhSachDiemGiaoDichResponse
    {

        public List<DiemGiaoDichItemResponse> Data { get; set; } = new List<DiemGiaoDichItemResponse>();
        public int TotalRows { get; set; }
    }
}
