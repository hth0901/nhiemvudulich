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
    public class SettingAdd
    {
        public class Command : IRequest<Result<SYS_Setting>>
        {
            public SYS_Setting Request { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<SYS_Setting>>
        {
            private readonly IConfiguration _configuration;
            public Handler(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            public async Task<Result<SYS_Setting>> Handle(Command request, CancellationToken cancellationToken)
            {   
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@GiaTri", request.Request.GiaTri.ToString());
                dynamicParameters.Add("@MoTa", request.Request.MoTa);

                string spName = "SP_SettingAdd";

                using (var connection = new SqlConnection(_configuration.GetConnectionString("HuecitConnection")))
                {   
                    connection.Open();
                    var result = await connection.QueryFirstOrDefaultAsync<SYS_Setting>(new CommandDefinition(spName, parameters: dynamicParameters, commandType: System.Data.CommandType.StoredProcedure));

                    return Result<SYS_Setting>.Success(result);
                }

            }
        }
    }
}
