using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.HueCit
{
    public class FileUploads
    {   public int Fileid { get; set; }
        public string FileName { get; set; }
        public string FileUrl { get; set; }
        public string Type { get; set; }
        public int Id { get; set; }
        public bool IsImage { get; set; }   

        public bool IsStatus { get; set; }
        public DateTime NgayTao { get; set; }
        public int FileType { get; set; }
        public int NguonDongBo { get; set; }
        public string MoTa { get; set; }

    }
}
