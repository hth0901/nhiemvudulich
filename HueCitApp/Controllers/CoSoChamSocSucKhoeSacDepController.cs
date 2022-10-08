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

        public async Task<IActionResult> danhsachdiemvesinh(CancellationToken ct)
        {
            var listResult = await Mediator.Send(new  DanhSachCoSoChamSocSucKhoeSacDep.Query(), ct);
            return HandlerResult(listResult);
        }
    }
}
