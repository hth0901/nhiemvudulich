using Application.BanDo;
using Dapper;
using Domain;
using Domain.HueCit;
using HueCitApp.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace HueCitApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager, IConfiguration configuration)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Bando()
        {
            var menu = HttpContext.Session.GetString("menuInfo");
            if (menu != null && !(string.IsNullOrEmpty(menu)))
            {
                if (menu.Contains('1'))
                {
                    return View("GisMap");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public IActionResult Privacy()
        {
            //var user = (ClaimsIdentity)User.Identity;
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(CancellationToken ct, string username, string password)
        {
            //login functionality
            var user = await _userManager.FindByNameAsync(username);

            if (user != null)
            {
                //sign in
                var signInResult = await _signInManager.PasswordSignInAsync(user, password, false, false);

                if (signInResult.Succeeded)
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@PUSER", user.UserName);

                    string spName = "SP_UserGet";

                    using (var connection = new SqlConnection(_configuration.GetConnectionString("HuecitConnection")))
                    {
                        connection.Open();
                        var result = await connection.QueryFirstOrDefaultAsync<SYS_UserTable>(new CommandDefinition(spName, parameters: dynamicParameters, commandType: System.Data.CommandType.StoredProcedure));
                        HttpContext.Session.SetString("userInfo", JsonConvert.SerializeObject(user));
                        if (result != null && result.TrangThai == 1)
                        {
                            DynamicParameters parameters = new DynamicParameters();
                            parameters.Add("@Role", result.Quyen);

                            var menu = await connection.QueryAsync<int>(new CommandDefinition("SP_PhanQuyenGetMenu", parameters: parameters, commandType: System.Data.CommandType.StoredProcedure));
                            HttpContext.Session.SetString("menuInfo", JsonConvert.SerializeObject(menu));
                        }
                        else
                        {
                            ViewBag.error = "Tên tài khoản hoặc mật khẩu không chính xác, vui lòng thử lại.";
                            return View();
                        }
                    }

                    return RedirectToAction("Index");
                }
            }
            ViewBag.error = "Tên tài khoản hoặc mật khẩu không chính xác, vui lòng thử lại.";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginSSO(CancellationToken ct, string username, string password)
        {
            //kiểm tra xác thực trên hệ thống tài khoản đăng nhập
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://user.thuathienhue.gov.vn/api/AuthenToken/login");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                var input = new
                {
                    username = username,
                    password = password,
                    appCode = "HSCV"
                };
                string json = System.Text.Json.JsonSerializer.Serialize(input);
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var resultObj = streamReader.ReadToEnd();
                JObject json2 = JObject.Parse(resultObj);
                if ((string)json2.SelectToken("Success") == "True")
                {
                    string pass = "iW{5W?2d" + "Pss@1";
                    var checkUser = await _signInManager.PasswordSignInAsync(username, pass, isPersistent: false, lockoutOnFailure: false);

                    if (checkUser.Succeeded)
                    {
                        //Pass
                        var user1 = await _userManager.FindByNameAsync(username);
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
                                    else
                                    {
                                        ViewBag.error = "Tên tài khoản hoặc mật khẩu không chính xác, vui lòng thử lại.";
                                        return View("Views/Home/Login.cshtml");
                                    }
                                }

                                return RedirectToAction("Index");
                            }
                        }
                    }
                    else
                    {
                        //Khoi tao
                        SYS_User us = new SYS_User
                        { 
                            HoTen = (string)json2.SelectToken("FullName"),
                            DiaChi = (string)json2.SelectToken("Address"),
                            DienThoai = (string)json2.SelectToken("CellPhone"),
                            HopThu = (string)json2.SelectToken("Email"),
                            TenDangNhap = username,
                            Quyen = 2,
                            TrangThai = 1
                        };

                        var user = new AppUser
                        {
                            DisplayName = (string)json2.SelectToken("FullName"),
                            Email = (string)json2.SelectToken("Email"),
                            UserName = username
                        };

                        var result = await _userManager.CreateAsync(user, pass);
                        if (result.Succeeded)
                        {
                            DynamicParameters dynamicParameters = new DynamicParameters();
                            dynamicParameters.Add("@PHOTEN", us.HoTen);
                            dynamicParameters.Add("@PDIACHI", us.DiaChi);
                            dynamicParameters.Add("@PDIENTHOAI", us.DienThoai);
                            dynamicParameters.Add("@PHOPTHU", us.HopThu);
                            dynamicParameters.Add("@PTENDANGNHAP", us.TenDangNhap);
                            dynamicParameters.Add("@PMATKHAU", "");
                            dynamicParameters.Add("@PQUYEN", us.Quyen);
                            dynamicParameters.Add("@PTRANGTHAI", us.TrangThai.ToString());

                            string spName = "SP_UserAdd";

                            using (var connection = new SqlConnection(_configuration.GetConnectionString("HuecitConnection")))
                            {
                                connection.Open();
                                var res = await connection.QueryFirstOrDefaultAsync<SYS_User>(new CommandDefinition(spName, parameters: dynamicParameters, commandType: System.Data.CommandType.StoredProcedure));

                                if (res != null)
                                {
                                    var user2 = await _userManager.FindByNameAsync(username);
                                    if (user2 != null)
                                    {
                                        //sign in
                                        var signInResult = await _signInManager.PasswordSignInAsync(user2, pass, false, false);

                                        if (signInResult.Succeeded)
                                        {
                                            DynamicParameters dynamicParameters1 = new DynamicParameters();
                                            dynamicParameters1.Add("@PUSER", user.UserName);

                                            var result1 = await connection.QueryFirstOrDefaultAsync<SYS_UserTable>(new CommandDefinition("SP_UserGet", parameters: dynamicParameters1, commandType: System.Data.CommandType.StoredProcedure));
                                            HttpContext.Session.SetString("userInfo", JsonConvert.SerializeObject(user));
                                            if (result1 != null && result1.TrangThai == 1)
                                            {
                                                DynamicParameters parameters1 = new DynamicParameters();
                                                parameters1.Add("@Role", result1.Quyen);

                                                var menu1 = await connection.QueryAsync<int>(new CommandDefinition("SP_PhanQuyenGetMenu", parameters: parameters1, commandType: System.Data.CommandType.StoredProcedure));
                                                HttpContext.Session.SetString("menuInfo", JsonConvert.SerializeObject(menu1));
                                            }
                                            else
                                            {
                                                ViewBag.error = "Tên tài khoản hoặc mật khẩu không chính xác, vui lòng thử lại.";
                                                return View();
                                            }

                                            return RedirectToAction("Index");
                                        }
                                    }

                                    return RedirectToAction("Index");
                                }
                                else
                                {
                                    ViewBag.error = "Đã xảy ra lỗi, vui lòng thử lại.";
                                    return View("Views/Home/Login.cshtml");
                                }
                            }
                        }
                        else
                        {
                            return BadRequest("Mật khẩu không thỏa mãn yêu cầu!!");
                        }
                    }

                    return RedirectToAction("Index");
                }

                ViewBag.error = "Tên tài khoản hoặc mật khẩu không chính xác, vui lòng thử lại.";
                return View("Views/Home/Login.cshtml");
            }
            return View("Views/Home/Login.cshtml");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> LogOut()
        {
            HttpContext.Session.Clear();

            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
