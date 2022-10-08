using Application.DuBaoThoiTiet;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using Application.DuongDayNong;

namespace HueCitApp.Controllers
{
    public class DuongDayNongController : BaseApiController
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public DuongDayNongController(IWebHostEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
            _webHostEnvironment = hostingEnvironment;
        }
        [HttpGet]
        [Route("danhsachduongdaynong")]
        [AllowAnonymous]
        public async Task<IActionResult> DanhSachDuongDayNong(CancellationToken ct)
        {
            var listResult = await Mediator.Send(new DuongDayNongGets.Query(), ct);
            return HandlerResult(listResult);
        }
    }
}
