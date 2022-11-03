using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ResponseEntity
{
    public class QuanTracMoiTruongThongKe
    {
        public DateTime ThoiDiem { get; set; }
        public float AQI { get; set; }
        public float PM01 { get; set; }
        public float PM25 { get; set; }
        public float HUM { get; set; }
        public float TVOC { get; set; }
        public float CO2 { get; set; }
        public float PM10 { get; set; }
        public float TEMP { get; set; }
        public int ToalRows { get; set; }
    }
}
