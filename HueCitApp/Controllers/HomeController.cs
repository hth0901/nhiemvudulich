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
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
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

        public IActionResult Bando()
        {
            return View("GisMap");
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

                        if (result != null)
                        {
                            DynamicParameters parameters = new DynamicParameters();
                            parameters.Add("@Role", result.Quyen);

                            var menu = await connection.QueryAsync<int>(new CommandDefinition("SP_PhanQuyenGetMenu", parameters: parameters, commandType: System.Data.CommandType.StoredProcedure));
                            HttpContext.Session.SetString("menuInfo", JsonConvert.SerializeObject(menu));
                        }

                        HttpContext.Session.SetString("userInfo", JsonConvert.SerializeObject(user));
                    }

                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
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
            return RedirectToAction("Index");
        }
    }
}
