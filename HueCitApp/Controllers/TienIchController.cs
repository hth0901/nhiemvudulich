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
using Domain.RequestEntity;

namespace HueCitApp.Controllers
{
    public class TienIchController : BaseApiController
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public TienIchController(IWebHostEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
            _webHostEnvironment = hostingEnvironment;
        }
        [HttpGet("danhsachduongdaynong/{pagesize?}/{pageindex?}")]
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

        [HttpGet("{id}")]
        [AllowAnonymous]

        public async Task<IActionResult> ThongTinDuBao(CancellationToken ct, string id)
        {
            var listResult = await Mediator.Send(new DuBaoThoiTietGet.Query { ID = id }, ct);
            return HandlerResult(listResult);
        }

     
        [HttpGet("danhsachdiemvesinh/{pagesize?}/{pageindex?}")]
        [AllowAnonymous]

        public async Task<IActionResult> danhsachdiemvesinh(CancellationToken ct, int pagesize = 10, int pageindex = 1)
        {
            var listResult = await Mediator.Send(new DiemVeSinhGets.Query { pagesize = pagesize, pageindex = pageindex }, ct);
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
        [HttpPost("diemvesinh/ganvitridukhach")]
        [AllowAnonymous]

        public async Task<IActionResult> DanhSachDiemVeSinhGanDuKhach(CancellationToken ct, [FromBody] Distance_Request _request)
        {
            var listResult = await Mediator.Send(new DiemVeSinhGanViTriDuKhachGets.Query { infor = _request }, ct);
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
