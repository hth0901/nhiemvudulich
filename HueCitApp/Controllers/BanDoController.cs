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
    }
}
