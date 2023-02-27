using Application.LoaiHinhCoSoLuuTru;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using Application.CoSoLuuTru;
using Domain.ResponseEntity;
using Application.Core;
using Application.DiaDiemDuLich;
using Domain.RequestEntity;
using Application.BanDo;
using System.Collections.Generic;
using Domain.TechLife;
using System.Net.Http;
using System.Text;
using System;
using Microsoft.AspNetCore.Identity;
using Domain;
using Dapper;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Domain.HueCit;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace HueCitApp.Controllers
{
    public class LoginController : Controller
    {
        //private readonly IWebHostEnvironment _webHostEnvironment;
        //private readonly IHttpClientFactory _httpClientFactory;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _configuration;

        public LoginController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IConfiguration configuration)
        {
            //_webHostEnvironment = hostingEnvironment;
            //_httpClientFactory = httpClientFactory;
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
        }
        //[HttpPost("signin")] 
        //[AllowAnonymous]
        //public async Task<IActionResult> Signin(CancellationToken ct, HoSoRequest request)
        //{
        //    var json = JsonConvert.SerializeObject(request);
        //    var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

        //    var client = _httpClientFactory.CreateClient();
        //    client.BaseAddress = new Uri("http://apicsdldl.huecit.com");
        //    var response = await client.PostAsync("/api/users/signin", httpContent);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        ApiSuccessResult<string> result = JsonConvert.DeserializeObject<ApiSuccessResult<string>>(await response.Content.ReadAsStringAsync());
        //        return HandlerResult(Result<ApiSuccessResult<string>>.Success(result));
        //    }

        //    ApiErrorResult<string> err = JsonConvert.DeserializeObject<ApiErrorResult<string>>(await response.Content.ReadAsStringAsync());
        //    return HandlerResult(Result<ApiErrorResult<string>>.Success(err));
        //}

        //loginSSO     
        public async Task<IActionResult> Authenticate(string user, string ownercode)
        {
            string uname = user;//= HashUtil.DecodeID(user.TenDangNhap.tos, "huecit.vn");
            if (!string.IsNullOrEmpty(uname))
            {
                //return RedirectToAction("/home/index?ReqId=dalogin");
                var curUser = await _userManager.FindByNameAsync(uname);
                if (curUser != null)
                {
                    // var signInResult = await signInManager.PasswordSignInAsync(curUser, "Abc@12345", false, false);
                    //kiểm tra hệ thống đã tồn tại tài khoản chưa
                    string pass = "iW{5W?2d" + "Pss@1";
                    var checkUser = await _signInManager.PasswordSignInAsync(uname, pass, isPersistent: false, lockoutOnFailure: false);

                    if (checkUser.Succeeded)
                    {
                        //Pass
                        var user1 = await _userManager.FindByNameAsync(uname);
                        if (user1 != null)
                        {
                            if (await _userManager.CheckPasswordAsync(user1, pass))
                            {
                                DynamicParameters dynamicParameters = new DynamicParameters();
                                dynamicParameters.Add("@PUSER", user1.UserName);

                                string spName = "SP_UserGet";

                                using (var connection = new SqlConnection(_configuration.GetConnectionString("HuecitConnection")))
                                {
                                    connection.Open();
                                    var result = await connection.QueryFirstOrDefaultAsync<SYS_UserTable>(new CommandDefinition(spName, parameters: dynamicParameters, commandType: System.Data.CommandType.StoredProcedure));
                                    HttpContext.Session.SetString("userInfo", JsonConvert.SerializeObject(user1));
                                    if (result != null && result.TrangThai == 1)
                                    {
                                        DynamicParameters parameters = new DynamicParameters();
                                        parameters.Add("@Role", result.Quyen);

                                        var menu = await connection.QueryAsync<int>(new CommandDefinition("SP_PhanQuyenGetMenu", parameters: parameters, commandType: System.Data.CommandType.StoredProcedure));
                                        HttpContext.Session.SetString("menuInfo", JsonConvert.SerializeObject(menu));
                                    }
                                }
                                return Redirect("/home/index?ReqId=abcde");
                            }
                        }
                    }
                }                
            }
            return Redirect("/home/index?ReqId=abcde");
        }
    }
    public class SesionLoginInfo
    {
        public string EmailDonVi { get; set; }
        public string TenDonVi { get; set; }
        public string MaDonVi { get; set; }
        public string ThuongTruXaPhuong { get; set; }
        public string DiaChiThuongTru { get; set; }
        public string MaSoCaNhan { get; set; }
        public int LoaiGiayToCaNhan { get; set; }
        public string DienThoaiDonVi { get; set; }
        public string DienThoaiCaNhan { get; set; }
        public string HoVaTen { get; set; }
        public bool LaCongVienChuc { get; set; }
        public string MaGuid { get; set; }
        public string MaChungThu { get; set; }
        public VaiTro VaiTro { get; set; }
        public string TenDangNhap { get; set; }
        public LoaiTaiKhoan LoaiTK { get; set; }
        public string EmailCaNhan { get; set; }
        public string DiaChiDonVi { get; set; }
    }
    public enum VaiTro
    {
        KhachHang = 0,
        CanBo = 1,
        QuanTri = 2,
        CongChuc = 3
    }
    public enum LoaiTaiKhoan
    {
        CXD = -1,
        CaNhan = 0,
        DoanhNghiep = 1,
        CoQuan = 2
    }
}
