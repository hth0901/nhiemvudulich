using Application.Core;
using Dapper;
using Domain;
using Domain.HueCit;
using Domain.ResponseEntity;
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
        public class Query : IRequest<Result<List<MonAnThucUongItemResponse>>>
        {// su li tham so dau vao
            public int pagesize { get; set; }
            public int pageindex { get; set; }

        }

        public class Handler : IRequestHandler<Query, Result<List<MonAnThucUongItemResponse>>>
        {
            private readonly IConfiguration _configuration;
            public Handler(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            public async Task<Result<List<MonAnThucUongItemResponse>>> Handle(Query request, CancellationToken cancellationToken)
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@PPAGEINDEX", request.pageindex);
                parameters.Add("@PPAGESIZE", request.pagesize);
                string spName = "SP_DSAmThucGets";
                using (var connection = new SqlConnection(_configuration.GetConnectionString("HuecitConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<MonAnThucUongItemResponse>(new CommandDefinition(spName, parameters, commandType: System.Data.CommandType.StoredProcedure));
                    return Result<List<MonAnThucUongItemResponse>>.Success(result.ToList());

                }

            }
        }
    }
}
