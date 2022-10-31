using Application.Core;
using AutoMapper;
using Dapper;
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
    public class HeThongSoHoaHuongDanVienEdit
    {
        public class Command : IRequest<Result<int>>
        {
            public Domain.HueCit.HuongDanVienDuLich infor { get; set; }

        }
        public class CommandValidator : AbstractValidator<Domain.HueCit.HuongDanVienDuLich>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Id).NotEmpty().NotNull();

                RuleFor(x => x.HoVaTen).NotEmpty().NotNull();
                RuleFor(x => x.GioiTinh).NotEmpty().NotNull();
                RuleFor(x => x.CMND).NotEmpty().NotNull();
                RuleFor(x => x.NgayCapCMND).NotEmpty().NotNull();
                RuleFor(x => x.NoiCapCMND).NotEmpty().NotNull();
                RuleFor(x => x.LoaiTheId).NotEmpty().NotNull();
                RuleFor(x => x.NgayCapThe).NotEmpty().NotNull();
                RuleFor(x => x.NgayHetHan).NotEmpty().NotNull();
                RuleFor(x => x.IsDelete).NotEmpty().NotNull();
                RuleFor(x => x.IsStatus).NotEmpty().NotNull();
                RuleFor(x => x.CongTyLuHanhId).NotEmpty().NotNull();
                RuleFor(x => x.NgaySinh).NotEmpty().NotNull();

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
                string spName = "SP_EDIT_HuongDanVienDuLich";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@ID", request.infor.Id);
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
                        return Result<int>.Failure("editing not success");
                    return Result<int>.Success(affectRow);
                }
            }
        }

    }
}
