using Application.Core;
using AutoMapper;
using Dapper;
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

namespace Application.HeThongSoHoaTinhHuongDanVienCapNhat

{
    public class HeThongSoHoaHuongDanVienPost
    {


        public class Command : IRequest<Result<int>>
        {
            public Domain.HueCit.HuongDanVienDuLich infor { get; set; }

        }
        public class CommandValidator : AbstractValidator<Domain.HueCit.HuongDanVienDuLich>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Id).NotEmpty().WithMessage("họ tên không được rỗng");
                RuleFor(x => x.HoVaTen).NotEmpty().WithMessage("họ tên không được rỗng");
                RuleFor(x => x.GioiTinh).NotEmpty().WithMessage("giới tính, không được rỗng");
                RuleFor(x => x.CMND).NotEmpty().WithMessage("CMNN không được rỗng");
                RuleFor(x => x.NgayCapCMND).NotEmpty().WithMessage("Ngày cấp CMNN không được rỗng");
                RuleFor(x => x.NoiCapCMND).NotEmpty().WithMessage("Nơi cấp CMNN không được rỗng");
                RuleFor(x => x.SoDienThoai).NotEmpty().WithMessage("Số điện thoại không được rỗng");
                RuleFor(x => x.Email).NotEmpty().WithMessage("Email không được rỗng");
                RuleFor(x => x.DiaChi).NotEmpty().WithMessage("Địa chỉ không được rỗng");
                RuleFor(x => x.HoKhau).NotEmpty().WithMessage("Hộ khẩu không được rỗng");
                RuleFor(x => x.LoaiTheId).NotEmpty().WithMessage("identity Loại thẻ không được rỗng");
                RuleFor(x => x.NgayCapThe).NotEmpty().WithMessage("Ngày cấp thẻ không được rỗng");
                RuleFor(x => x.NgayHetHan).NotEmpty().WithMessage("Ngày hết hạn không được rỗng");
                RuleFor(x => x.SoTheHDV).NotEmpty().WithMessage("Số thẻ HDV không được rỗng");
                RuleFor(x => x.IsStatus).NotEmpty().WithMessage("Status không được rỗng");
                RuleFor(x => x.CongTyLuHanhId).NotEmpty().WithMessage("Identity công ty lữ hành không được rỗng");
                RuleFor(x => x.NoiCapThe).NotEmpty().WithMessage("Nơi cấp thẻ không được rỗng");
                RuleFor(x => x.NgaySinh).NotEmpty().WithMessage("Ngày Sinh không được rỗng");

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
                string spName = "SP_ADD_HuongDanVienDuLich";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@HoVaTen", request.infor.HoVaTen);
                parameters.Add("@GioiTinh", request.infor.GioiTinh);
                parameters.Add("@CMND", request.infor.CMND);
                parameters.Add("@NgayCapCMND", request.infor.NgayCapCMND);
                parameters.Add("@NoiCapCMND", request.infor.NoiCapCMND);
                parameters.Add("@SoDienThoai", request.infor.SoDienThoai);
                parameters.Add("@Email", request.infor.Email);
                parameters.Add("@DiaChi", request.infor.DiaChi);
                parameters.Add("@HoKhau", request.infor.HoKhau);
                parameters.Add("@LoaiTheId", request.infor.LoaiTheId);
                parameters.Add("@NgayCapThe", request.infor.NgayCapThe);
                parameters.Add("@NgayHetHan", request.infor.NgayHetHan);
                parameters.Add("@SoTheHDV", request.infor.SoTheHDV);
                parameters.Add("@IsStatus", request.infor.IsStatus);
                parameters.Add("@IsDelete", request.infor.IsDelete);
                parameters.Add("@CongTyLuHanhId", request.infor.CongTyLuHanhId);
                parameters.Add("@NoiCapThe", request.infor.NoiCapThe);
                parameters.Add("@NgaySinh", request.infor.NgaySinh);



                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();
                    var affectRow = await connection.ExecuteAsync(spName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                    var result = affectRow > 0;
                    if (!result)
                        return Result<int>.Failure("adding not success");
                    return Result<int>.Success(affectRow);
                }
            }
        }
    }



}

