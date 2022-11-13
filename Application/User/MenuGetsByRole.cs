using Application.Core;
using Dapper;
using Domain;
using Domain.HueCit;
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
    public class MenuGetsByRole
    {
        public class Query : IRequest<Result<List<int>>>
        {
            public int Role { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<List<int>>>
        {
            private readonly IConfiguration _configuration;
            public Handler(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            public async Task<Result<List<int>>> Handle(Query request, CancellationToken cancellationToken)
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@Role", request.Role);
                string spName = "SP_PhanQuyenGetMenu";

                using (var connection = new SqlConnection(_configuration.GetConnectionString("HuecitConnection")))
                {   
                    connection.Open();
                    var result = await connection.QueryAsync<int>(new CommandDefinition(spName, commandType: System.Data.CommandType.StoredProcedure));

                    return Result<List<int>>.Success(result.ToList());
                }

            }
        }
    }
}
