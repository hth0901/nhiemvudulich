using Application.LoaiHinhCoSoLuuTru;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.AspNetCore.Hosting;
using Application.HeThongSoHoaTinhHuongDanVienCapNhat;
using Domain.ResponseEntity;
using Application.HeThongSoHoaTinhDoanhNghiepDaiLiLuHanhCapNhat;
using Domain.TechLife;
using Application.HeThongSoHoaTinhCoSoLuuTruDuLichCapNhat;
using Application.HeThongSoHoaTinhTaiLieuLienQuanCapNhat;
using Domain.HueCit;
using Domain.RequestEntity;

namespace HueCitApp.Controllers
{
    public class CapNhatDuLieuDuLichLenHeThongSoHoaController : BaseApiController
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CapNhatDuLieuDuLichLenHeThongSoHoaController(IWebHostEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
            _webHostEnvironment = hostingEnvironment;
        }

        [HttpPost("huongdanvienadd")]
        [AllowAnonymous]
        public async Task<IActionResult> HuongDanVienAdd( [FromBody] HuongDanVienDuLichRequestAdd infor)
        {
            var Result = await Mediator.Send(new HeThongSoHoaHuongDanVienAdd.Command { infor=infor});

            //return HandlerResult(listResult);
            return HandlerResult(Result);



        }

        [HttpPut("huongdanvienedit")]
        [AllowAnonymous]
        public async Task<IActionResult> HuongDanVienEdit([FromBody] Domain.HueCit.HuongDanVienDuLich infor)
        {
            var Result = await Mediator.Send(new HeThongSoHoaHuongDanVienEdit.Command { infor=infor});

            //return HandlerResult(listResult);
            return HandlerResult(Result);



        }
        [HttpPost("doanhnghiepluhanhadd")]
        [AllowAnonymous]
        public async Task<IActionResult> DoanhNghiepLuHanhAdd([FromBody] HoSoRequestAdd infor)
        {
            var Result = await Mediator.Send(new HeThongSoHoaDoanhNghiepDaiLiLuHanhAdd.Command { infor =infor});

            //return HandlerResult(listResult);
            return HandlerResult(Result);



        }
        [HttpPut("doanhnghiepluhanhedit")]
        [AllowAnonymous]
        public async Task<IActionResult> DoanhNghiepLuHanhEdit([FromBody] HoSo infor)
        {
            var Result = await Mediator.Send(new HeThongSoHoaDoanhNghiepDaiLiLuHanhEdit.Command { infor = infor });

            //return HandlerResult(listResult);
            return HandlerResult(Result);



        }
        [HttpPost("cosoluutruadd")]
        [AllowAnonymous]
        public async Task<IActionResult> CoSoLuuTruAdd([FromBody] HoSoRequestAdd infor)
        {
            var Result = await Mediator.Send(new HeThongSoHoaCoSoLuuTruAdd.Command { infor=infor});

            //return HandlerResult(listResult);
            return HandlerResult(Result);



        }
        [HttpPut("cosoluutruedit")]
        [AllowAnonymous]
        public async Task<IActionResult> CoSoLuuTruEdit([FromBody] HoSo infor)
        {
            var Result = await Mediator.Send(new HeThongSoHoaCoSoLuuTruEdit.Command { infor=infor});

            //return HandlerResult(listResult);
            return HandlerResult(Result);



        }
        [HttpPost("tailieulienquanadd")]
        [AllowAnonymous]
        public async Task<IActionResult> TaiLieuLienQuanAdd([FromBody] FileUploads infor)
        {
            var Result = await Mediator.Send(new HeThongSoHoaTaiLieuLienQuanAdd.Command { infor=infor});

            //return HandlerResult(listResult);
            return HandlerResult(Result);



        }
        [HttpPut("tailieulienquanedit")]
        [AllowAnonymous]
        public async Task<IActionResult> TaiLieuLienQuanEdit([FromBody] FileUploads infor)
        {
            var Result = await Mediator.Send(new HeThongSoHoaTaiLieuLienquanEdit.Command { infor = infor });

            //return HandlerResult(listResult);
            return HandlerResult(Result);

        }
    }
}
