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
using Application.LoaiCoSoVuiChoiGiaiTri;
using Application.DiaDiemMuaSamVuiChoiGiaiTri;

namespace HueCitApp.Controllers
{
    public class MuaSamVuiChoiGiaiTriController : BaseApiController
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public MuaSamVuiChoiGiaiTriController(IWebHostEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
            _webHostEnvironment = hostingEnvironment;
        }
        [HttpGet("danhsach/{pagesize?}/{pageindex?}")]
        [AllowAnonymous]

        public async Task<IActionResult> DanhSachDiaDiemMuaSamVuiChoiGiaiTri(CancellationToken ct, int pagesize = 10, int pageindex = 1)
        {
            var listResult = await Mediator.Send(new DiaDiemMuaSamVuiChoiGiaiTriGets.Query { pagesize = pagesize, pageindex = pageindex }, ct);
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
        [HttpPost("ganvitridukhach")]
        [AllowAnonymous]

        public async Task<IActionResult> DiemMuaSamGiaiTriGanDuKhach(CancellationToken ct, [FromBody] Distance_Request _request)
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
        [HttpGet("loaihinh")]
        [AllowAnonymous]
        public async Task<IActionResult> DanhSachLoaiHinhCoSoVuiChoiGiaiTri(CancellationToken ct)
        {
            var listResult = await Mediator.Send(new LoaiHinhCoSoVuiChoiGiaiTriGets.Query(), ct);

            //return HandlerResult(listResult);
            return HandlerResult(listResult);
        }
        [HttpGet("vuichoigiaitri/{pagesize?}/{pageindex?}")]
        [AllowAnonymous]

        public async Task<IActionResult> DanhSachDiaDiemVuiChoiGiaiTri(CancellationToken ct, int pagesize = 10, int pageindex = 1)
        {
            var listResult = await Mediator.Send(new DiaDiemVuiChoiGiaiTriGets.Query { pagesize = pagesize, pageindex = pageindex }, ct);
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
