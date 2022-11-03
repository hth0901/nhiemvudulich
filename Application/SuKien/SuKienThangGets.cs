using Application.Core;
using Dapper;
using Domain.HueCit;
using Domain.RequestEntity;
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
        public class Query : IRequest<Result<List<SuKienTheoThangItemResponse>>>
        {// su li tham so dau vao
         public SuKienChuDeThang search { get; set; }

        }

        public class Handler : IRequestHandler<Query, Result<List<SuKienTheoThangItemResponse>>>
        {
            private readonly IConfiguration _configuration;
            public Handler(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            public async Task<Result<List<SuKienTheoThangItemResponse>>> Handle(Query request, CancellationToken cancellationToken)
            {   DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@Month", request.search.Month);
                dynamicParameters.Add("@Year", request.search.Year);
                dynamicParameters.Add("@PPAGEINDEX", request.search.pageindex);
                dynamicParameters.Add("@PPAGESIZE", request.search.pagesize);
                string spName = "SP_SuKienThangGets";
                using (var connection = new SqlConnection(_configuration.GetConnectionString("HuecitConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<SuKienTheoThangItemResponse>(new CommandDefinition(spName,dynamicParameters, commandType: System.Data.CommandType.StoredProcedure));
                    return Result<List<SuKienTheoThangItemResponse>>.Success(result.ToList());

                }

            }
        }
    }
}
