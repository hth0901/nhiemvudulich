using Application.LoaiHinhCoSoLuuTru;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.AspNetCore.Hosting;
using Application.HeThongSoHoaTinhDuLieuAnUongCapNhat;
using Domain.HueCit;
using Application.HeThongSoHoaTinhDuLieuMuaSamCapNhat;
using Domain.TechLife;
using Application.HeThongSoHoaTinhDuLieuTheThaoCapNhat;
using Application.HeThongSoHoaTinhDuLieuVuiChoiGiaiTriCapNhat;
using Domain.RequestEntity;

namespace HueCitApp.Controllers
{
    public class CapNhatDuLieuDichVuLenHeThongSoHoaController : BaseApiController
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CapNhatDuLieuDichVuLenHeThongSoHoaController(IWebHostEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
            _webHostEnvironment = hostingEnvironment;
        }

        [HttpPost("dulieuanuongadd")]
        [AllowAnonymous]
        public async Task<IActionResult> DuLieuAnUongAdd( [FromBody] HoSoRequestAdd infor)
        {
            var Result = await Mediator.Send(new HeThongSoHoaDuLieuAnUongAdd.Command{ infor=infor});

            //return HandlerResult(listResult);
            return HandlerResult(Result);



        }

        [HttpPut("dulieuanuongedit")]
        [AllowAnonymous]
        public async Task<IActionResult> DuLieuAnUongEdit([FromBody] HoSo infor)
        {
            var Result = await Mediator.Send(new HeThongSoHoaDuLieuAnUongEdit.Command{infor = infor});

            //return HandlerResult(listResult);
            return HandlerResult(Result);


            //upd1, add0
        }
        [HttpPost("dulieumuasamadd")]
        [AllowAnonymous]
        public async Task<IActionResult> DuLieuMuaSamAdd([FromBody] HoSoRequestAdd infor)
        {
            var Result = await Mediator.Send(new HeThongSoHoaDuLieuMuaSamAdd.Command { infor=infor});

            //return HandlerResult(listResult);
            return HandlerResult(Result);



        }
        [HttpPut("dulieumuasamedit")]
        [AllowAnonymous]
        public async Task<IActionResult> DuLieuMuaSamEdit([FromBody] HoSo infor)
        {
            var Result = await Mediator.Send(new HeThongSoHoaDuLieuMuaSamEdit.Command { infor=infor});

            //return HandlerResult(listResult);
            return HandlerResult(Result);



        }
        [HttpPost("dulieuthethaoadd")]
        [AllowAnonymous]
        public async Task<IActionResult> DuLieuTheThaoAdd([FromBody] HoSoRequestAdd infor)
        {
            var Result = await Mediator.Send(new HeThongSoHoaDuLieuTheThaoAdd.Command { infor=infor});

            //return HandlerResult(listResult);
            return HandlerResult(Result);



        }
        [HttpPut("dulieuthethaoedit")]
        [AllowAnonymous]
        public async Task<IActionResult> DuLieuTheThaoEdit([FromBody]  HoSo infor)
        {
            var Result = await Mediator.Send(new HeThongSoHoaDuLieuTheThaoEdit.Command { infor=infor});

            //return HandlerResult(listResult);
            return HandlerResult(Result);



        }
        [HttpPost("dulieuvuichoigiaitriadd")]
        [AllowAnonymous]
        public async Task<IActionResult> DuLieuVuiChoiGiaiTriAdd([FromBody] HoSoRequestAdd infor)
        {
            var Result = await Mediator.Send(new HeThongSoHoaDuLieuVuiChoiGiaiTriAdd.Command{ infor=infor});

            //return HandlerResult(listResult);
            return HandlerResult(Result);



        }
        [HttpPut("dulieuvuichoigiatriedit")]
        [AllowAnonymous]
        public async Task<IActionResult> DuLieuVuiChoiGiaiTriEdit([FromBody] HoSo infor)
        {
            var Result = await Mediator.Send(new HeThongSoHoaDuLieuVuiChoiGiaiTriEdit.Command { infor=infor});

            //return HandlerResult(listResult);
            return HandlerResult(Result);



        }
    }
    }

