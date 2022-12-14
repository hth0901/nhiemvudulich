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
using Domain;
using HueCitApp.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HueCitApp.Controllers
{
    public class UserController : BaseApiController
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public UserController(IWebHostEnvironment hostingEnvironment, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager) : base(hostingEnvironment)
        {
            _webHostEnvironment = hostingEnvironment;
            _userManager = userManager;
            _signInManager = signInManager;
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
            if (await _userManager.Users.AnyAsync(x => x.Email == request.HopThu))
            {
                return BadRequest("Email đã tồn tại!");
            }

            if (await _userManager.Users.AnyAsync(x => x.UserName == request.TenDangNhap))
            {
                return BadRequest("Tên tài khoản đã tồn tại!");
            }

            var user = new AppUser
            {
                DisplayName = request.HoTen,
                Email = request.HopThu,
                UserName = request.TenDangNhap
            };

            var result = await _userManager.CreateAsync(user, request.MatKhau);
            if (result.Succeeded)
            {
                request.MatKhau = "";
                var res = await Mediator.Send(new UserAdd.Command { Request = request }, ct);
                return HandlerResult(res);
            }
            else
            {
                return BadRequest("Mật khẩu không thỏa mãn yêu cầu!!");
            }
        }

        [HttpPut("nguoidungedit")]
        [AllowAnonymous]
        public async Task<IActionResult> UserEdit(CancellationToken ct, [FromBody] SYS_User request)
        {
            var result = await Mediator.Send(new UserEdit.Command { Request = request }, ct);

            return HandlerResult(result);
        }

        [HttpDelete("nguoidungdelete/{request}")]
        [AllowAnonymous]
        public async Task<IActionResult> UserDelete(CancellationToken ct, int request)
        {
            var u = await Mediator.Send(new UserGetById.Query { Id = request }, ct);
            if (u != null)
            {
                var user = await _userManager.FindByNameAsync(u.Value.TenDangNhap);
                if (user != null)
                {
                    await _userManager.DeleteAsync(user);
                    var result = await Mediator.Send(new UserDelete.Command { Request = request }, ct);
                    return HandlerResult(result);
                }
                else
                {
                    return BadRequest("Không tìm thấy người dùng!");
                }
            }
            else
            {
                return BadRequest("Không tìm thấy người dùng!");
            }
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

        [HttpGet("phanquyen/{request}")]
        [AllowAnonymous]
        public async Task<IActionResult> PhanQuyenGet(CancellationToken ct, int request)
        {
            var result = await Mediator.Send(new PhanQuyenGet.Query
            {
                Role = request
            }, ct);

            return HandlerResult(result);
        }

        [HttpPost("phanquyen")]
        [AllowAnonymous]
        public async Task<IActionResult> PhanQuyen(CancellationToken ct, [FromBody] SYS_PhanQuyenRequest request)
        {
            var result = await Mediator.Send(new PhanQuyenRole.Command
            {
                Request = request
            }, ct);

            return HandlerResult(result);
        }

        [HttpGet("menu")]
        [AllowAnonymous]
        public async Task<IActionResult> MenuGets(CancellationToken ct)
        {
            var result = await Mediator.Send(new MenuGets.Query
            {
            }, ct);

            return HandlerResult(result);
        }
    }
}
