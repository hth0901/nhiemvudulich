using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ResponseEntity
{
    public class DanhSach<T>
    {
        
            public List<T> Data { get; set; } = new List<T>();
            public int TotalRows { get; set;}
        
    }
}
