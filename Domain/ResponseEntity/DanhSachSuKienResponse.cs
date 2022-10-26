using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ResponseEntity
{
    public class DanhSachSuKienResponse
    {
        public List<SuKienItemResponse> Data { get; set; } = new List<SuKienItemResponse>();
        public int TotalRows { get; set; }
    }
}
