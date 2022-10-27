using Application.DuBaoThoiTiet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using Application.DiaDiemMuaSamGiaiTri;
using Application.Core;
using Application.DiaDiemDuLich;
using Domain.ResponseEntity;
using Application.DiemGiaoDich;
using Domain.RequestEntity;

namespace HueCitApp.Controllers
{
    public class DiaDiemMuaSamVuiChoiGiaiTriController : BaseApiController
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public DiaDiemMuaSamVuiChoiGiaiTriController(IWebHostEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
            _webHostEnvironment = hostingEnvironment;
        }
        [HttpGet("danhsach/{pagesize?}/{pageindex?}")]
        [AllowAnonymous]

        public async Task<IActionResult> DanhSachVuiChoiGiaiTri(CancellationToken ct, int pagesize = 10, int pageindex = 1)
        {
            var listResult = await Mediator.Send(new DiaDiemMuaSamGiaiTriGets.Query { pagesize = pagesize, pageindex = pageindex }, ct);
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

        public async Task<IActionResult> DanhSachDiemGiaoDichGanDuKhach(CancellationToken ct, [FromBody] Distance_Request _request)
        {
            var listResult = await Mediator.Send(new DiaDiemMuaSamGiaiTriGanViTriDuKhachGets.Query { infor = _request }, ct);
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
