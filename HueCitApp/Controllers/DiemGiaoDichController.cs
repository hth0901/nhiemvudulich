﻿using Application.DiaDiemDuLich;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using Application.DiemGiaoDich;
using Domain.RequestEntity;

namespace HueCitApp.Controllers
{
    public class DiemGiaoDichController : BaseApiController
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public DiemGiaoDichController(IWebHostEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
            _webHostEnvironment = hostingEnvironment;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> DanhSachDiemGiaoDich(CancellationToken ct)
        {
            var listResult = await Mediator.Send(new DiemGiaoDichGets.Query(), ct);
            return HandlerResult(listResult);
        }
        [HttpGet("danhsachnganhang")]
        [AllowAnonymous]

        public async Task<IActionResult> DanhSachNganHang(CancellationToken ct)
        {
            var listResult = await Mediator.Send(new DiemGiaoDichNganHangGets.Query(), ct);
            return HandlerResult(listResult);
        }
        [HttpGet("danhsachnganhangdiaban")]
        [AllowAnonymous]

        public async Task<IActionResult> DanhSachNganHangDiaBan(CancellationToken ct, [FromBody] Place_Request _request)
        {
            var listResult = await Mediator.Send(new DiemGiaoDichNganHangDiaBanGets.Query { _request=_request}, ct);
            return HandlerResult(listResult);
        }

    }
}
