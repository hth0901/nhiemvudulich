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

namespace Application.BanDo
{
    public class LinhVucPhanAnhGets
    {
        public class Query : IRequest<Result<List<LinhVucPhanAnh>>>
        {

        }

        public class Handler : IRequestHandler<Query, Result<List<LinhVucPhanAnh>>>
        {
            private readonly IConfiguration _configuration;
            public Handler(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            public async Task<Result<List<LinhVucPhanAnh>>> Handle(Query request, CancellationToken cancellationToken)
            {   
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@IsEnable", true);

                string spName = "SP_LoaiLinhVucPhanAnhHienTruongGetsIsEnable";

                using (var connection = new SqlConnection(_configuration.GetConnectionString("HuecitConnection")))
                {   
                    connection.Open();
                    var result = await connection.QueryAsync<LinhVucPhanAnh>(new CommandDefinition(spName, parameters: dynamicParameters, commandType: System.Data.CommandType.StoredProcedure));

                    return Result<List<LinhVucPhanAnh>>.Success(result.ToList());
                }

            }
        }
    }
}
