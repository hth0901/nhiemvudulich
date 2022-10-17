
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using Application.Core;
using Application.MonAnThucUong;
using Domain.ResponseEntity;
using System.Drawing.Printing;
using Application.LoaiCoSoVuiChoiGiaiTri;

namespace HueCitApp.Controllers
{
    public class LoaiHinhCoSoVuiChoiGiaiTriController : BaseApiController
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public LoaiHinhCoSoVuiChoiGiaiTriController(IWebHostEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
            _webHostEnvironment = hostingEnvironment;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> DanhSachLoaiHinhCoSoVuiChoiGiaiTri(CancellationToken ct)
        {
            var listResult = await Mediator.Send(new LoaiHinhCoSoVuiChoiGiaiTriGets.Query (), ct);
         
            //return HandlerResult(listResult);
            return HandlerResult(listResult);
        }
    }
}
