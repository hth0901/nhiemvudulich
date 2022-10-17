using Application.Core;
using Application.CoSoChamSocSucKhoeSacDep;
using Domain.ResponseEntity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using Application.HuongDanVienDuLich;
using Application.SuKien;
using Domain.HueCit;

namespace HueCitApp.Controllers
{
    public class HuongDanVienDuLichController : BaseApiController
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public HuongDanVienDuLichController(IWebHostEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
            _webHostEnvironment = hostingEnvironment;
        }
        [HttpGet("danhsach/{pagesize?}/{pageindex?}")]
        [AllowAnonymous]

        public async Task<IActionResult> danhsachhuongdanviendulich(CancellationToken ct, int pagesize = 10, int pageindex = 1)
        {
            var listResult = await Mediator.Send(new HuongDanVienDuLichGets.Query { pagesize = pagesize, pageindex = pageindex }, ct);
            var result = new DanhSach<HuongDanVienDuLichItemResponse>();
            result.TotalRows = 0;
            if (listResult.Value.Count > 0)
            {
                result.Data = listResult.Value;
                result.TotalRows = result.Data[0].TotalRows;
            }
            //return HandlerResult(listResult);
            return HandlerResult(Result<DanhSach<HuongDanVienDuLichItemResponse>>.Success(result));

        }
        [HttpGet("{id}")]
        [AllowAnonymous]

        public async Task<IActionResult> ChiTietHuongDanVienDuLich(CancellationToken ct, int ID)
        {
            var listResult = await Mediator.Send(new HuongDanVienDuLichGet.Query { ID = ID }, ct);
            return HandlerResult(listResult);

        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> AddDoiTuong([FromBody] HuongDanVienDuLich _request)
        {
            var result = await Mediator.Send(new HuongDanVienDuLichCreate.Command { infor = _request });
            return HandlerResult(result);
        }
        [HttpPut]   
        [AllowAnonymous]
        public async Task<IActionResult> EditDoiTuong([FromBody] HuongDanVienDuLich _request)
        {
            var result = await Mediator.Send(new HuongDanVienDuLichEdit.Command { infor = _request });
            return HandlerResult(result);
        }
    }
}
