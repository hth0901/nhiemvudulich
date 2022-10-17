using Application.DuBaoThoiTiet;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using Application.DuongDayNong;
using Application.Core;
using Application.DiemVeSinh;
using Domain.ResponseEntity;
using System.Drawing.Printing;

namespace HueCitApp.Controllers
{
    public class DuongDayNongController : BaseApiController
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public DuongDayNongController(IWebHostEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
            _webHostEnvironment = hostingEnvironment;
        }
        [HttpGet("danhsach/{pagesize?}/{pageindex?}")]
        [AllowAnonymous]
        public async Task<IActionResult> DanhSachDuongDayNong(CancellationToken ct, int pagesize = 10, int pageindex = 1)
        {
            var listResult = await Mediator.Send(new DuongDayNongGets.Query { pagesize = pagesize, pageindex = pageindex }, ct);
            var result = new DanhSach<DuongDayNongItemResponse>();
            result.TotalRows = 0;
            if (listResult.Value.Count > 0)
            {
                result.Data = listResult.Value;
                result.TotalRows = result.Data[0].TotalRows;
            }
            //return HandlerResult(listResult);
            return HandlerResult(Result<DanhSach<DuongDayNongItemResponse>>.Success(result));


      
        }
    }
}
