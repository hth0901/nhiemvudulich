using Application.Core;
using AutoMapper;
using Dapper;
using Domain.RequestEntity;
using Domain.TechLife;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Persistence;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.HeThongSoHoaTinhCoSoLuuTruDuLichCapNhat
{
    public class HeThongSoHoaCoSoLuuTruAdd
    {
        public class Command : IRequest<Result<int>>
        {
            public HoSoRequestAdd infor { get; set; }

        }
        public class CommandValidator : AbstractValidator<HoSoRequestAdd>
        {
            public CommandValidator()
            {

          
                RuleFor(x => x.Ten).NotEmpty().NotNull();
             //   RuleFor(x => x.LinhVucKinhDoanhId).NotEmpty().NotNull();
                RuleFor(x => x.HangSao).NotEmpty().NotNull();
                RuleFor(x => x.LoaiHinhId).NotEmpty().NotNull();
                RuleFor(x => x.TongVonDauTuBanDau).NotEmpty().NotNull();
                RuleFor(x => x.TongVonDauTuBoSung).NotEmpty().NotNull();
                RuleFor(x => x.DienTichMatBang).NotEmpty().NotNull();
                RuleFor(x => x.DienTichMatBangXayDung).NotNull().NotEmpty();
                RuleFor(x => x.SoTang).NotEmpty().NotNull();
                RuleFor(x => x.TongSoPhong).NotEmpty().NotNull();
                RuleFor(x => x.TongSoGiuong).NotEmpty().NotNull();
                RuleFor(x => x.SoLanChuyenChu).NotEmpty().NotNull();
                RuleFor(x => x.DuongPho).NotEmpty().NotNull();
                RuleFor(x => x.PhuongXaId).NotEmpty().NotNull();
                RuleFor(x => x.QuanHuyenId).NotEmpty().NotNull();
                RuleFor(x => x.TinhThanhId).NotEmpty().NotNull();
                RuleFor(x => x.GioiTinhNguoiDaiDien).NotEmpty().NotNull();
                RuleFor(x => x.SoLuongLaoDong).NotEmpty().NotNull();
                RuleFor(x => x.DoTuoiTBNam).NotEmpty().NotNull();
                RuleFor(x => x.DoTuoiTBNu).NotEmpty().NotNull();
                RuleFor(x => x.KhamSucKhoeDinhKy).NotEmpty().NotNull();
                RuleFor(x => x.TrangPhucRieng).NotEmpty().NotNull();
                RuleFor(x => x.PhongChayNo).NotEmpty().NotNull();
                RuleFor(x => x.CNVSMoiTruong).NotEmpty().NotNull();
                RuleFor(x => x.IsStatus).NotEmpty().NotNull();
                RuleFor(x => x.IsDelete).NotEmpty().NotNull();
                RuleFor(x => x.SoLDNamNgoaiNuoc).NotEmpty().NotNull();
                RuleFor(x => x.SoLDNamTrongNuoc).NotEmpty().NotNull();
                RuleFor(x => x.SoLDNuNgoaiNuoc).NotEmpty().NotNull();
                RuleFor(x => x.SoLDNuTrongNuoc).NotEmpty().NotNull();
                RuleFor(x => x.SoLDThoiVu).NotEmpty().NotNull();
                RuleFor(x => x.SoLDThuongXuyen).NotEmpty().NotNull();
                RuleFor(x => x.SoLDTrucTiep).NotEmpty().NotNull();
                RuleFor(x => x.IsDatChuan).NotEmpty().NotNull();
                RuleFor(x => x.CSLTId).NotEmpty().NotNull();
                RuleFor(x => x.IsNhaHangTrongCSLT).NotEmpty().NotNull();
                RuleFor(x => x.NhaCungCapId).NotEmpty().NotNull();
                RuleFor(x => x.NgonNguId).NotEmpty().NotNull();









            }
        }


        public class Handler : IRequestHandler<Command, Result<int>>
        {
            private readonly DataContext _context;
            private readonly IConfiguration _configuration;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IConfiguration configuration, IMapper mapper)
            {
                _context = context;
                _configuration = configuration;
                _mapper = mapper;
            }
            public async Task<Result<int>> Handle(Command request, CancellationToken cancellationToken)
            {
                //_context.Activities.Add(request.Activity);
                //var ac = await _context.Activities.FindAsync(request.Activity.Id);
                //if (ac == null) return null;
                //_mapper.Map(request.Activity, ac);

                //ac.Title = request.Activity.Title ?? ac.Title;
                //var result = await _context.SaveChangesAsync() > 0;
                //if (!result)
                //    return Result<Unit>.Failure("Failed to update");

                //return Result<Unit>.Success(Unit.Value);
                string spName = "SP_DuLieuSoHoa_Add";
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@Ten", request.infor.Ten);
                parameters.Add("@LinhVucKinhDoanhId", 1);
                parameters.Add("@HangSao", request.infor.HangSao);
                parameters.Add("@SoQuyetDinh", request.infor.SoQuyetDinh);
                parameters.Add("@NgayQuyetDinh", request.infor.NgayQuyetDinh);
                parameters.Add("@LoaiHinhId", request.infor.LoaiHinhId);
                parameters.Add("@TongVonDauTuBanDau", request.infor.TongVonDauTuBanDau);
                parameters.Add("@TongVonDauTuBoSung", request.infor.TongVonDauTuBoSung);
                parameters.Add("@DienTichMatBang", request.infor.DienTichMatBang);
                parameters.Add("@DienTichMatBangXayDung", request.infor.DienTichMatBangXayDung);
                parameters.Add("@DienTichXayDung", request.infor.DienTichXayDung);
                parameters.Add("@SoTang", request.infor.SoTang);
                parameters.Add("@TongSoPhong", request.infor.TongSoPhong);
                parameters.Add("@TongSoGiuong", request.infor.TongSoGiuong);
                parameters.Add("@SoGiayPhep", request.infor.SoGiayPhep);
                parameters.Add("@SoLanChuyenChu", request.infor.SoLanChuyenChu);
                parameters.Add("@SoNha", request.infor.SoNha);
                parameters.Add("@DuongPho", request.infor.DuongPho);
                parameters.Add("@PhuongXaId", request.infor.PhuongXaId);
                parameters.Add("@QuanHuyenId", request.infor.QuanHuyenId);
                parameters.Add("@TinhThanhId", request.infor.TinhThanhId);
                parameters.Add("@SoDienThoai", request.infor.SoDienThoai);
                parameters.Add("@Fax", request.infor.Fax);
                parameters.Add("@Email", request.infor.Email);
                parameters.Add("@Website", request.infor.Website);
                parameters.Add("@HoTenNguoiDaiDien", request.infor.HoTenNguoiDaiDien);
                parameters.Add("@ChucVuNguoiDaiDien", request.infor.ChucVuNguoiDaiDien);
                parameters.Add("@GioiTinhNguoiDaiDien", request.infor.GioiTinhNguoiDaiDien);
                parameters.Add("@SoDienThoaiNguoiDaiDien", request.infor.SoDienThoaiNguoiDaiDien);
                parameters.Add("@SoLuongLaoDong", request.infor.SoLuongLaoDong);
                parameters.Add("@DoTuoiTBNam", request.infor.DoTuoiTBNam);
                parameters.Add("@DoTuoiTBNu", request.infor.DoTuoiTBNu);
                parameters.Add("@KhamSucKhoeDinhKy", request.infor.KhamSucKhoeDinhKy);
                parameters.Add("@TrangPhucRieng", request.infor.TrangPhucRieng);
                parameters.Add("@PhongChayNo", request.infor.PhongChayNo);
                parameters.Add("@CNVSMoiTruong", request.infor.CNVSMoiTruong);
                parameters.Add("@GhiChu", request.infor.GhiChu);
                parameters.Add("@IsStatus", request.infor.IsStatus);
                parameters.Add("@IsDelete", request.infor.IsDelete);
                parameters.Add("@ThoiDiemBatDauKinhDoanh", request.infor.ThoiDiemBatDauKinhDoanh);
                parameters.Add("@GioDongCua", request.infor.GioDongCua);
                parameters.Add("@GioMoCua", request.infor.GioMoCua);
                parameters.Add("@SoLDGianTiep", request.infor.SoLDGianTiep);
                parameters.Add("@SoLDNamNgoaiNuoc", request.infor.SoLDNamNgoaiNuoc);
                parameters.Add("@SoLDNamTrongNuoc", request.infor.SoLDNamTrongNuoc);
                parameters.Add("@SoLDNuNgoaiNuoc", request.infor.SoLDNuNgoaiNuoc);
                parameters.Add("@SoLDNuTrongNuoc", request.infor.SoLDNuTrongNuoc);
                parameters.Add("@SoLDThoiVu", request.infor.SoLDThoiVu);
                parameters.Add("@SoLDThuongXuyen", request.infor.SoLDThuongXuyen);
                parameters.Add("@SoLDTrucTiep", request.infor.SoLDTrucTiep);
                parameters.Add("@TenVietTat", request.infor.TenVietTat);
                parameters.Add("@ViTriTrenBanDo", request.infor.ViTriTrenBanDo);
                parameters.Add("@ToaDoX", request.infor.ToaDoX);
                parameters.Add("@ToaDoY", request.infor.ToaDoY);
                parameters.Add("@DonViCapPhep", request.infor.DonViCapPhep);
                parameters.Add("@MaSoCapPhep", request.infor.MaSoCapPhep);
                parameters.Add("@NgayCapPhep", request.infor.NgayCapPhep);
                parameters.Add("@IsDatChuan", request.infor.IsDatChuan);
                parameters.Add("@NgayCVDatChuan", request.infor.NgayCVDatChuan);
                parameters.Add("@SoCVDatChuan", request.infor.SoCVDatChuan);
                parameters.Add("@NgayHetHan", request.infor.NgayHetHan);
                parameters.Add("@CSLTId", request.infor.CSLTId);
                parameters.Add("@IsNhaHangTrongCSLT", request.infor.IsNhaHangTrongCSLT);
                parameters.Add("@NhaCungCapId", request.infor.NhaCungCapId);
                parameters.Add("@GioiThieu", request.infor.GioiThieu);
                parameters.Add("@NgonNguId", request.infor.NgonNguId);
                parameters.Add("@GiaThamKhaoTu", request.infor.GiaThamKhaoTu);
                parameters.Add("@GiaThamKhaoDen", request.infor.GiaThamKhaoDen);
                parameters.Add("@LoaiDiaDiemAnUong", request.infor.LoaiDiaDiemAnUong);
                parameters.Add("@PhucVu", request.infor.PhucVu);
                parameters.Add("@MaDoanhNghiep", request.infor.MaDoanhNghiep);
                parameters.Add("@NguonDongBo", request.infor.NguonDongBo);
                parameters.Add("@DongBoID", 0);

                using (var connection = new SqlConnection(_configuration.GetConnectionString("Huecitconnection")))
                {
                    connection.Open();
                    var result = await connection.QueryFirstAsync(spName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                    return Result<HoSo>.Success(result);
                }
            }
        }
    }
}
