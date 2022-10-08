using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HueCitApp.Controllers
{
    public class TestController : BaseApiController
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        public TestController(IWebHostEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("danhsach")]
        public async Task<IActionResult> danhsach()
        {
            var lstResult = await Mediator.Send(new Application.Activity.DanhSach.Query());
            return HandlerResult(lstResult);
        }
    }
}
