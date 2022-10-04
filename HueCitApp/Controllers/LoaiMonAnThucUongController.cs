using Application.DuongDayNong;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using Application.LoaiMonAnThucUong;

namespace HueCitApp.Controllers
{
    public class LoaiMonAnThucUongController : BaseApiController
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public LoaiMonAnThucUongController(IWebHostEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
            _webHostEnvironment = hostingEnvironment;
        }
        [HttpGet]
        [AllowAnonymous]
    
        public async Task<IActionResult> DanhSachLoaiMonAnThucUong(CancellationToken ct)
        {
            var listResult = await Mediator.Send(new LoaiMonAnThucUong.Query(), ct);
            return HandlerResult(listResult);
        }
    }
}
