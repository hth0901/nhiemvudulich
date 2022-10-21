using Application.DiemVeSinh;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using Application.CoSoChamSocSucKhoeSacDep;
using Application.Core;
using Application.CoSoLuuTru;
using Domain.ResponseEntity;
using System.Drawing.Printing;
using Application.CoSoKhamChuaBenh;
using Domain.RequestEntity;

namespace HueCitApp.Controllers
{
    public class CoSoChamSocSucKhoeSacDepController : BaseApiController
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CoSoChamSocSucKhoeSacDepController(IWebHostEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
            _webHostEnvironment = hostingEnvironment;
        }
        [HttpGet("danhsach/{pagesize?}/{pageindex?}")]
        [AllowAnonymous]

        public async Task<IActionResult> cosochamsocsuckhoesacdep(CancellationToken ct, int pagesize = 10, int pageindex = 1)
        {
            var listResult = await Mediator.Send(new CoSoChamSocSucKhoeSacDepGets.Query { pagesize = pagesize, pageindex = pageindex }, ct);
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

        public async Task<IActionResult> chitietcosochamsocsuckhoesacdep(int ID,CancellationToken ct)
        {
            var listResult = await Mediator.Send(new CoSoChamSocSucKhoeSacDepGet.Query { ID=ID}, ct);
            return HandlerResult(listResult);
        }
        [HttpGet("ganvitridukhach")]
        [AllowAnonymous]

        public async Task<IActionResult> DanhSachCoSoChamSocSucKhoeSacDepGanDuKhach(CancellationToken ct, [FromBody] Distance_Request _request)
        {
            var listResult = await Mediator.Send(new CoSoChamSocSucKhoeSacDepGanViTriDuKhachGets.Query { infor = _request }, ct);
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
