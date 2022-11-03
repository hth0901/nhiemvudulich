using Application.DiaDiemDuLich;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.AspNetCore.Hosting;
using Application.MonAnThucUong;
using Application.Core;
using Application.DuongDayNong;
using Domain.ResponseEntity;
using System.Drawing.Printing;

namespace HueCitApp.Controllers
{
    public class MonAnThucUongController :BaseApiController
    {
        public MonAnThucUongController(IWebHostEnvironment hostingEnvironment) : base(hostingEnvironment)
        {//run on hosting
        }

        [HttpGet("danhsach/{pagesize?}/{pageindex?}")]
        [AllowAnonymous]
        public async Task<IActionResult> DanhSachMonAnThucUong(CancellationToken ct, int pagesize=10,int pageindex=1)
        {
            var listResult = await Mediator.Send(new MonAnThucUongGets.Query { pagesize = pagesize, pageindex = pageindex }, ct);
            var result = new DanhSach<MonAnThucUongItemResponse>();
            result.TotalRows = 0;
            if (listResult.Value.Count > 0)
            {
                result.Data = listResult.Value;
                result.TotalRows = result.Data[0].TotalRows;
            }
            //return HandlerResult(listResult);
            return HandlerResult(Result<DanhSach<MonAnThucUongItemResponse>>.Success(result));

        }
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> ChiTietMonAnThucUong(int id,CancellationToken ct)
        {
            var listResult = await Mediator.Send(new ChiTietMonAnThucUongGet.Query { ID=id}, ct);
            return HandlerResult(listResult);

        }
    }
}
