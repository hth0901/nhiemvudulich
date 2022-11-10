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
    public class RoleEdit
    {
        public class Command : IRequest<Result<SYS_Roles>>
        {
            public SYS_Roles Request { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<SYS_Roles>>
        {
            private readonly IConfiguration _configuration;
            public Handler(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            public async Task<Result<SYS_Roles>> Handle(Command request, CancellationToken cancellationToken)
            {   
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@ID", request.Request.ID);
                dynamicParameters.Add("@Ten", request.Request.Ten);
                dynamicParameters.Add("@MoTa", request.Request.MoTa);
                dynamicParameters.Add("@TrangThai", request.Request.TrangThai.ToString());

                string spName = "SP_RoleEdit";

                using (var connection = new SqlConnection(_configuration.GetConnectionString("HuecitConnection")))
                {   
                    connection.Open();
                    var result = await connection.QueryFirstOrDefaultAsync<SYS_Roles>(new CommandDefinition(spName, parameters: dynamicParameters, commandType: System.Data.CommandType.StoredProcedure));

                    return Result<SYS_Roles>.Success(result);
                }

            }
        }
    }
}
