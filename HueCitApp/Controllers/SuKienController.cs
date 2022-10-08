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

        [HttpGet("theothang")]
        [AllowAnonymous]

        public async Task<IActionResult> DanhSachSuKienTheoThang(CancellationToken ct)
        {
            var listResult = await Mediator.Send(new SuKienThangGets.Query(), ct);
            return HandlerResult(listResult);

        }
        [HttpGet("theothang/theochude={id}")]
        [AllowAnonymous]

        public async Task<IActionResult> DanhSachSuKienThangTheoChuDe(CancellationToken ct, int ID)
        {
            var listResult = await Mediator.Send(new SuKienThangTheoChuDeGets.Query { IDChuDe = ID }, ct);
            return HandlerResult(listResult);

        }
        [HttpGet("{id}")]
        [AllowAnonymous]
      
      
        public async Task<IActionResult> ChiTietSuKien(CancellationToken ct,int ID)
        {
            var listResult = await Mediator.Send(new ChiTietSuKienGet.Query{ IDSuKien=ID}, ct);
            return HandlerResult(listResult);

        }



    }
}
