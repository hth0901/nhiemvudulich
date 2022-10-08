using Application.LoaiHinhCoSoLuuTru;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using Application.CoSoLuuTru;

namespace HueCitApp.Controllers
{
    public class CoSoLuuTruController : BaseApiController
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CoSoLuuTruController(IWebHostEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
            _webHostEnvironment = hostingEnvironment;
        }
        [HttpGet] 
        [AllowAnonymous]
        public async Task<IActionResult> DanhSachCoSoLuuTru(CancellationToken ct)
        {
            var listResult = await Mediator.Send(new CoSoLuuTruGets.Query(), ct);
            return HandlerResult(listResult);
        }
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> ChiTietCoSoLuuTru(int ID,CancellationToken ct)
        {
            var listResult = await Mediator.Send(new ChiTietCoSoLuuTruGet.Query { ID=ID}, ct);
            return HandlerResult(listResult);
        }
    }
}
