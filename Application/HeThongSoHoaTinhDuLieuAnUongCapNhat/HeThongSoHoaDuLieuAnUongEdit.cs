using Application.Core;
using AutoMapper;
using Dapper;
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

namespace Application.HeThongSoHoaTinhDuLieuAnUongCapNhat
{
    public class HeThongSoHoaDuLieuAnUongEdit
    {

        public class Command : IRequest<Result<int>>
        {
            public DL_MonAnThucUong infor { get; set; }

        }
        public class CommandValidator : AbstractValidator<DL_MonAnThucUong>
        {
            public CommandValidator()
            {

                RuleFor(x => x.ID).NotEmpty().NotNull();
                RuleFor(x => x.TenMon).NotEmpty().NotNull();
                RuleFor(x => x.MaLoai).NotEmpty().NotNull();
 

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

                string spName = "SP_EDIT_DuLieuAnUong";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@ID", request.infor.ID);
                parameters.Add("@TenMon", request.infor.TenMon);
                parameters.Add("@MaLoai", request.infor.MaLoai);
                parameters.Add("@MoTa", request.infor.MoTa);
                parameters.Add("@KieuMon", request.infor.KieuMon);
                parameters.Add("@ThucUong", request.infor.ThucUong);
                parameters.Add("@CachLam", request.infor.CachLam);
                parameters.Add("@ThanhPhan", request.infor.ThanhPhan);
                parameters.Add("@KhuyenNghiKhiDung", request.infor.KhuyenNghiKhiDung);
                parameters.Add("@AmThucID", request.infor.AmThucID);
                parameters.Add("@NguoiDongBo", request.infor.NguoiDongBo);


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
