using Application.DiaDiemDuLich;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using Application.NganHangDiemGiaoDich;

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
        [Route("danhsachdiemgiaodich")]
        public async Task<IActionResult> DanhSachDiaDiem(CancellationToken ct)
        {
            var listResult = await Mediator.Send(new DanhSachDiemGiaoDich.Query(), ct);
            return HandlerResult(listResult);
        }
    }
}
