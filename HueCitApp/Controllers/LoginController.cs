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

namespace HueCitApp.Controllers
{
    public class LoginController : BaseApiController
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpClientFactory _httpClientFactory;
        public LoginController(IWebHostEnvironment hostingEnvironment, IHttpClientFactory httpClientFactory) : base(hostingEnvironment)
        {
            _webHostEnvironment = hostingEnvironment;
            _httpClientFactory = httpClientFactory;
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
    }
}
