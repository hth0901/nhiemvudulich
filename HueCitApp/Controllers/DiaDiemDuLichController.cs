using Application.Core;
using Application.DiaDiemAnUong;
using Application.DiaDiemDuLich;
using Domain.ResponseEntity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;
using System.Threading;
using System.Threading.Tasks;

namespace HueCitApp.Controllers
{
    public class DiaDiemDuLichController : BaseApiController
    {   private readonly IWebHostEnvironment _webHostEnvironment;
        public DiaDiemDuLichController(IWebHostEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
            _webHostEnvironment = hostingEnvironment;
        }
        [HttpGet("danhsach/{pagesize?}/{pageindex?}")]
        [AllowAnonymous]
   
        public async Task<IActionResult> GetPlaces( CancellationToken ct, int pagesize = 10, int pageindex = 1)
        {
            var listResult = await Mediator.Send(new DiaDiemDuLichGets.Query { pagesize = pagesize, pageindex = pageindex }, ct);
            var result = new DanhSachHoSoLuTruResponse();
            result.TotalRows = 0;
            if (listResult.Value.Count > 0)
            {
                result.Data = listResult.Value;
                result.TotalRows = result.Data[0].TotalRows;
            }
            //return HandlerResult(listResult);
            return HandlerResult(Result<DanhSachHoSoLuTruResponse>.Success(result));
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("loaihinhtainguyen")]
        public async Task<IActionResult> LoaiHinhTaiNguyenDuLich(CancellationToken ct)
        {
            var listResult = await Mediator.Send(new LoaiHinhTaiNguyenDuLichGets.Query(), ct);
            return HandlerResult(listResult);
            
        }

    }
}
