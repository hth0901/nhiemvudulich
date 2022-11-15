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
    public class UserGet
    {
        public class Query : IRequest<Result<SYS_UserTable>>
        {
            public string Username { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<SYS_UserTable>>
        {
            private readonly IConfiguration _configuration;
            public Handler(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            public async Task<Result<SYS_UserTable>> Handle(Query request, CancellationToken cancellationToken)
            {   
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@PUSER", request.Username);

                string spName = "SP_UserGet";

                using (var connection = new SqlConnection(_configuration.GetConnectionString("HuecitConnection")))
                {   
                    connection.Open();
                    var result = await connection.QueryFirstOrDefaultAsync<SYS_UserTable>(new CommandDefinition(spName, parameters: dynamicParameters, commandType: System.Data.CommandType.StoredProcedure));

                    return Result<SYS_UserTable>.Success(result);
                }

            }
        }
    }
}
