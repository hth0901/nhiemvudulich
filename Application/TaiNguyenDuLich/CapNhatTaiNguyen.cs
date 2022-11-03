using Application.Core;
using Dapper;
using Domain;
using Domain.HueCit;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Persistence;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TaiNguyenDuLich
{
    public class CapNhatTaiNguyen
    {
        public class Command : IRequest<Result<DL_BangTaiNguyen>>
        {
            public DL_BangTaiNguyen _tainguyen { get; set; }
        }
        public class CommandValidator : AbstractValidator<DL_BangTaiNguyen>
        {
            public CommandValidator()
            {
                RuleFor(x => x.ID).NotEmpty().WithMessage("ID không được rỗng");
                RuleFor(x => x.Ten).NotEmpty().WithMessage("Tên không được rỗng");
                RuleFor(x => x.LinhVucKinhDoanhId).NotEmpty().WithMessage("Mã kinh lĩnh vực doanh không được rỗng");
                RuleFor(x => x.HangSao).NotEmpty().WithMessage("Hạng sao không được rỗng");
                RuleFor(x => x.LoaiHinhId).NotEmpty().WithMessage("Mã loại hình không được rỗng");
                RuleFor(x => x.TongVonDauTuBanDau).NotEmpty().WithMessage("Vốn đầu tư ban đầu không được rỗng");
                RuleFor(x => x.TongVonDauTuBoSung).NotEmpty().WithMessage("Vốn đầu tư bổ sung không được rỗng");
                RuleFor(x => x.DienTichMatBang).NotEmpty().WithMessage("Diện tích mặt bằng không được rỗng");
                RuleFor(x => x.DienTichMatBangXayDung).NotEmpty().WithMessage("Diện tích mặt bằng xây dựng không được rỗng");
                RuleFor(x => x.DienTichXayDung).NotEmpty().WithMessage("Diện tích xây dựng không được rỗng");
                RuleFor(x => x.SoTang).NotEmpty().WithMessage("Số tầng không được rỗng");
                RuleFor(x => x.TongSoPhong).NotEmpty().WithMessage("Tổng số Phòng không được rỗng");
                RuleFor(x => x.TongSoGiuong).NotEmpty().WithMessage("Tổng số giường không được rỗng");
                RuleFor(x => x.SoLanChuyenChu).NotEmpty().WithMessage("Số lần chuyển chủ không được rỗng");
                RuleFor(x => x.PhuongXaId).NotEmpty().WithMessage("Mã phường xã không được rỗng");
                RuleFor(x => x.QuanHuyenId).NotEmpty().WithMessage("Mã quận không được rỗng");
                RuleFor(x => x.TinhThanhId).NotEmpty().WithMessage("Mã tỉnh không được rỗng");
                RuleFor(x => x.GioiTinhNguoiDaiDien).NotEmpty().WithMessage("Giới tính người đại diện không được rỗng");
                RuleFor(x => x.SoLuongLaoDong).NotEmpty().WithMessage("Số lượng lao động không được rỗng");
                RuleFor(x => x.DoTuoiTBNam).NotEmpty().WithMessage("Tuổi TB nam không được rỗng");
                RuleFor(x => x.DoTuoiTBNu).NotEmpty().WithMessage("Tuổi TB nữ không được rỗng");
                RuleFor(x => x.KhamSucKhoeDinhKy).NotEmpty().WithMessage("Khám sức khỏe định kỳ không được rỗng");
                RuleFor(x => x.TrangPhucRieng).NotEmpty().WithMessage("Trang phục riêng không được rỗng");
                RuleFor(x => x.PhongChayNo).NotEmpty().WithMessage("Phòng cháy nổ không được rỗng");
                RuleFor(x => x.CNVSMoiTruong).NotEmpty().WithMessage("CNVS môi trường không được rỗng");
                RuleFor(x => x.IsStatus).NotEmpty().WithMessage("Trạng thái không được rỗng");
                RuleFor(x => x.IsDelete).NotEmpty().WithMessage("Đã xóa không được rỗng");
                RuleFor(x => x.SoLDGianTiep).NotEmpty().WithMessage("Lao động gián tiếp không được rỗng");
                RuleFor(x => x.SoLDNamNgoaiNuoc).NotEmpty().WithMessage("Lao động nam ngoài không được rỗng");
                RuleFor(x => x.SoLDNamTrongNuoc).NotEmpty().WithMessage("Lao động nam trong nước không được rỗng");
                RuleFor(x => x.SoLDNuNgoaiNuoc).NotEmpty().WithMessage("Lao động nữ ngoài nước không được rỗng");
                RuleFor(x => x.SoLDNuTrongNuoc).NotEmpty().WithMessage("Lao động nữ trong nước không được rỗng");
                RuleFor(x => x.SoLDThoiVu).NotEmpty().WithMessage("Lao động thời vụ không được rỗng");
                RuleFor(x => x.SoLDThuongXuyen).NotEmpty().WithMessage("Lao động thường xuyên được rỗng");
                RuleFor(x => x.SoLDTrucTiep).NotEmpty().WithMessage("Lao động trực tiếp không được rỗng");
                RuleFor(x => x.CSLTId).NotEmpty().WithMessage("CSLT không được rỗng");
                RuleFor(x => x.IsNhaHangTrongCSLT).NotEmpty().WithMessage("Nhà hàng trong CSTL không được rỗng");
                RuleFor(x => x.NhaCungCapId).NotEmpty().WithMessage("Nhà cung cấp không được rỗng");
                RuleFor(x => x.NgonNguId).NotEmpty().WithMessage("Ngôn ngữ không được rỗng");


            }
        }
        public class Handler : IRequestHandler<Command, Result<DL_BangTaiNguyen>>
        {
            private readonly DataContext _context;
            private readonly IConfiguration _configuration;

            public Handler(DataContext context, IConfiguration configuration)
            {
                _context = context;
                _configuration = configuration;
            }
            public async Task<Result<DL_BangTaiNguyen>> Handle(Command request, CancellationToken cancellationToken)
            {
                string spName = "SP_edit_TaiNguyen";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@ID", request._tainguyen.ID);
                parameters.Add("@Ten", request._tainguyen.Ten);
                parameters.Add("@LinhVucKinhDoanhId", request._tainguyen.LinhVucKinhDoanhId);
                parameters.Add("@HangSao", request._tainguyen.HangSao);
                parameters.Add("@SoQuyetDinh", request._tainguyen.SoQuyetDinh);
                parameters.Add("@NgayQuyetDinh", request._tainguyen.NgayQuyetDinh);
                parameters.Add("@LoaiHinhId", request._tainguyen.LoaiHinhId);
                parameters.Add("@TongVonDauTuBanDauaa", request._tainguyen.TongVonDauTuBanDau);
                parameters.Add("@TongVonDauTuBoSung", request._tainguyen.TongVonDauTuBoSung);
                parameters.Add("@DienTichMatBang", request._tainguyen.DienTichMatBang);
                parameters.Add("@DienTichMatBangXayDung", request._tainguyen.DienTichMatBangXayDung);
                parameters.Add("@DienTichXayDung", request._tainguyen.DienTichXayDung);
                parameters.Add("@SoTang", request._tainguyen.SoTang);
                parameters.Add("@TongSoPhong", request._tainguyen.TongSoPhong);
                parameters.Add("@TongSoGiuong", request._tainguyen.TongSoGiuong);
                parameters.Add("@SoGiayPhep", request._tainguyen.SoGiayPhep);
                parameters.Add("@SoLanChuyenChu", request._tainguyen.SoLanChuyenChu);
                parameters.Add("@SoNha", request._tainguyen.SoNha);
                parameters.Add("@DuongPho", request._tainguyen.DuongPho);
                parameters.Add("@PhuongXaId", request._tainguyen.PhuongXaId);
                parameters.Add("@QuanHuyenId", request._tainguyen.QuanHuyenId);
                parameters.Add("@TinhThanhId", request._tainguyen.TinhThanhId);
                parameters.Add("@SoDienThoai", request._tainguyen.SoDienThoai);
                parameters.Add("@Fax", request._tainguyen.Fax);
                parameters.Add("@Email", request._tainguyen.Email);
                parameters.Add("@Website", request._tainguyen.Website);
                parameters.Add("@HoTenNguoiDaiDien", request._tainguyen.HoTenNguoiDaiDien);
                parameters.Add("@ChucVuNguoiDaiDien", request._tainguyen.ChucVuNguoiDaiDien);
                parameters.Add("@GioiTinhNguoiDaiDien", request._tainguyen.GioiTinhNguoiDaiDien);
                parameters.Add("@SoDienThoaiNguoiDaiDien", request._tainguyen.SoDienThoaiNguoiDaiDien);
                parameters.Add("@SoLuongLaoDong", request._tainguyen.SoLuongLaoDong);
                parameters.Add("@DoTuoiTBNam", request._tainguyen.DoTuoiTBNam);
                parameters.Add("@DoTuoiTBNu", request._tainguyen.DoTuoiTBNu);
                parameters.Add("@KhamSucKhoeDinhKy", request._tainguyen.KhamSucKhoeDinhKy);
                parameters.Add("@TrangPhucRieng", request._tainguyen.TrangPhucRieng);
                parameters.Add("@PhongChayNo", request._tainguyen.PhongChayNo);
                parameters.Add("@CNVSMoiTruong", request._tainguyen.CNVSMoiTruong);
                parameters.Add("@GhiChu", request._tainguyen.GhiChu);
                parameters.Add("@IsStatus", request._tainguyen.IsStatus);
                parameters.Add("@IsDelete", request._tainguyen.IsDelete);
                parameters.Add("@ThoiDiemBatDauKinhDoanh", request._tainguyen.ThoiDiemBatDauKinhDoanh);
                parameters.Add("@GioDongCua", request._tainguyen.GioDongCua);
                parameters.Add("@GioMoCua", request._tainguyen.GioMoCua);
                parameters.Add("@SoLDGianTiep", request._tainguyen.SoLDGianTiep);
                parameters.Add("@SoLDNamNgoaiNuoc", request._tainguyen.SoLDNamNgoaiNuoc);
                parameters.Add("@SoLDNamTrongNuoc", request._tainguyen.SoLDNamTrongNuoc);
                parameters.Add("@SoLDNuNgoaiNuoc", request._tainguyen.SoLDNuNgoaiNuoc);
                parameters.Add("@SoLDNuTrongNuoc", request._tainguyen.SoLDNuTrongNuoc);
                parameters.Add("@SoLDThoiVu", request._tainguyen.SoLDThoiVu);
                parameters.Add("@SoLDThuongXuyen", request._tainguyen.SoLDThuongXuyen);
                parameters.Add("@SoLDTrucTiep", request._tainguyen.SoLDTrucTiep);
                parameters.Add("@TenVietTat", request._tainguyen.TenVietTat);
                parameters.Add("@ViTriTrenBanDo", request._tainguyen.ViTriTrenBanDo);
                parameters.Add("@ToaDoX", request._tainguyen.ToaDoX);
                parameters.Add("@ToaDoY", request._tainguyen.ToaDoY);
                parameters.Add("@DonViCapPhep", request._tainguyen.DonViCapPhep);
                parameters.Add("@MaSoCapPhep", request._tainguyen.MaSoCapPhep);
                parameters.Add("@NgayCapPhep", request._tainguyen.NgayCapPhep);
                parameters.Add("@IsDatChuan", request._tainguyen.IsDatChuan);
                parameters.Add("@NgayCVDatChuan", request._tainguyen.NgayCVDatChuan);
                parameters.Add("@SoCVDatChuan", request._tainguyen.SoCVDatChuan);
                parameters.Add("@NgayHetHan", request._tainguyen.NgayHetHan);
                parameters.Add("@CSLTId", request._tainguyen.CSLTId);
                parameters.Add("@IsNhaHangTrongCSLT", request._tainguyen.IsNhaHangTrongCSLT);
                parameters.Add("@NhaCungCapId", request._tainguyen.NhaCungCapId);
                parameters.Add("@GioiThieu", request._tainguyen.GioiThieu);
                parameters.Add("@NgonNguId", request._tainguyen.NgonNguId);
                parameters.Add("@GiaThamKhaoTu", request._tainguyen.GiaThamKhaoTu);
                parameters.Add("@GiaThamKhaoDen", request._tainguyen.GiaThamKhaoDen);
                parameters.Add("@LoaiDiaDiemAnUong", request._tainguyen.LoaiDiaDiemAnUong);
                parameters.Add("@PhucVu", request._tainguyen.PhucVu);
                parameters.Add("@MaDoanhNghiep", request._tainguyen.MaDoanhNghiep);
                parameters.Add("@NguonDongBo", request._tainguyen.NguonDongBo);
                parameters.Add("@DongBoID", 1);




                using (var connection = new SqlConnection(_configuration.GetConnectionString("TechLifeConnection")))
                {
                    connection.Open();
                    var result = await connection.ExecuteScalarAsync<DL_BangTaiNguyen>(spName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                    return Result<DL_BangTaiNguyen>.Success(result);
                }
            }
        }
    }
}
