using Application.Core;
using Dapper;
using Domain.HueCit;
using Domain.RequestEntity;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Application.HuongDanVienDuLich
{
    public class HuongDanVienDuLichCreate
    {
        public class Command : IRequest<Result<int>>
        {
            public Domain.HueCit.HuongDanVienDuLich infor { get; set; }
        }
        public class CommandValidator : AbstractValidator<Domain.HueCit.HuongDanVienDuLich>
        {
            public CommandValidator()
            {
                RuleFor(x => x.HoVaTen).NotEmpty().WithMessage("họ tên không được trống");
                RuleFor(x => x.GioiTinh).NotEmpty().WithMessage("giới tính không được trống");
                RuleFor(x => x.NgayCapCMND).NotEmpty().WithMessage("ngày cấp chứng minh nhân dân không được để trống");
                RuleFor(x => x.LoaiTheId).NotEmpty().WithMessage("loại thẻ không được để trống");
                RuleFor(x => x.NgayCapThe).NotEmpty().WithMessage("ngày cấp thẻ không được để trống");
                RuleFor(x => x.NgayHetHan).NotEmpty().WithMessage("ngày hết hạn thẻ không được để trống");
                RuleFor(x => x.IsStatus).NotEmpty().WithMessage("trạng thái không được để trống");
                RuleFor(x => x.IsDelete).NotEmpty().WithMessage("isdelete không được để trống");
                RuleFor(x => x.CongTyLuHanhId).NotEmpty().WithMessage("cong ty lu hanh không được để trống");
                RuleFor(x => x.NgaySinh).NotEmpty().WithMessage("ngày sinh không được để trống");


            }
        }
        public class Handler : IRequestHandler<Command, Result<int>>
            {
                private readonly IConfiguration _configuration;

                public Handler(IConfiguration configuration)
                {
                    _configuration = configuration;
                }

                public async Task<Result<int>> Handle(Command request, CancellationToken cancellationToken)
               {
                string spName = "SP_DSHuongDanVienDuLichCreate";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@HoVaTen", request.infor.HoVaTen);
                parameters.Add("@GioiTinh", request.infor.GioiTinh);
                parameters.Add("@CMND", request.infor.CMND);
                parameters.Add("@NgayCapCMND", request.infor.NgayCapCMND);
                parameters.Add("@NoiCapCMND", request.infor.NoiCapCMND);
                parameters.Add("@SoDienThoai", request.infor.SoDienThoai);
                parameters.Add("@Email", request.infor.Email);
                parameters.Add("@DiaCHi", request.infor.DiaChi);
                parameters.Add("@HoKhau", request.infor.HoKhau);
                parameters.Add("@SoTheHDV", request.infor.SoTheHDV);
                parameters.Add("@LoaiTheId", request.infor.LoaiTheId);
                parameters.Add("@NgayCapThe", request.infor.NgayCapThe);
                parameters.Add("@NgayHetHan", request.infor.NgayHetHan);
                parameters.Add("@IsStatus", request.infor.IsStatus);
                parameters.Add("@IsDelete", request.infor.IsDelete);
                parameters.Add("@CongTyLuHanhId", request.infor.CongTyLuHanhId);
                parameters.Add("@NoiCapThe", request.infor.NoiCapThe);
                parameters.Add("@NgaySinh", request.infor.NgaySinh);
                using (var connection = new SqlConnection(_configuration.GetConnectionString("HuecitConnection")))
                    {
                    connection.Open();
                    var affectRow = await connection.ExecuteAsync(spName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                    var result = affectRow > 0;
                    if (!result)
                        return Result<int>.Failure("Create Activity not success");
                    return Result<int>.Success(affectRow);
                }
                }
            }


    }
}
