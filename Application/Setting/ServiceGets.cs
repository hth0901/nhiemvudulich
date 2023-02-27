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
    public class ServiceGets
    {
        public class Query : IRequest<Result<List<SYS_Services>>>
        {
        }

        public class Handler : IRequestHandler<Query, Result<List<SYS_Services>>>
        {
            private readonly IConfiguration _configuration;
            public Handler(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            public async Task<Result<List<SYS_Services>>> Handle(Query request, CancellationToken cancellationToken)
            {   
                string spName = "SP_ServicesGets";

                using (var connection = new SqlConnection(_configuration.GetConnectionString("HuecitConnection")))
                {   
                    connection.Open();
                    var result = await connection.QueryAsync<SYS_Services>(new CommandDefinition(spName, commandType: System.Data.CommandType.StoredProcedure));

                    return Result<List<SYS_Services>>.Success(result.ToList());
                }

            }
        }
    }
}
