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
    public class PhanQuyenGet
    {
        public class Query : IRequest<Result<List<SYS_PhanQuyenTrinhDien>>>
        {
            public int Role { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<List<SYS_PhanQuyenTrinhDien>>>
        {
            private readonly IConfiguration _configuration;
            public Handler(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            public async Task<Result<List<SYS_PhanQuyenTrinhDien>>> Handle(Query request, CancellationToken cancellationToken)
            {   
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@Role", request.Role);

                string spName = "SP_PhanQuyenGet";

                using (var connection = new SqlConnection(_configuration.GetConnectionString("HuecitConnection")))
                {   
                    connection.Open();
                    var result = await connection.QueryAsync<SYS_PhanQuyenTrinhDien>(new CommandDefinition(spName, parameters: dynamicParameters, commandType: System.Data.CommandType.StoredProcedure));

                    return Result<List<SYS_PhanQuyenTrinhDien>>.Success(result.ToList());
                }

            }
        }
    }
}
