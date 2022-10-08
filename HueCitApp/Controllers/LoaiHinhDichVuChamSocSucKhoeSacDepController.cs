using Application.LoaiHinhCoSoLuuTru;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using Application.LoaiHinhChamSocSucKhoeSacDep;

namespace HueCitApp.Controllers
{
    public class LoaiHinhDichVuChamSocSucKhoeSacDepController : BaseApiController
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public LoaiHinhDichVuChamSocSucKhoeSacDepController(IWebHostEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
            _webHostEnvironment = hostingEnvironment;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> DanhSachLoaiHinh(CancellationToken ct)
        {
            var listResult = await Mediator.Send(new LoaiHinhChamSocSucKhoeSacDepGets.Query(), ct);
            return HandlerResult(listResult);
        }
    }
}
