using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ResponseEntity
{
    public class LoaiHinhItemResponse
    {
        public int Id { get; set; }
        public string TenLoai { get; set; }
        public int TotalRows { get; set; }   
    }
}
