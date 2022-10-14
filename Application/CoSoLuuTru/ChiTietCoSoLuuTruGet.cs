using Application.Core;
using Dapper;
using Domain;
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
    public class ChiTietCoSoLuuTruGet
    {
        public class Query : IRequest<Result<HoSo>>
        {// su li tham so dau vao
            public int ID {get;set;}
        }

        public class Handler : IRequestHandler<Query, Result<HoSo>>
        {
            private readonly IConfiguration _configuration;
            public Handler(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            public async Task<Result<HoSo>> Handle(Query request, CancellationToken cancellationToken)
            {   DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@Id", request.ID);
                string spName = "SP_DSCoSoLuuTruGet";
                using (var connection = new SqlConnection(_configuration.GetConnectionString("HuecitConnection")))
                {   
                    connection.Open();
                    var result = await connection.QueryFirstOrDefaultAsync<HoSo>(new CommandDefinition(spName, parameters: dynamicParameters, commandType: System.Data.CommandType.StoredProcedure));
                    return Result<HoSo>.Success(result);// compare of list  

                }

            }
        }
    }
}
