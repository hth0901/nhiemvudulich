using Application.CoSoLuuTru;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using Application.DichVuVanChuyen;
using Application.Core;
using Application.CoSoChamSocSucKhoeSacDep;
using Domain.ResponseEntity;
using System.Drawing.Printing;

namespace HueCitApp.Controllers
{
    public class CoSoKinhDoanhDichVuVanChuyenController : BaseApiController
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CoSoKinhDoanhDichVuVanChuyenController(IWebHostEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
            _webHostEnvironment = hostingEnvironment;
        }
        [HttpGet("danhsach/{pagesize?}/{pageindex?}")]
        [AllowAnonymous]
        public async Task<IActionResult> DanhSachCoSoKinhDoanhDichVuVanChuyen(CancellationToken ct, int pagesize = 10, int pageindex = 1)
        {

            var listResult = await Mediator.Send(new CoSoDichVuVanChuyenGets.Query { pagesize = pagesize, pageindex = pageindex }, ct);
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


        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> ChiTietCoSoKinhDoanhDichVuVanChuyen(int ID,CancellationToken ct)
        {
            var listResult = await Mediator.Send(new CoSoDichVuVanChuyenGet.Query { ID=ID}, ct);
            return HandlerResult(listResult);
        }
    }
}
