using Application.DiaDiemDuLich;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using Application.DiemGiaoDich;

namespace HueCitApp.Controllers
{
    public class DiemGiaoDichController : BaseApiController
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public DiemGiaoDichController(IWebHostEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
            _webHostEnvironment = hostingEnvironment;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> DanhSachDiemGiaoDich(CancellationToken ct)
        {
            var listResult = await Mediator.Send(new DanhSachDiemGiaoDich.Query(), ct);
            return HandlerResult(listResult);
        }
        [HttpGet("danhsachnganhang")]
        [AllowAnonymous]
     
        public async Task<IActionResult> DanhSachNganHang(CancellationToken ct)
        {
            var listResult = await Mediator.Send(new DanhSachNganHang.Query(), ct);
            return HandlerResult(listResult);
        }
    }
}
