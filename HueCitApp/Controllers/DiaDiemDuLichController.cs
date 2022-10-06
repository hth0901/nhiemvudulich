using Application.DiaDiemDuLich;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace HueCitApp.Controllers
{
    public class DiaDiemDuLichController : BaseApiController
    {   private readonly IWebHostEnvironment _webHostEnvironment;
        public DiaDiemDuLichController(IWebHostEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
            _webHostEnvironment = hostingEnvironment;
        }
        [HttpGet]
        [AllowAnonymous]
   
        public async Task<IActionResult> GetPlaces( CancellationToken ct)
        {
            var listResult = await Mediator.Send(new DanhSachDiaDiemDuLich.Query(), ct);
            return HandlerResult(listResult);
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("loaihinhtainguyen")]
        public async Task<IActionResult> LoaiHinhTaiNguyenDuLich(CancellationToken ct)
        {
            var listResult = await Mediator.Send(new DanhSachLoaiHinhTaiNguyenDuLich.Query(), ct);
            return HandlerResult(listResult);
            
        }

    }
}
