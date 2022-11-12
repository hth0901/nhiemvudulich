using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HueCitApp.ViewModels;

namespace HueCitApp.ViewComponents
{
    public class DropdownUserViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var user = (ClaimsIdentity)User.Identity;
            string uname = user.Name;
            DropdownUserViewModel vm = new DropdownUserViewModel { Username = uname };
            if (!string.IsNullOrEmpty(uname))
                return View(vm);
            else
                return View("Empty");
        }
    }
}
