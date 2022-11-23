using Application.Core;
using Dapper;
using Domain;
using Domain.HueCit;
using Domain.RequestEntity;
using Domain.TechLife;
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
    public class ThemMoiTaiNguyen
    {
        public class Command : IRequest<Result<HoSo>>
        {
            public HoSoRequestAdd infor { get; set; }
        }
        public class CommandValidator : AbstractValidator<HoSo>
        {
            public CommandValidator()
            {
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
        public class Handler : IRequestHandler<Command, Result<HoSo>>
        {
            private readonly DataContext _context;
            private readonly IConfiguration _configuration;

            public Handler(DataContext context, IConfiguration configuration)
            {
                _context = context;
                _configuration = configuration;
            }
            public async Task<Result<HoSo>> Handle(Command request, CancellationToken cancellationToken)
            {
                string spName = "SP_DuLieuSoHoa_Add";
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@Ten", request.infor.Ten);
                parameters.Add("@LinhVucKinhDoanhId", 5);
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

                using (var connection = new SqlConnection(_configuration.GetConnectionString("HuecitConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryFirstAsync<HoSo>(spName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                    return Result<HoSo>.Success(result);
                }
            }
        }
    }
}
