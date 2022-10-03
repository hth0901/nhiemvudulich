using Application.DiaDiemDuLich;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using Application.SuKien;
using Microsoft.AspNetCore.Hosting;

namespace HueCitApp.Controllers
{
    public class SuKienController : BaseApiController
    {
        public SuKienController(IWebHostEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("listofmonthly")]
        public async Task<IActionResult> ListOfMonthly(CancellationToken ct)
        {
            var listResult = await Mediator.Send(new DanhSachSuKienThang.Query(), ct);
            return HandlerResult(listResult);

        }
        [HttpGet]
        [AllowAnonymous]
        [Route("listofmonthlybytopic/{id}")]//
        public async Task<IActionResult> ListOfMonthlyByTopic(CancellationToken ct, int ID)
        {
            var listResult = await Mediator.Send(new DanhSachSuKienThangTheoChuDe.Query { IDChuDe=ID}, ct);
            return HandlerResult(listResult);

        }
        [HttpGet]
        [AllowAnonymous]
        [Route("Detail/{id}")]
        public async Task<IActionResult> Detail(CancellationToken ct,int ID)
        {
            var listResult = await Mediator.Send(new ChiTietSuKien.Query{ IDSuKien=ID}, ct);
            return HandlerResult(listResult);

        }



    }
}
