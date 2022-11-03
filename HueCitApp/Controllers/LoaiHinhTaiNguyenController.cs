using Application.Core;
using Application.DiaDiemDuLich;
using Domain.ResponseEntity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;

namespace HueCitApp.Controllers
{
    public class LoaiHinhTaiNguyenController : BaseApiController
    {

        private readonly IWebHostEnvironment _webHostEnvironment;
        public LoaiHinhTaiNguyenController(IWebHostEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
            _webHostEnvironment = hostingEnvironment;
        }
        [HttpGet("tainguyen")]
        [AllowAnonymous]

        public async Task<IActionResult> DanhSachLoaiHinhTaiNguyen(CancellationToken ct)
        {
            var listResult = await Mediator.Send(new LoaiHinhTaiNguyenDuLichGets.Query(), ct);

            //return HandlerResult(listResult);
            return HandlerResult(listResult);
        }
        [HttpGet("disanvanhoa")]
        [AllowAnonymous]

        public async Task<IActionResult> LoaiHinhDiSanVanHoa(CancellationToken ct)
        {
            var listResult = await Mediator.Send(new LoaiHinhDiSanVanHoaGets.Query(), ct);

            //return HandlerResult(listResult);
            return HandlerResult(listResult);
        }



    }
}
