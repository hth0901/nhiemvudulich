using Application.Core;
using Application.CoSoLuuTru;
using Domain.ResponseEntity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using Application.LoaiHinhDanhMucCoSoKhamChuaBenh;
using Application.DiaPhuong;

namespace HueCitApp.Controllers
{
    public class DiaPhuongController : BaseApiController
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public DiaPhuongController(IWebHostEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
            _webHostEnvironment = hostingEnvironment;
        }

        [HttpGet("diaphuong")]
        [AllowAnonymous]
        public async Task<IActionResult> DiaPhuongGets(CancellationToken ct)
        {
            var result = await Mediator.Send(new DiaPhuongGets.Query { }, ct);
            return HandlerResult(result);
        }

        [HttpGet("diaphuong/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> DiaPhuongGetsByparent(CancellationToken ct, int id)
        {
            var result = await Mediator.Send(new DiaPhuongGetsByParent.Query { ID = id }, ct);
            return HandlerResult(result);
        }
    }
}
