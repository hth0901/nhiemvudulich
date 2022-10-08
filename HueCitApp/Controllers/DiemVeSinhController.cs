using Application.DiemGiaoDich;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using Application.DiemVeSinh;

namespace HueCitApp.Controllers
{
    public class DiemVeSinhController : BaseApiController
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public DiemVeSinhController(IWebHostEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
            _webHostEnvironment = hostingEnvironment;
        }
        [HttpGet]
        [AllowAnonymous]
    
        public async Task<IActionResult> danhsachdiemvesinh(CancellationToken ct)
        {
            var listResult = await Mediator.Send(new DiemVeSinhGets.Query(), ct);
            return HandlerResult(listResult);
        }
    }
}

