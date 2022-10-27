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


                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryFirstAsync<DL_BangTaiNguyen>(spName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                    return Result<DL_BangTaiNguyen>.Success(result);
                }
            }
        }
    }
}
