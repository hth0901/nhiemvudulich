using Application.DiaDiemDuLich;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using Application.DiemGiaoDich;
using Domain.RequestEntity;
using Application.Core;
using Application.DiaDiemMuaSamGiaiTri;
using Domain.ResponseEntity;
using System.Drawing.Printing;

namespace HueCitApp.Controllers
{
    public class DiemGiaoDichController : BaseApiController
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public DiemGiaoDichController(IWebHostEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
            _webHostEnvironment = hostingEnvironment;
        }
        [HttpGet("danhsach/{pagesize?}/{pageindex?}")]
        [AllowAnonymous]
        public async Task<IActionResult> DanhSachDiemGiaoDich(CancellationToken ct, int pagesize = 10, int pageindex = 1)
        {

            var listResult = await Mediator.Send(new DiemGiaoDichGets.Query { pagesize = pagesize, pageindex = pageindex }, ct);
            var result = new DanhSach<DiemGiaoDichItemResponse>();
            result.TotalRows = 0;
            if (listResult.Value.Count > 0)
            {
                result.Data = listResult.Value;
                result.TotalRows = result.Data[0].TotalRows;
            }
            //return HandlerResult(listResult);
            return HandlerResult(Result<DanhSach<DiemGiaoDichItemResponse>>.Success(result));
          
        }
        [HttpGet("danhsachnganhang/{pagesize?}/{pageindex?}")]
        [AllowAnonymous]

        public async Task<IActionResult> DanhSachNganHang(CancellationToken ct, int pagesize = 10, int pageindex = 1)
        {   
            var listResult = await Mediator.Send(new DiemGiaoDichNganHangGets.Query { pagesize = pagesize, pageindex = pageindex }, ct);
            var result = new DanhSach<DiemGiaoDichItemResponse>();
            result.TotalRows = 0;
            if (listResult.Value.Count > 0)
            {
                result.Data = listResult.Value;
                result.TotalRows = result.Data[0].TotalRows;
            }
            //return HandlerResult(listResult);
            return HandlerResult(Result<DanhSach<DiemGiaoDichItemResponse>>.Success(result));
        }
        [HttpGet("danhsachnganhangdiaban")]
        [AllowAnonymous]

        public async Task<IActionResult> DanhSachNganHangDiaBan(CancellationToken ct, [FromBody] Place_Request _request)
        {
            var listResult = await Mediator.Send(new DiemGiaoDichNganHangDiaBanGets.Query { _request=_request}, ct);
            return HandlerResult(listResult);
        }
        [HttpGet("ganvitridukhach")]
        [AllowAnonymous]

        public async Task<IActionResult> DanhSachDiemGiaoDichGanDuKhach(CancellationToken ct, [FromBody] Distance_Request _request)
        {
            var listResult = await Mediator.Send(new DiemGiaoDichGanViTriDuKhach.Query {infor=_request  }, ct);
            var result = new DanhSach<DiemGiaoDichItemResponse>();
            result.TotalRows = 0;
            if (listResult.Value.Count > 0)
            {
                result.Data = listResult.Value;
                result.TotalRows = result.Data[0].TotalRows;
            }
            //return HandlerResult(listResult);
            return HandlerResult(Result<DanhSach<DiemGiaoDichItemResponse>>.Success(result));
        }

        [HttpGet("get/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> DiemGiaoDichGet(CancellationToken ct, int id)
        {
            var listResult = await Mediator.Send(new DiemGiaoDichGet.Query { ID = id }, ct);
            
            return HandlerResult(listResult);
        }
    }
}
