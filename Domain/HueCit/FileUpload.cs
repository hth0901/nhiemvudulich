using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.HueCit
{
    public class FileUpload
    {
        public int ID { get; set; }
        public string TenFile { get; set; }
        public string DuongDan { get; set; }
        public string LoaiDoiTuong { get; set; }
        public int LoaiFile { get; set; }
        public string LoaiHinhDuLieu { get; set; }
        public DateTime NgayTao { get; set; }
        public bool TrangThai { get; set; }
        public string IDDoiTuong { get; set; }
    }
}
