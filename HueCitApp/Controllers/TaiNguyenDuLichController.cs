using Application.Core;
using Application.DiemVeSinh;
using Domain.ResponseEntity;
using Domain.HueCit;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using Application.DoanhNghiep;
using Domain.TechLife;
using Application.SuKien;
using Application.TaiNguyenDuLich;

namespace HueCitApp.Controllers
{
    public class TaiNguyenDuLichController: BaseApiController
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public TaiNguyenDuLichController(IWebHostEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
            _webHostEnvironment = hostingEnvironment;
        }
        [HttpPost("themmoitainguyen")]
        [AllowAnonymous]

        public async Task<IActionResult> ThemMoiTaiNguyenDuLich(CancellationToken ct, [FromBody] DL_BangTaiNguyen _request)
        {
            var result = await Mediator.Send(new ThemMoiTaiNguyen.Command { _tainguyen = _request },ct);

            return HandlerResult(result);

        }
        [HttpPost("capnhattainguyen")]
        [AllowAnonymous]
        public async Task<IActionResult> CapNhatTaiNguyenDuLich(CancellationToken ct, [FromBody] DL_BangTaiNguyen _request)
        {
            var result = await Mediator.Send(new CapNhatTaiNguyen.Command { _tainguyen = _request }, ct);

            return HandlerResult(result);
        }
    }
}
