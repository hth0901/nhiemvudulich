using Application.Core;
using Dapper;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Persistence;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DiemVeSinh
{
    public class DanhSachDiemVeSinh
    {
        public class Query : IRequest<Result<List<Domain.DiemVeSinh>>>
        {
        }
        public class Handler : IRequestHandler<Query, Result<List<Domain.DiemVeSinh>>>
        {
            private readonly IConfiguration _configuration;
            private readonly DataContext _context;

            public Handler(DataContext context, IConfiguration configuration)
            {
                _context = context;
                _configuration = configuration;

            }

            public async Task<Result<List<Domain.DiemVeSinh>>> Handle(Query request, CancellationToken cancellationToken)
            {
                string spName = "SP_DiemVeSinhGets";
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();
                    //var result = await connection.QueryAsync<Place>(spName);
                    var result = await connection.QueryAsync<Domain.DiemVeSinh>(new CommandDefinition(spName, parameters: null, commandType: System.Data.CommandType.StoredProcedure));
                    return Result<List<Domain.DiemVeSinh>>.Success(result.ToList());
                }


            }
        }
    }
}
