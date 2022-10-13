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

namespace Application.SuKien
{
    public class SuKienThangGets
    {
        public class Query : IRequest<Result<List<SuKienItemResponse>>>
        {// su li tham so dau vao
            public int Month { get; set; }  
            public int Year { get; set; }
            public int pagesize { get; set; }
            public int pageindex { get; set; }

        }

        public class Handler : IRequestHandler<Query, Result<List<SuKienItemResponse>>>
        {
            private readonly IConfiguration _configuration;
            public Handler(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            public async Task<Result<List<SuKienItemResponse>>> Handle(Query request, CancellationToken cancellationToken)
            {   DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@Month", request.Month);
                dynamicParameters.Add("@Year", request.Year);
                dynamicParameters.Add("@PPAGEINDEX", request.pageindex);
                dynamicParameters.Add("@PPAGESIZE", request.pagesize);
                string spName = "SP_SuKienThangGets";
                using (var connection = new SqlConnection(_configuration.GetConnectionString("HuecitConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<SuKienItemResponse>(new CommandDefinition(spName,dynamicParameters, commandType: System.Data.CommandType.StoredProcedure));
                    return Result<List<SuKienItemResponse>>.Success(result.ToList());

                }

            }
        }
    }
}
