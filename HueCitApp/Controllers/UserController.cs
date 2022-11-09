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
    public class UserController : BaseApiController
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public UserController(IWebHostEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
            _webHostEnvironment = hostingEnvironment;
        }

        [HttpPost("nguoidung")] 
        [AllowAnonymous]
        public async Task<IActionResult> UserGets(CancellationToken ct, UserRequest request)
        {
            var result = await Mediator.Send(new UserGets.Query {
                TuKhoa = request.TuKhoa,
                Quyen = request.Quyen,
            }, ct);

            return HandlerResult(result);
        }

        [HttpPost("nguoidungadd")]
        [AllowAnonymous]
        public async Task<IActionResult> UserAdd(CancellationToken ct, [FromBody] SYS_User request)
        {
            var result = await Mediator.Send(new UserAdd.Command { Request = request }, ct);

            return HandlerResult(result);
        }

        [HttpPut("nguoidungedit")]
        [AllowAnonymous]
        public async Task<IActionResult> Useredit(CancellationToken ct, [FromBody] SYS_User request)
        {
            var result = await Mediator.Send(new UserEdit.Command { Request = request }, ct);

            return HandlerResult(result);
        }

        [HttpDelete("nguoidungdelete/{request}")]
        [AllowAnonymous]
        public async Task<IActionResult> UserDelete(CancellationToken ct, int request)
        {
            var result = await Mediator.Send(new UserDelete.Command { Request = request }, ct);

            return HandlerResult(result);
        }

        [HttpPost("vaitro")]
        [AllowAnonymous]
        public async Task<IActionResult> RoleGets(CancellationToken ct, RoleRequest request)
        {
            var result = await Mediator.Send(new RoleGets.Query { TuKhoa = request.TuKhoa, Used = request.Used }, ct);

            return HandlerResult(result);
        }

        [HttpPost("vaitroadd")]
        [AllowAnonymous]
        public async Task<IActionResult> RoleAdd(CancellationToken ct, [FromBody] SYS_Roles request)
        {
            var result = await Mediator.Send(new RoleAdd.Command { Request = request }, ct);

            return HandlerResult(result);
        }

        [HttpPut("vaitroedit")]
        [AllowAnonymous]
        public async Task<IActionResult> RoleEdit(CancellationToken ct, [FromBody] SYS_Roles request)
        {
            var result = await Mediator.Send(new RoleEdit.Command { Request = request }, ct);

            return HandlerResult(result);
        }

        [HttpDelete("vaitrodelete/{request}")]
        [AllowAnonymous]
        public async Task<IActionResult> RoleDelete(CancellationToken ct, int request)
        {
            var result = await Mediator.Send(new RoleDelete.Command { Request = request }, ct);

            return HandlerResult(result);
        }
    }
}
