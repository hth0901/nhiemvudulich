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
using Domain.RequestEntity;

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

        public async Task<IActionResult> ThemMoiTaiNguyenDuLich(CancellationToken ct, [FromBody] HoSoRequestAdd _request)
        {
            var result = await Mediator.Send(new ThemMoiTaiNguyen.Command { infor = _request },ct);

            return HandlerResult(result);

        }
        [HttpPost("capnhattainguyen")]
        [AllowAnonymous]
        public async Task<IActionResult> CapNhatTaiNguyenDuLich(CancellationToken ct, [FromBody] HoSo _request)
        {
            var result = await Mediator.Send(new CapNhatTaiNguyen.Command { infor = _request }, ct);

            return HandlerResult(result);
        }
    }
}
