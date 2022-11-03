using Application.Core;
using Dapper;
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
using static Dapper.SqlMapper;

namespace Application.SuKien
{
    public class SuKienThangTheoChuDeGets
    {
        public class Query : IRequest<Result<List<SuKienItemResponse>>>
        {// su li tham so dau vao
            public SuKienChuDeThang search { get; set; }
      
        }

        public class Handler : IRequestHandler<Query, Result<List<SuKienItemResponse>>>
        {
            private readonly IConfiguration _configuration;
            public Handler(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            public async Task<Result<List<SuKienItemResponse>>> Handle(Query request, CancellationToken cancellationToken)
            {
                string spName = "SP_SuKienThangChuDeGets";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@IdChuDe", request.search.IDChuDeThang);
                parameters.Add("@Month", request.search.Month);
                parameters.Add("@Year", request.search.Year);
                parameters.Add("@PPAGEINDEX", request.search.pageindex
                    );
                parameters.Add("@PPAGESIZE", request.search.pagesize);
                using (var connection = new SqlConnection(_configuration.GetConnectionString("HuecitConnection")))
                {   
                    connection.Open();
                    var result = await connection.QueryAsync<SuKienItemResponse>(new CommandDefinition(spName, parameters, commandType: System.Data.CommandType.StoredProcedure));
                    return Result<List<SuKienItemResponse>>.Success(result.ToList());

                }

            }
        }


    }
}
