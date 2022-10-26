using Application.LoaiHinhCoSoLuuTru;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using Application.CoSoLuuTru;
using Domain.ResponseEntity;
using Application.Core;
using Application.DiaDiemDuLich;
using Domain.RequestEntity;

namespace HueCitApp.Controllers
{
    public class CoSoLuuTruDuLichController : BaseApiController
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CoSoLuuTruDuLichController(IWebHostEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
            _webHostEnvironment = hostingEnvironment;
        }
        [HttpGet("danhsachcosoluutru/{pagesize?}/{pageindex?}")] 
        [AllowAnonymous]
        public async Task<IActionResult> DanhSachCoSoLuuTru(CancellationToken ct, int pagesize = 10, int pageindex = 1)
        {
            var listResult = await Mediator.Send(new CoSoLuuTruGets.Query { pagesize = pagesize, pageindex = pageindex }, ct);
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
        [HttpGet("{ID}")]
        [AllowAnonymous]
        public async Task<IActionResult> ChiTietCoSoLuuTru(int ID,CancellationToken ct)
        {
            var listResult = await Mediator.Send(new ChiTietCoSoLuuTruGet.Query { ID = ID }, ct);
            return HandlerResult(listResult);
        }

        [HttpGet("ganvitridukhach")]
        [AllowAnonymous]

        public async Task<IActionResult> DanhSachCoSoLuuTruGanDuKhach(CancellationToken ct, [FromBody] Distance_Request _request)
        {
            var listResult = await Mediator.Send(new CoSoLuuTruGanViTriDuKhachGets.Query { infor = _request }, ct);
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
        public async Task<IActionResult> DanhSachLoaiHinhLuuTru(CancellationToken ct)
        {
            var listResult = await Mediator.Send(new LoaiHinhCoSoLuuTruGets.Query(), ct);

            //return HandlerResult(listResult);
            return HandlerResult(listResult);



        }

    }
}
