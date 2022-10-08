using Application.DuBaoThoiTiet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using Application.DiaDiemMuaSamGiaiTri;

namespace HueCitApp.Controllers
{
    public class DiaDiemVuiChoiGiaiTriController : BaseApiController
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public DiaDiemVuiChoiGiaiTriController(IWebHostEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
            _webHostEnvironment = hostingEnvironment;
        }
        [HttpGet]
        [AllowAnonymous]

        public async Task<IActionResult> DanhSachDuBao(CancellationToken ct)
        {
            var listResult = await Mediator.Send(new DiaDiemMuaSamGiaiTriGets.Query(), ct);
            return HandlerResult(listResult);
        }
    }
}
