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
    public class ServiceAdd
    {
        public class Command : IRequest<Result<SYS_Services>>
        {
            public SYS_Services Request { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<SYS_Services>>
        {
            private readonly IConfiguration _configuration;
            public Handler(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            public async Task<Result<SYS_Services>> Handle(Command request, CancellationToken cancellationToken)
            {   
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@Name", request.Request.Name);
                dynamicParameters.Add("@Url", request.Request.Url);
                dynamicParameters.Add("@LinhVucKinhDoanhId", request.Request.LinhVucKinhDoanhId);
                dynamicParameters.Add("@ParentID", request.Request.ParentID);

                string spName = "SP_ServicesAdd";

                using (var connection = new SqlConnection(_configuration.GetConnectionString("HuecitConnection")))
                {   
                    connection.Open();
                    var result = await connection.QueryFirstOrDefaultAsync<SYS_Services>(new CommandDefinition(spName, parameters: dynamicParameters, commandType: System.Data.CommandType.StoredProcedure));

                    return Result<SYS_Services>.Success(result);
                }

            }
        }
    }
}
