using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.HueCit
{
    public  class DL_ThoiTiet_Symbol
    {
        public int ID { get; set; }
        public string Language { get; set; }
        public string SymbolName { get;set; }
        public string IDFile { get;set;}
        public string TenFileDinhKem { get; set; }
        public int SortOrder { get; set; }
        public bool Published { get; set; }
        public DateTime NgayTao { get; set; }
    }
}
