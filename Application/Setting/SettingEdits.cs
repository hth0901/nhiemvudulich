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
    public class SettingEdits
    {
        public class Command : IRequest<Result<int>>
        {
            public SYS_SettingRequest Request { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<int>>
        {
            private readonly IConfiguration _configuration;
            public Handler(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            public async Task<Result<int>> Handle(Command request, CancellationToken cancellationToken)
            {
                string spName = "SP_SettingEdit";

                using (var connection = new SqlConnection(_configuration.GetConnectionString("HuecitConnection")))
                {   
                    connection.Open();
                    int num = 0;
                    for (int i = 1; i < 10; i++)
                    {
                        DynamicParameters dynamicParameters = new DynamicParameters();
                        dynamicParameters.Add("@ID", i);
                        if (i == 1)
                        {
                            dynamicParameters.Add("@GiatRI", request.Request.RadiusMin.ToString());
                        }
                        else if (i == 2)
                        {
                            dynamicParameters.Add("@GiatRI", request.Request.RadiusMax.ToString());
                        }
                        else if (i == 3)
                        {
                            dynamicParameters.Add("@GiatRI", request.Request.Unit.ToString());
                        }
                        else if (i == 4)
                        {
                            dynamicParameters.Add("@GiatRI", request.Request.SendEmail.ToString());
                        }
                        else if (i == 5)
                        {
                            dynamicParameters.Add("@GiatRI", request.Request.SendPassword.ToString());
                        }
                        else if (i == 6)
                        {
                            dynamicParameters.Add("@GiatRI", request.Request.SendDisplayName.ToString());
                        }
                        else if (i == 7)
                        {
                            dynamicParameters.Add("@GiatRI", request.Request.ToEmail.ToString());
                        }
                        else if (i == 8)
                        {
                            dynamicParameters.Add("@GiatRI", request.Request.ToName.ToString());
                        }
                        else if (i == 9)
                        {
                            dynamicParameters.Add("@GiatRI", request.Request.Scheduler.ToString());
                        }

                        var result = await connection.QueryFirstOrDefaultAsync<SYS_Setting>(new CommandDefinition(spName, parameters: dynamicParameters, commandType: System.Data.CommandType.StoredProcedure));
                        if (result != null)
                        {
                            num++;
                        }
                    }

                    return Result<int>.Success(num);
                }

            }
        }
    }
}
