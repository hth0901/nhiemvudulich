using Application.Core;
using Application.HuongDanVienDuLich;
using Domain.ResponseEntity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using Application.TourDuLich;

namespace HueCitApp.Controllers
{
    public class TourDuLichController:BaseApiController
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public TourDuLichController(IWebHostEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
            _webHostEnvironment = hostingEnvironment;
        }
        [HttpGet("danhsach/{pagesize?}/{pageindex?}")]
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
