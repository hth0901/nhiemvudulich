using Application.Core;
using Application.DiemVeSinh;
using Domain.ResponseEntity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using Application.DoanhNghiep;
using Domain.TechLife;
using Application.SuKien;
using Application.TourDuLich;

namespace HueCitApp.Controllers
{
    public class DoanhNghiepLuHanhController: BaseApiController
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public DoanhNghiepLuHanhController(IWebHostEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
            _webHostEnvironment = hostingEnvironment;
        }
        [HttpGet("danhsach/{pagesize?}/{pageindex?}")]
        [AllowAnonymous]

        public async Task<IActionResult> DanhSachDoanhNghiepLuHanh(CancellationToken ct, int pagesize = 10, int pageindex = 1)
        {
            var listResult = await Mediator.Send(new DoanhNghiepLuHanhGets.Query { pagesize = pagesize, pageindex = pageindex }, ct);
            var result = new DanhSach<HoSoLuTruItemResponse>();
            result.TotalRows = 0;
            if (listResult.Value.Count > 0)
            {
                result.Data = listResult.Value;
                result.TotalRows = result.Data[0].TotalRows;
            }
            //return HandlerResult(listResult);
            return HandlerResult(Result<DanhSach<HoSoLuTruItemResponse>>.Success(result));



        }
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> ChiTietDoanhNghiepLuHanh(CancellationToken ct, int id)
        {
            var listResult = await Mediator.Send(new DoanhNghiepLuHanhGet.Query { ID=id }, ct);
           return HandlerResult(listResult);
          



        }
        [HttpGet("danhsachtour/{pagesize?}/{pageindex?}")]
        [AllowAnonymous]

        public async Task<IActionResult> DanhSachTourDuLich(CancellationToken ct, int pagesize = 10, int pageindex = 1)
        {
            var listResult = await Mediator.Send(new TourDuLichGets.Query { pagesize = pagesize, pageindex = pageindex }, ct);
            var result = new DanhSach<TourItemResponse>();
            result.TotalRows = 0;
            if (listResult.Value.Count > 0)
            {
                result.Data = listResult.Value;
                result.TotalRows = result.Data[0].TotalRows;
            }
            //return HandlerResult(listResult);
            return HandlerResult(Result<DanhSach<TourItemResponse>>.Success(result));

        }

    }
}
