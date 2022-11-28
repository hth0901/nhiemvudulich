using Application.DiaDiemDuLich;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using Application.SuKien;
using Microsoft.AspNetCore.Hosting;
using Application.Core;
using Application.MonAnThucUong;
using Domain.ResponseEntity;
using System.Drawing.Printing;
using Domain.RequestEntity;
using System.Dynamic;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data.SqlClient;
using System;

namespace HueCitApp.Controllers
{
    public class QuanTracMoiTruongController : BaseApiController
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IConfiguration _configuration;
        public QuanTracMoiTruongController(IWebHostEnvironment hostingEnvironment, IConfiguration configuration) : base (hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;
        }

        [HttpGet]
        [Route("danhsachtrongngay/{pageIndex}/{pageSize}")]
        [AllowAnonymous]
        public async Task<IActionResult> DanhSachTrongNgay(CancellationToken ct, int pageIndex, int pageSize)
        {
            var lstResult = await Mediator.Send(new Application.QuanTracMoiTruong.DanhSachTrongNgay.Query { pageIndex = pageIndex, pageSize = pageSize });

            var result = new DanhSach<QuanTracMoiTruongThongKe>();
            result.TotalRows = 0;
            if (lstResult.Value.Count > 0)
            {
                result.Data = lstResult.Value;
                result.TotalRows = result.Data[0].TotalRows;
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("thongkedanhsachtrongngay")]
        [AllowAnonymous]
        public async Task<IActionResult> ThongKeDanhSachTrongNgay(CancellationToken ct)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("HuecitConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync("SP_QUANTRACMOITRUONG_THONGKE_TRONGNGAY_ALL", param: null, commandType: System.Data.CommandType.StoredProcedure);
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("thongkedanhsachtrongthang")]
        [AllowAnonymous]
        public async Task<IActionResult> ThongKeDanhSachTrongThang(CancellationToken ct)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("HuecitConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync("SP_QUANTRACMOITRUONG_THONGKE_TRONGTHANG_ALL", param: null, commandType: System.Data.CommandType.StoredProcedure);
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("danhsachtrongthang/{pageIndex}/{pageSize}")]
        [AllowAnonymous]
        public async Task<IActionResult> DanhSachTrongThang(CancellationToken ct, int pageIndex, int pageSize)
        {
            var lstResult = await Mediator.Send(new Application.QuanTracMoiTruong.DanhSachTrongThang.Query { pageIndex = pageIndex, pageSize = pageSize });

            var result = new DanhSach<QuanTracMoiTruongThongKe>();
            result.TotalRows = 0;
            if (lstResult.Value.Count > 0)
            {
                result.Data = lstResult.Value;
                result.TotalRows = result.Data[0].TotalRows;
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("currentaqi")]
        [AllowAnonymous]
        public async Task<IActionResult> AqiHienTai(CancellationToken ct)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("HuecitConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryFirstOrDefaultAsync<AqiHienTaiResponse>("SP_QUANTRACMOITRUONG_AQI_HIENTAI", param: null, commandType: System.Data.CommandType.StoredProcedure);
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
