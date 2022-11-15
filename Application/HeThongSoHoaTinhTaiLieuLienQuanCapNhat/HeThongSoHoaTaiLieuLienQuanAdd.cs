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

namespace Application.HeThongSoHoaTinhTaiLieuLienQuanCapNhat
{
    public class HeThongSoHoaTaiLieuLienQuanAdd
    {

        public class Command : IRequest<Result<int>>
        {
            public FileUploads infor { get; set; }

        }
        public class CommandValidator : AbstractValidator<FileUploads>
        {
            public CommandValidator()
            {


     
                RuleFor(x => x.FileName).NotEmpty().NotNull();
                RuleFor(x => x.FileUrl).NotEmpty().NotNull();
                RuleFor(x => x.Id).NotEmpty().NotNull();

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

                string spName = "SP_ADD_TaiLieuLienQuan";
                DynamicParameters parameters = new DynamicParameters();


                parameters.Add("@FileName", request.infor.FileName);
                parameters.Add("@FileUrl", request.infor.FileUrl);
                parameters.Add("@Type", request.infor.Type);
                parameters.Add("@Id", request.infor.Id);
                parameters.Add("@IsImage", request.infor.IsImage);
                parameters.Add("@IsStatus", request.infor.IsStatus);
                parameters.Add("@NgayTao", request.infor.NgayTao);
                parameters.Add("@FileType", request.infor.FileType);
                parameters.Add("@NguonDongBo", request.infor.NguonDongBo);
                parameters.Add("@MoTa", request.infor.MoTa);


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
