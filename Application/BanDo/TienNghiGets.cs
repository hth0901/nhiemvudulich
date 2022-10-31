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
    public class TienNghiGets
    {
        public class Query : IRequest<Result<List<TienNghi>>>
        {
            public int LinhVucId { get;set;}
        }

        public class Handler : IRequestHandler<Query, Result<List<TienNghi>>>
        {
            private readonly IConfiguration _configuration;
            public Handler(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            public async Task<Result<List<TienNghi>>> Handle(Query request, CancellationToken cancellationToken)
            {   DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@LinhVuc", request.LinhVucId.ToString());
                string spName = "SP_TienNghiGetsTheoLinhVuc";
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DbMainConnection")))
                {   
                    connection.Open();
                    var result = await connection.QueryAsync<TienNghi>(new CommandDefinition(spName, parameters: dynamicParameters, commandType: System.Data.CommandType.StoredProcedure));
                    return Result<List<TienNghi>>.Success(result.ToList());

                }

            }
        }
    }
}
