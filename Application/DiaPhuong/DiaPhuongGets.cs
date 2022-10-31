using Application.Core;
using Dapper;
using Domain;
using Domain.ResponseEntity;
using Domain.TechLife;
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

namespace Application.DiaPhuong
{
    public class DiaPhuongGets
    {
        public class Query : IRequest<Result<List<Domain.TechLife.DiaPhuong>>>
        {

        }
        public class Handler : IRequestHandler<Query, Result<List<Domain.TechLife.DiaPhuong>>>
        {
            private readonly IConfiguration _configuration;
            private readonly DataContext _context;

            public Handler(DataContext context, IConfiguration configuration)
            {
                _context = context;
                _configuration = configuration;

            }

            public async Task<Result<List<Domain.TechLife.DiaPhuong>>> Handle(Query request, CancellationToken cancellationToken)
            {
                string spName = "SP_DiaPhuongGets";
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DbMainConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<Domain.TechLife.DiaPhuong>(new CommandDefinition(spName, commandType: System.Data.CommandType.StoredProcedure));
                    return Result<List<Domain.TechLife.DiaPhuong>>.Success(result.ToList());
                }
            }
        }

    }
}
