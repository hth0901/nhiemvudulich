using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using Application.DiaDiemAnUong;

namespace HueCitApp.Controllers
{
    public class DiaDiemAnUongController : BaseApiController

    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public DiaDiemAnUongController(IWebHostEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
            _webHostEnvironment = hostingEnvironment;
        }
        [HttpGet]
        [AllowAnonymous]

        public async Task<IActionResult> danhsachDiaDiemAnUong(CancellationToken ct)
        {
            var listResult = await Mediator.Send(new DanhSachDiaDiemAnUong.Query(), ct);
            return HandlerResult(listResult);
        }
    }
}
