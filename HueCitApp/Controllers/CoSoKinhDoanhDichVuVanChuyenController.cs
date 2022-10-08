using Application.CoSoLuuTru;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using Application.DichVuVanChuyen;

namespace HueCitApp.Controllers
{
    public class CoSoKinhDoanhDichVuVanChuyenController : BaseApiController
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CoSoKinhDoanhDichVuVanChuyenController(IWebHostEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
            _webHostEnvironment = hostingEnvironment;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> DanhSachCoSoKinhDoanhDichVuVanChuyen(CancellationToken ct)
        {
            var listResult = await Mediator.Send(new CoSoDichVuVanChuyenGets.Query(), ct);
            return HandlerResult(listResult);
        }


        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> ChiTietCoSoKinhDoanhDichVuVanChuyen(int ID,CancellationToken ct)
        {
            var listResult = await Mediator.Send(new CoSoDichVuVanChuyenGet.Query { ID=ID}, ct);
            return HandlerResult(listResult);
        }
    }
}
