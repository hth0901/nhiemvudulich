using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ResponseEntity
{
    public class DanhSachDuongDayNongResponse
    {

        public List<DuongDayNongItemResponse> Data { get; set; } = new List<DuongDayNongItemResponse>();
        public int TotalRows { get; set; }
    }
}
