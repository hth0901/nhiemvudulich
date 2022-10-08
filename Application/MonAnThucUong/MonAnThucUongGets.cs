using Application.Core;
using Dapper;
using Domain;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.MonAnThucUong
{
    public class MonAnThucUongGets
    {
        public class Query : IRequest<Result<List<DL_MonAnThucUong>>>
        {// su li tham so dau vao

        }

        public class Handler : IRequestHandler<Query, Result<List<DL_MonAnThucUong>>>
        {
            private readonly IConfiguration _configuration;
            public Handler(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            public async Task<Result<List<DL_MonAnThucUong>>> Handle(Query request, CancellationToken cancellationToken)
            {
                string spName = "SP_AmThucGets";
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<DL_MonAnThucUong>(new CommandDefinition(spName, parameters: null, commandType: System.Data.CommandType.StoredProcedure));
                    return Result<List<DL_MonAnThucUong>>.Success(result.ToList());

                }

            }
        }
    }
}
