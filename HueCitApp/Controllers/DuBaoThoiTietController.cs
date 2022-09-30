using Application.DiemGiaoDich;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using Application.DuBaoThoiTiet;

namespace HueCitApp.Controllers
{
    public class DuBaoThoiTietController : BaseApiController
    {   
        private readonly IWebHostEnvironment _webHostEnvironment;
        public DuBaoThoiTietController(IWebHostEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
            _webHostEnvironment = hostingEnvironment;
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("forecastinglist")]
        public async Task<IActionResult> DanhSachDuBaoThoiTiet(CancellationToken ct)
        {
            var listResult = await Mediator.Send(new DanhSachDuBaoThoiTiet.Query(), ct);
            return HandlerResult(listResult);
        }
    }
}
