using Application.BanDo;
using Dapper;
using Domain;
using Domain.HueCit;
using HueCitApp.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HueCitApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDtoExt>> Login(CancellationToken ct, LoginDto request)
        {
            var user = await _userManager.FindByNameAsync(request.Username);
            if (user == null)
                return Unauthorized();
            var loginResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (loginResult.Succeeded)
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@PUSER", request.Username);

                string spName = "SP_UserGet";

                using (var connection = new SqlConnection(_configuration.GetConnectionString("HuecitConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryFirstOrDefaultAsync<SYS_UserTable>(new CommandDefinition(spName, parameters: dynamicParameters, commandType: System.Data.CommandType.StoredProcedure));
                    
                    UserDtoExt userinfo = new UserDtoExt
                    {
                        DisplayName = user.DisplayName,
                        Image = null,
                        Token = "",
                        Username = user.UserName,
                        Role = result.Quyen
                    };

                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Role", result.Quyen);

                    var menu = await connection.QueryAsync<int>(new CommandDefinition("SP_PhanQuyenGetMenu", parameters: parameters, commandType: System.Data.CommandType.StoredProcedure));

                    HttpContext.Session.SetString("userInfo", JsonConvert.SerializeObject(userinfo));
                    HttpContext.Session.SetString("menuInfo", JsonConvert.SerializeObject(menu));

                    return userinfo;
                }
            }

            return Unauthorized();
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await _userManager.Users.AnyAsync(x => x.Email == registerDto.Email))
            {
                return BadRequest("Email đã tồn tại");
            }

            if (await _userManager.Users.AnyAsync(x => x.UserName == registerDto.Username))
            {
                return BadRequest("Tên tài khoản đã tồn tại!");
            }

            var user = new AppUser
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.Username
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (result.Succeeded)
            {
                return new UserDto
                {
                    DisplayName = user.DisplayName,
                    Image = null,
                    Token = "",
                    Username = user.UserName
                };
            }

            return BadRequest("Không thể tạo tài khoản!!");
        }
    }
}
