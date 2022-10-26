using Application.Core;
using Application.DiaDiemDuLich;
using Domain.ResponseEntity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using Domain.RequestEntity;

namespace HueCitApp.Controllers
{
    public class KhuDuLichDiaDiemDuLichController : BaseApiController
    {

        private readonly IWebHostEnvironment _webHostEnvironment;
        public KhuDuLichDiaDiemDuLichController(IWebHostEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
            _webHostEnvironment = hostingEnvironment;
        }
        [HttpGet("loaihinhtainguyen")]
        [AllowAnonymous]

        public async Task<IActionResult> DanhSachLoaiHinhTaiNguyen(CancellationToken ct)
        {
            var listResult = await Mediator.Send(new LoaiHinhTaiNguyenDuLichGets.Query(), ct);

            //return HandlerResult(listResult);
            return HandlerResult(listResult);
        }
        [HttpGet("loaihinhdisanvanhoa")]
        [AllowAnonymous]

        public async Task<IActionResult> LoaiHinhDiSanVanHoa(CancellationToken ct)
        {
            var listResult = await Mediator.Send(new LoaiHinhDiSanVanHoaGets.Query(), ct);

            //return HandlerResult(listResult);
            return HandlerResult(listResult);
        }
        [HttpGet("danhsach/{pagesize?}/{pageindex?}")]
        [AllowAnonymous]

        public async Task<IActionResult> GetPlaces(CancellationToken ct, int pagesize = 10, int pageindex = 1)
        {
            var listResult = await Mediator.Send(new DiaDiemDuLichGets.Query { pagesize = pagesize, pageindex = pageindex }, ct);
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


        public async Task<IActionResult> ChiTietDiemDuLich(CancellationToken ct, int ID)
        {
            var listResult = await Mediator.Send(new DiemDuLichGet.Query { ID = ID }, ct);
            return HandlerResult(listResult);

        }
        [HttpGet("ganvitridukhach")]
        [AllowAnonymous]

        public async Task<IActionResult> DanhSachDiemDuLichGanDuKhach(CancellationToken ct, [FromBody] Distance_Request _request)
        {
            var listResult = await Mediator.Send(new DiaDiemDuLichGanViTriDuKhachGets.Query { infor = _request }, ct);
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





    }
}
