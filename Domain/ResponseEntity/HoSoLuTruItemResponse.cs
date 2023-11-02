using Domain.TechLife;

namespace Domain.ResponseEntity
{
    public class HoSoLuTruItemResponse
    {
        public int Id { get; set; }
        public string Ten { get; set; }
        public double? ToaDoX { get; set; }
        public double? ToaDoY { get; set; }
        public int TotalRows { get; set; }
    }
    public class HoSoCoSoLuuTruItemResponse : HoSo
    {
        public int TotalRows { get; set; }
    }
}
