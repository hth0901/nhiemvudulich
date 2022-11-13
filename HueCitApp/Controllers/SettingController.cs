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
using Domain.HueCit;

namespace HueCitApp.Controllers
{
    public class SettingController : BaseApiController
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public SettingController(IWebHostEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
            _webHostEnvironment = hostingEnvironment;
        }

        [HttpGet("danhsach")] 
        [AllowAnonymous]
        public async Task<IActionResult> SettingGets(CancellationToken ct)
        {
            var result = await Mediator.Send(new SettingGets.Query { }, ct);

            return HandlerResult(result);
        }

        [HttpPut("capnhat")]
        [AllowAnonymous]
        public async Task<IActionResult> UserEdit(CancellationToken ct, [FromBody] SYS_SettingRequest request)
        {
            var result = await Mediator.Send(new SettingEdits.Command { Request = request }, ct);

            return HandlerResult(result);
        }
    }
}
