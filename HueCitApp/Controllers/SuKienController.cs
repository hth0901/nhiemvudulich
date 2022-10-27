using Application.DiaDiemDuLich;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using Application.SuKien;
using Microsoft.AspNetCore.Hosting;
using Application.Core;
using Application.MonAnThucUong;
using Domain.ResponseEntity;
using System.Drawing.Printing;
using Domain.RequestEntity;

namespace HueCitApp.Controllers
{
    public class SuKienController : BaseApiController
    {
        public SuKienController(IWebHostEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
        }

        [HttpGet("theothang")]
        [AllowAnonymous]

        public async Task<IActionResult> DanhSachSuKienTheoThang(CancellationToken ct, [FromBody] SuKienChuDeThang search)
        {
            var listResult = await Mediator.Send(new SuKienThangGets.Query {search= search }, ct);
            var result = new DanhSach<SuKienItemResponse>();
            result.TotalRows = 0;
            if (listResult.Value.Count > 0)
            {
                result.Data = listResult.Value;
                result.TotalRows = result.Data[0].TotalRows;
            }
            //return HandlerResult(listResult);
            return HandlerResult(Result<DanhSach<SuKienItemResponse>>.Success(result));

          

        }
        [HttpGet("theothang/theochude")]
        [AllowAnonymous]

        public async Task<IActionResult> DanhSachSuKienThangTheoChuDe(CancellationToken ct, [FromBody] SuKienChuDeThang search)
        {

            var listResult = await Mediator.Send(new SuKienThangTheoChuDeGets.Query {search=search  }, ct);
            var result = new DanhSach<SuKienItemResponse>();
            result.TotalRows = 0;
            if (listResult.Value.Count > 0)
            {
                result.Data = listResult.Value;
                result.TotalRows = result.Data[0].TotalRows;
            }
            //return HandlerResult(listResult);
            return HandlerResult(Result<DanhSach<SuKienItemResponse>>.Success(result));


      

        }
        [HttpGet("{id}")]
        [AllowAnonymous]
      
      
        public async Task<IActionResult> ChiTietSuKien(CancellationToken ct,int ID)
        {
            var listResult = await Mediator.Send(new ChiTietSuKienGet.Query{ IDSuKien=ID}, ct);
            return HandlerResult(listResult);

        }



    }
}
