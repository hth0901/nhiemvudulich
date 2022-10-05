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
    public class ChiTietMonAnThucUong
    {

        public class Query : IRequest<Result<List<DL_MonAnThucUong>>>
        {// su li tham so dau vao
            public int ID { get; set; }
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
                string spName = "[SP_AmThucGet]";
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@ID", request.ID);
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<DL_MonAnThucUong>(new CommandDefinition(spName,dynamicParameters, commandType: System.Data.CommandType.StoredProcedure));
                    return Result<List<DL_MonAnThucUong>>.Success(result.ToList());

                }

            }
        }
    }
}
