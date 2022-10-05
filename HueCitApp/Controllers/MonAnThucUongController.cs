using Application.DiaDiemDuLich;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.AspNetCore.Hosting;
using Application.MonAnThucUong;

namespace HueCitApp.Controllers
{
    public class MonAnThucUongController :BaseApiController
    {
        public MonAnThucUongController(IWebHostEnvironment hostingEnvironment) : base(hostingEnvironment)
        {//run on hosting
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> DanhSachMonAnThucUong(CancellationToken ct)
        {
            var listResult = await Mediator.Send(new DanhSachMonAnThucUong.Query(), ct);
            return HandlerResult(listResult);

        }
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> ChiTietMonAnThucUong(int id,CancellationToken ct)
        {
            var listResult = await Mediator.Send(new ChiTietMonAnThucUong.Query { ID=id}, ct);
            return HandlerResult(listResult);

        }
    }
}
