using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ResponseEntity
{
    public class DiemGiaoDichItemResponse
    {
        public int Id { get; set; }
        public string TenDiaDiem { get; set; }
        public int TotalRows { get; set; }
    }
}
