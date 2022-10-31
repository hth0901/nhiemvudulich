using Application.Core;
using Dapper;
using Domain;
using Domain.TechLife;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.BanDo
{
    public class LoaiPhongGets
    {
        public class Query : IRequest<Result<List<TienNghi>>>
        {
        }

        public class Handler : IRequestHandler<Query, Result<List<TienNghi>>>
        {
            private readonly IConfiguration _configuration;
            public Handler(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            public async Task<Result<List<TienNghi>>> Handle(Query request, CancellationToken cancellationToken)
            {   
                string spName = "SP_LoaiPhongGets";
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DbMainConnection")))
                {   
                    connection.Open();
                    var result = await connection.QueryAsync<TienNghi>(new CommandDefinition(spName, commandType: System.Data.CommandType.StoredProcedure));
                    return Result<List<TienNghi>>.Success(result.ToList());

                }

            }
        }
    }
}
