using Application.DiemVeSinh;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using Application.CoSoChamSocSucKhoeSacDep;

namespace HueCitApp.Controllers
{
    public class CoSoChamSocSucKhoeSacDepController : BaseApiController
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CoSoChamSocSucKhoeSacDepController(IWebHostEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
            _webHostEnvironment = hostingEnvironment;
        }
        [HttpGet]
        [AllowAnonymous]

        public async Task<IActionResult> cosochamsocsuckhoesacdep(CancellationToken ct)
        {
            var listResult = await Mediator.Send(new CoSoChamSocSucKhoeSacDepGets.Query(), ct);
            return HandlerResult(listResult);
        }
        [HttpGet("{id}")]
        [AllowAnonymous]

        public async Task<IActionResult> chitietcosochamsocsuckhoesacdep(int ID,CancellationToken ct)
        {
            var listResult = await Mediator.Send(new CoSoChamSocSucKhoeSacDepGet.Query { ID=ID}, ct);
            return HandlerResult(listResult);
        }
    }
}
