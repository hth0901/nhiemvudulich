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
using Application.LoaiMonAnThucUong;
using Application.MonAnThucUong;

namespace HueCitApp.Controllers
{
    public class AmThucHueController : BaseApiController

    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AmThucHueController(IWebHostEnvironment hostingEnvironment) : base(hostingEnvironment)
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
        [HttpGet("ganvitridukhach")]
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
        [HttpGet]
        [AllowAnonymous]

        public async Task<IActionResult> DanhSachLoaiMonAnThucUong(CancellationToken ct)
        {
            var listResult = await Mediator.Send(new LoaiMonAnThucUongGets.Query(), ct);
            //return HandlerResult(listResult);
            return HandlerResult(listResult);
        }
        [HttpGet("danhsach/{pagesize?}/{pageindex?}")]
        [AllowAnonymous]
        public async Task<IActionResult> DanhSachMonAnThucUong(CancellationToken ct, int pagesize = 10, int pageindex = 1)
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
        public async Task<IActionResult> ChiTietMonAnThucUong(int id, CancellationToken ct)
        {
            var listResult = await Mediator.Send(new ChiTietMonAnThucUongGet.Query { ID = id }, ct);
            return HandlerResult(listResult);

        }

    }
}
