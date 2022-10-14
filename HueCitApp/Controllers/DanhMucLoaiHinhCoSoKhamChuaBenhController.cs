using Application.Core;
using Application.CoSoLuuTru;
using Domain.ResponseEntity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using Application.LoaiHinhDanhMucCoSoKhamChuaBenh;

namespace HueCitApp.Controllers
{
    public class DanhMucLoaiHinhCoSoKhamChuaBenhController : BaseApiController
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public DanhMucLoaiHinhCoSoKhamChuaBenhController(IWebHostEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
            _webHostEnvironment = hostingEnvironment;
        }
        [HttpGet("danhmuc/{pagesize?}/{pageindex?}")]
        [AllowAnonymous]
        public async Task<IActionResult> DanhMucLoaiHinhCoSoKhamChuaBenh(CancellationToken ct, int pagesize = 10, int pageindex = 1)
        {
            var listResult = await Mediator.Send(new DanhMucLoaiHinhCoSoKhamChuaBenh.Query { pagesize = pagesize, pageindex = pageindex }, ct);
            var result = new DanhSachHoSoLuTruResponse();
            result.TotalRows = 0;
            if (listResult.Value.Count > 0)
            {
                result.Data = listResult.Value;
                result.TotalRows = result.Data[0].TotalRows;
            }
            //return HandlerResult(listResult);
            return HandlerResult(Result<DanhSachHoSoLuTruResponse>.Success(result));
        }
    }
}
