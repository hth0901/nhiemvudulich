using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class DL_ThoiTiet
    {

        public string ID { get; set; }
        public DateTime TuNgay { get; set; }
        public DateTime DenNgay { get; set; }
        public string DuBao { get; set; }
        public string SymbolId { get; set; }
        public string Title { get; set; }
        public string Temperature { get; set; }
        public string PlainArea { get; set; }
        public string MountainousRegoin { get; set; }
        public string HueCityArea { get; set; }
        public string MarineWheather { get; set; }
        public string ForestfiresForeCast { get;set; }
        public string Warning { get; set; }
        public string Content { get; set; }
        public string Language { get; set; }
        public bool Publish { get; set; }  
        public string OwnerCode { get; set; }  
        public int ModuleID { get; set; }
        public int CreatedByUserId { get; set; }
        public int LastModifiedByUserId { get; set; }
        public DateTime NgayTao { get; set; }

    }
}
