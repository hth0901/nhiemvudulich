using Application.Core;
using Dapper;
using Domain;
using Domain.ResponseEntity;
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

namespace Application.CoSoLuuTru
{
    public class CoSoLuuTruGets
    {
        public class Query : IRequest<Result<List<HoSoLuTruItemResponse>>>
        {// su li tham so dau vao
            public int pagesize { get; set; }
            public int pageindex { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<List<HoSoLuTruItemResponse>>>
        {
            private readonly IConfiguration _configuration;
            public Handler(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            public async Task<Result<List<HoSoLuTruItemResponse>>> Handle(Query request, CancellationToken cancellationToken)
            {
                string spName = "SP_CoSoLuuTruGets";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@PPAGEINDEX", request.pageindex);
                parameters.Add("@PPAGESIZE", request.pagesize);
                using (var connection = new SqlConnection(_configuration.GetConnectionString("HuecitConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<HoSoLuTruItemResponse>(new CommandDefinition(spName, parameters, commandType: System.Data.CommandType.StoredProcedure));
                    return Result<List<HoSoLuTruItemResponse>>.Success(result.ToList());// compare of list  

                }

            }
        }


    }
}
