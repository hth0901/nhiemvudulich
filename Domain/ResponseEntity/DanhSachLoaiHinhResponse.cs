using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ResponseEntity
{
    public class DanhSachLoaiHinhResponse
    {
        public List<LoaiHinhItemResponse> Data { get; set; } = new List<LoaiHinhItemResponse>();
        public int TotalRows { get; set; }
    }
}
