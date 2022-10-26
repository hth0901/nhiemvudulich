using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace HueCitApp.Controllers
{
    public class ThongKeController : BaseApiController
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IConfiguration _configuration;

        public ThongKeController(IWebHostEnvironment hostingEnvironment, IConfiguration configuration) : base(hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("phananhientruong/{year?}")]
        public async Task<IActionResult> PhanAnhHienTruong(string year = "-1")
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@PYEAR", string.IsNullOrEmpty(year) || year == "-1" ? null : year);
                using (var connection = new SqlConnection(_configuration.GetConnectionString("HuecitConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync("SP_PHANANHHIENTRUONG_THONGKE_THEOLINHVUC", param: parameters, commandType: System.Data.CommandType.StoredProcedure);
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("phananhientruongtong/{year?}")]
        public async Task<IActionResult> PhanAnhHienTruongTong(string year = "-1")
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@PYEAR", string.IsNullOrEmpty(year) || year == "-1" ? null : year);
                using (var connection = new SqlConnection(_configuration.GetConnectionString("HuecitConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryFirstOrDefaultAsync("SP_PHANANHHIENTRUONG_THONGKE_TONGHOP", param: parameters, commandType: System.Data.CommandType.StoredProcedure);
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("linhvuckinhdoanh")]
        public async Task<IActionResult> LinhVucKinhDoanh()
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("TechLifeConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryFirstOrDefaultAsync("SP_LINHVUCKINHDOANH_THONGKE", param: null, commandType: System.Data.CommandType.StoredProcedure);
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
