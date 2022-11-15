using HueCitApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace HueCitApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public AdminController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [Authorize]
        public IActionResult QuanLyTaiKhoan()
        {
            var menu = HttpContext.Session.GetString("menuInfo");
            if (menu != null && !(string.IsNullOrEmpty(menu)))
            {
                if (menu.Contains('2'))
                {
                    return View();
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public IActionResult QuanLyNhomVaiTro()
        {
            var menu = HttpContext.Session.GetString("menuInfo");
            if (menu != null && !(string.IsNullOrEmpty(menu)))
            {
                if (menu.Contains('3'))
                {
                    return View();
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public IActionResult QuanLyCauHinh()
        {
            var menu = HttpContext.Session.GetString("menuInfo");
            if (menu != null && !(string.IsNullOrEmpty(menu)))
            {
                if (menu.Contains('5'))
                {
                    return View();
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public IActionResult QuanLyPhanQuyen()
        {
            var menu = HttpContext.Session.GetString("menuInfo");
            if (menu != null && !(string.IsNullOrEmpty(menu)))
            {
                if (menu.Contains('4'))
                {
                    return View();
                }
            }
            return RedirectToAction("Index", "Home");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
