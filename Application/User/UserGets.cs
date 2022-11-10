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
    public class UserGets
    {
        public class Query : IRequest<Result<List<SYS_UserTable>>>
        {
            public string TuKhoa { get; set; }
            public int? Quyen { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<List<SYS_UserTable>>>
        {
            private readonly IConfiguration _configuration;
            public Handler(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            public async Task<Result<List<SYS_UserTable>>> Handle(Query request, CancellationToken cancellationToken)
            {   
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@PTUKHOA", request.TuKhoa);
                dynamicParameters.Add("@PNHOM", request.Quyen);

                string spName = "SP_UserGets";

                using (var connection = new SqlConnection(_configuration.GetConnectionString("HuecitConnection")))
                {   
                    connection.Open();
                    var result = await connection.QueryAsync<SYS_UserTable>(new CommandDefinition(spName, parameters: dynamicParameters, commandType: System.Data.CommandType.StoredProcedure));

                    return Result<List<SYS_UserTable>>.Success(result.ToList());
                }

            }
        }
    }
}
