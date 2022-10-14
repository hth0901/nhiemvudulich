using Application.Core;
using Application.DiaDiemDuLich;
using Domain.ResponseEntity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;

namespace HueCitApp.Controllers
{
    public class LoaiHinhTaiNguyenController : Controller
    {
        public class DiaDiemDuLichController : BaseApiController
        {
            private readonly IWebHostEnvironment _webHostEnvironment;
            public DiaDiemDuLichController(IWebHostEnvironment hostingEnvironment) : base(hostingEnvironment)
            {
                _webHostEnvironment = hostingEnvironment;
            }
            [HttpGet("danhsach/{pagesize?}/{pageindex?}")]
            [AllowAnonymous]

            public async Task<IActionResult>DanhSachLoaiHinhTaiNguyen(CancellationToken ct, int pagesize = 10, int pageindex = 1)
            {
                var listResult = await Mediator.Send(new LoaiHinhTaiNguyenDuLichGets.Query { pagesize = pagesize, pageindex = pageindex }, ct);
                var result = new DanhSachLoaiHinhTaiNguyenResponse();
                result.TotalRows = 0;
                if (listResult.Value.Count > 0)
                {
                    result.Data = listResult.Value;
                    result.TotalRows = result.Data[0].TotalRows;
                }
                //return HandlerResult(listResult);
                return HandlerResult(Result<DanhSachLoaiHinhTaiNguyenResponse>.Success(result));
            }
          

        }
    }
}
