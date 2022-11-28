using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HueCitApp.ViewModels;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Dapper;
using Domain.HueCit;
using Newtonsoft.Json;

namespace HueCitApp.ViewComponents
{
    public class NavUserViewComponent : ViewComponent
    {
        private readonly IConfiguration _configuration;

        public NavUserViewComponent(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IViewComponentResult Invoke()
        {
            var user = (ClaimsIdentity)User.Identity;
            string uname = user.Name;
            //DropdownUserViewModel vm = new DropdownUserViewModel { Username = uname };
            //if (!string.IsNullOrEmpty(uname))
            //    return View(vm);
            //else
            //    return View("Empty");
            if (string.IsNullOrEmpty(uname))
                return View("Empty");


            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("HuecitConnection")))
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@PUSER", uname);
                    connection.Open();
                    var result = connection.QueryFirstOrDefault<SYS_UserTable>(new CommandDefinition("SP_UserGet", parameters: dynamicParameters, commandType: System.Data.CommandType.StoredProcedure));

                    if (result != null)
                    {
                        DynamicParameters parameters = new DynamicParameters();
                        parameters.Add("@Role", result.Quyen);

                        var menu = connection.Query<int>(new CommandDefinition("SP_PhanQuyenGetMenu", parameters: parameters, commandType: System.Data.CommandType.StoredProcedure));
                        string menuinfo = JsonConvert.SerializeObject(menu);

                        NavUserViewModel vm = new NavUserViewModel { menuinfo = menuinfo };

                        return View(vm);
                    }
                    else
                    {
                        return View("Empty");
                    }
                }
            }
            catch(Exception ex)
            {
                return View("Empty");
            }
        }
    }
}
