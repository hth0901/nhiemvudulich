using Domain.TechLife;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ResponseEntity
{
    public class DanhSachHoSoLuTruResponse
    {
        public List<HoSoLuTruItemResponse> Data { get; set; } = new List<HoSoLuTruItemResponse>();
        public int TotalRows { get; set; }
    }
}
