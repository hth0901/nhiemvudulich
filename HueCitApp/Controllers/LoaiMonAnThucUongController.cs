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
        [HttpGet("danhsach/{pagesize?}/{pageindex?}")]
        [AllowAnonymous]
    
        public async Task<IActionResult> DanhSachLoaiMonAnThucUong(CancellationToken ct, int pagesize = 10, int pageindex = 1)
        {
            var listResult = await Mediator.Send(new LoaiMonAnThucUongGets.Query { pagesize = pagesize, pageindex = pageindex }, ct);
            var result = new DanhSachLoaiHinhResponse();
            result.TotalRows = 0;
            if (listResult.Value.Count > 0)
            {
                result.Data = listResult.Value;
                result.TotalRows = result.Data[0].TotalRows;
            }
            //return HandlerResult(listResult);
            return HandlerResult(Result<DanhSachLoaiHinhResponse>.Success(result));
        }
    }
}
