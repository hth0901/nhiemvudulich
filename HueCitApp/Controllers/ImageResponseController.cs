using Application.HuongDanVienDuLich;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using Application.Image_Url;

namespace HueCitApp.Controllers
{

    public class ImageResponseController : BaseApiController
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ImageResponseController(IWebHostEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
            _webHostEnvironment = hostingEnvironment;
        }

        [HttpGet("imagefileupload/{ID}")]
        [AllowAnonymous]

        public async Task<IActionResult> ImageFileUploadGet (CancellationToken ct, int ID)
        {
            var listResult = await Mediator.Send(new Image_FileUploadGet.Query { Id = ID }, ct);
            return HandlerResult(listResult);

        }
        [HttpGet("imagethuvienmedia/{IDDoiTuong}/{LoaiDoiTuong}")]
        [AllowAnonymous]

        public async Task<IActionResult> ImageThuVienMediaGet(CancellationToken ct, string IDDoiTuong, string LoaiDoiTuong)
        {
            var listResult = await Mediator.Send(new Image_ThuVienMediaGet.Query { IDDoiTuong = IDDoiTuong,LoaiDoiTuong=LoaiDoiTuong }, ct);
            return HandlerResult(listResult);
        }
    }
}
