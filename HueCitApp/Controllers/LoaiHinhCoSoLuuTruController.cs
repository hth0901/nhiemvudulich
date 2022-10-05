using Application.DiaDiemDuLich;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using Application.LoaiHinhCoSoLuuTru;

namespace HueCitApp.Controllers
{
    public class LoaiHinhCoSoLuuTruController : BaseApiController
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public LoaiHinhCoSoLuuTruController(IWebHostEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
            _webHostEnvironment = hostingEnvironment;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> DanhSachLoaiHinh(CancellationToken ct)
        {
            var listResult = await Mediator.Send(new DanhSachLoaiHinhCoSoLuuTru.Query(), ct);
            return HandlerResult(listResult);
        }
    }
}
