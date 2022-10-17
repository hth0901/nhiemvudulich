using Application.DuongDayNong;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using Application.LoaiMonAnThucUong;
using Application.MonAnThucUong;
using Domain.ResponseEntity;
using System.Drawing.Printing;
using Application.Core;

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
    
        public async Task<IActionResult> DanhSachLoaiMonAnThucUong(CancellationToken ct )
        {
            var listResult = await Mediator.Send(new LoaiMonAnThucUongGets.Query (), ct);
            //return HandlerResult(listResult);
            return HandlerResult(listResult);
        }
    }
}
