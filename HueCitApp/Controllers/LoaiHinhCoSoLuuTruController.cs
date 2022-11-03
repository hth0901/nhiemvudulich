using Application.DiaDiemDuLich;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using Application.LoaiHinhCoSoLuuTru;
using Application.Core;
using Application.MonAnThucUong;
using Domain.ResponseEntity;
using System.Drawing.Printing;

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
        public async Task<IActionResult> DanhSachLoaiHinhLuuTru(CancellationToken ct)
        {
            var listResult = await Mediator.Send(new LoaiHinhCoSoLuuTruGets.Query (), ct);
           
            //return HandlerResult(listResult);
            return HandlerResult(listResult);


            
        }
    }
}
