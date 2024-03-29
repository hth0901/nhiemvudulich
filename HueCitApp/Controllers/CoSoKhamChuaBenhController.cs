﻿using Application.Core;
using Application.CoSoChamSocSucKhoeSacDep;
using Application.LoaiHinhChamSocSucKhoeSacDep;
using Domain.RequestEntity;
using Domain.ResponseEntity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using Application.CoSoKhamChuaBenh;
using Application.LoaiHinhDanhMucCoSoKhamChuaBenh;

namespace HueCitApp.Controllers
{
    public class CoSoKhamChuaBenhController : BaseApiController
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CoSoKhamChuaBenhController(IWebHostEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
            _webHostEnvironment = hostingEnvironment;
        }
        [HttpGet("danhsach/{pagesize?}/{pageindex?}")]
        [AllowAnonymous]

        public async Task<IActionResult> CoSoKhamChuaBenh(CancellationToken ct, int pagesize = 10, int pageindex = 1)
        {
            var listResult = await Mediator.Send(new CoSoKhamChuaBenhGets.Query { pagesize = pagesize, pageindex = pageindex }, ct);
            var result = new DanhSach<HoSoLuTruItemResponse>();
            result.TotalRows = 0;
            if (listResult.Value.Count > 0)
            {
                result.Data = listResult.Value;
                result.TotalRows = result.Data[0].TotalRows;
            }
            //return HandlerResult(listResult);
            return HandlerResult(Result<DanhSach<HoSoLuTruItemResponse>>.Success(result));

        }
        [HttpPost("ganvitridukhach")]
        [AllowAnonymous]

        public async Task<IActionResult> DanhSachCoSoKhamChuaBenhGanDuKhach(CancellationToken ct, [FromBody] Distance_Request _request)
        {
            var listResult = await Mediator.Send(new CoSoKhamChuaBenhGanViTriDuKhachGets.Query { infor = _request }, ct);
            var result = new DanhSach<HoSoLuTruItemResponse>();
            result.TotalRows = 0;
            if (listResult.Value.Count > 0)
            {
                result.Data = listResult.Value;
                result.TotalRows = result.Data[0].TotalRows;
            }
            //return HandlerResult(listResult);
            return HandlerResult(Result<DanhSach<HoSoLuTruItemResponse>>.Success(result));
        }
        [HttpGet("danhmuc/{pagesize?}/{pageindex?}")]
        [AllowAnonymous]
        public async Task<IActionResult> DanhMucLoaiHinhCoSoSucKhoe(CancellationToken ct, int pagesize = 10, int pageindex = 1)
        {
            var listResult = await Mediator.Send(new DanhMucLoaiHinhCoSoKhamChuaBenh.Query { pageindex = pageindex, pagesize = pagesize }, ct);
            //return HandlerResult(listResult);
            return HandlerResult(listResult);


        }
    }
}
