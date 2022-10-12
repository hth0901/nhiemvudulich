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

namespace HueCitApp.Controllers
{
    public class CoSoLuuTruController : BaseApiController
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CoSoLuuTruController(IWebHostEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
            _webHostEnvironment = hostingEnvironment;
        }
        [HttpGet("danhsachhosoluutru/{pagesize?}/{pageindex?}")] 
        [AllowAnonymous]
        public async Task<IActionResult> DanhSachCoSoLuuTru(CancellationToken ct, int pagesize = 10, int pageindex = 1)
        {
            var listResult = await Mediator.Send(new CoSoLuuTruGets.Query { pagesize = pagesize, pageindex = pageindex }, ct);
            var result = new DanhSachHoSoLuTruResponse();
            result.TotalRows = 0;
            if (listResult.Value.Count > 0)
            {
                result.Data = listResult.Value;
                result.TotalRows = result.Data[0].TotalRows;
            }
            //return HandlerResult(listResult);
            return HandlerResult(Result<DanhSachHoSoLuTruResponse>.Success(result));
        }
        [HttpGet("{ID}")]
        [AllowAnonymous]
        public async Task<IActionResult> ChiTietCoSoLuuTru(int ID,CancellationToken ct)
        {
            var listResult = await Mediator.Send(new ChiTietCoSoLuuTruGet.Query { ID = ID }, ct);
            return HandlerResult(listResult);
        }
    }
}
