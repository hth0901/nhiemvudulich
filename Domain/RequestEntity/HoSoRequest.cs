using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RequestEntity
{
    public class HoSoRequest
    {
        public int LinhVucId { get; set; }
        public string HangSao { get; set; }
        public string LoaiHinhId { get; set; }
        public string LoaiDiaDiemAnUong { get; set; }
        public string TienNghi { get; set; }
        public string LoaiPhong { get; set; }
    }

    public class HoSoGetRequest
    {
        public int LinhVuc { get; set; }
        public int Id { get; set; }
    }
}
