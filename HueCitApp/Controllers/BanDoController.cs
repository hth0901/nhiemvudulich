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
using Application.BanDo;
using System.Collections.Generic;
using Domain.TechLife;

namespace HueCitApp.Controllers
{
    public class BanDoController : BaseApiController
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public BanDoController(IWebHostEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
            _webHostEnvironment = hostingEnvironment;
        }

        [HttpPost("hs")] 
        [AllowAnonymous]
        public async Task<IActionResult> HoSoFilter(CancellationToken ct, HoSoRequest request)
        {
            var result = await Mediator.Send(new HoSoQuery.Query {
                LinhVucId = request.LinhVucId,
                HangSao = request.HangSao,
                LoaiHinhId = request.LoaiHinhId,
                LoaiDiaDiemAnUong = request.LoaiDiaDiemAnUong,
                TienNghi = request.TienNghi,
                LoaiPhong = request.LoaiPhong,
            }, ct);

            return HandlerResult(result);
        }

        [HttpGet("tn/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> TienNghiGetsTheoLinhVuc(CancellationToken ct, int id)
        {
            var result = await Mediator.Send(new TienNghiGets.Query { LinhVucId = id }, ct);

            return HandlerResult(result);
        }

        [HttpGet("lp")]
        [AllowAnonymous]
        public async Task<IActionResult> LoaiPhongGets(CancellationToken ct)
        {
            var result = await Mediator.Send(new LoaiPhongGets.Query { }, ct);

            return HandlerResult(result);
        }

        [HttpGet("lvpa")]
        [AllowAnonymous]
        public async Task<IActionResult> LinhVucPhanAnhGets(CancellationToken ct)
        {
            var result = await Mediator.Send(new LinhVucPhanAnhGets.Query { }, ct);

            return HandlerResult(result);
        }

        [HttpGet("llh")]
        [AllowAnonymous]
        public async Task<IActionResult> LoaiLeHoiGets(CancellationToken ct)
        {
            var result = await Mediator.Send(new LoaiLeHoiGets.Query { }, ct);

            return HandlerResult(result);
        }

        [HttpGet("setting")]
        [AllowAnonymous]
        public async Task<IActionResult> SettingBanDoGets(CancellationToken ct)
        {
            var result = await Mediator.Send(new BanDoSettingGets.Query { }, ct);

            return HandlerResult(result);
        }

        [HttpPost("ghs")]
        [AllowAnonymous]
        public async Task<IActionResult> HoSoGet(CancellationToken ct, HoSoGetRequest request)
        {
            var result = await Mediator.Send(new BanDoDataGet.Query
            {
                LinhVuc = request.LinhVuc,
                ID = request.Id,
            }, ct);

            return HandlerResult(result);
        }

        [HttpGet("lehoiget/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> LeHoiGet(CancellationToken ct, string id)
        {
            var result = await Mediator.Send(new LeHoiGet.Query{ ID = id, }, ct);

            return HandlerResult(result);
        }

    }
}
