using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ResponseEntity
{
    public class SuKienItemResponse
    {
        public int ID {  get; set; }
        public string TieuDe { get; set; }   
        public string ?ChuDe { get; set; }
        public int ?MaChude { get; set; }
        public int TotalRows { get; set; }
    }
}
