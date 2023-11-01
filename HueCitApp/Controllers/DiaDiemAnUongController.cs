using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using Application.DiaDiemAnUong;
using Application.CoSoLuuTru;
using Application.Core;
using Domain.ResponseEntity;
using Application.DiaDiemDuLich;
using Domain.RequestEntity;

namespace HueCitApp.Controllers
{
    public class DiaDiemAnUongController : BaseApiController

    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public DiaDiemAnUongController(IWebHostEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
            _webHostEnvironment = hostingEnvironment;
        }

        [HttpGet("danhsach/{pagesize?}/{pageindex?}")]
        [AllowAnonymous]

        public async Task<IActionResult> danhsachDiaDiemAnUong(CancellationToken ct, int pagesize = 10, int pageindex = 1)
        {

            var listResult = await Mediator.Send(new DiaDiemAnUongGets.Query { pagesize = pagesize, pageindex = pageindex }, ct);
            var result = new DanhSach<HoSoLuTruItemResponse>();
            result.TotalRows = 0;
            if (listResult.Value.Count > 0)
            {
                result.Data = listResult.Value;
                result.TotalRows = result.Data[0].TotalRows;
            }
            //return HandlerResult(listResult);
            return HandlerResult(Result<DanhSach<HoSoLuTruItemResponse>>.Success(result));
        }


        [HttpGet("chitiet/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> ChiTiet(int id)
        {
            var result = await Mediator.Send(new Application.HoSoApplication.HoSoChiTiet.Query { Id = id });
            return HandlerResult(result);
        }


        [HttpPost("ganvitridukhach")]
        [AllowAnonymous]

        public async Task<IActionResult> DanhSachDiemAnUongGanDuKhach(CancellationToken ct, [FromBody] Distance_Request _request)
        {
            var listResult = await Mediator.Send(new DiemAnUongGanViTriDuKhachGets.Query { infor = _request }, ct);
            var result = new DanhSach<HoSoLuTruItemResponse>();
            result.TotalRows = 0;
            if (listResult.Value.Count > 0)
            {
                result.Data = listResult.Value;
                result.TotalRows = result.Data[0].TotalRows;
            }
            //return HandlerResult(listResult);
            return HandlerResult(Result<DanhSach<HoSoLuTruItemResponse>>.Success(result));
        }

    }
}
