using Application.DichVuVanChuyen;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using Application.LoaiHinhCoSoLuuTru;
using Microsoft.AspNetCore.Authorization;
using Application.LoaiPhuongtienGiaoThong;
using Application.LoaiCoSoVuiChoiGiaiTri;

namespace HueCitApp.Controllers
{
    public class LoaiHinhController : BaseApiController
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public LoaiHinhController(IWebHostEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
            _webHostEnvironment = hostingEnvironment;
        }
        [HttpGet("phuongtiengiaothong")]
        [AllowAnonymous]
        public async Task<IActionResult> DanhSachLoaiPhuongTienGiaoThong(CancellationToken ct)
        {
            var listResult = await Mediator.Send(new LoaiPhuongTienGiaoThongGets.Query(), ct);
            return HandlerResult(listResult);
        }
        [HttpGet("cosovuichoigiaitri")]
        [AllowAnonymous]
        public async Task<IActionResult> DanhSachLoaiHinhCoSoVuiChoiGiaiTri(CancellationToken ct)
        {
            var listResult = await Mediator.Send(new LoaiHinhCoSoVuiChoiGiaiTriGets.Query(), ct);
            return HandlerResult(listResult);

        }
    }
}
