using Application.Core;
using Dapper;
using Domain;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.SuKien
{
    public class ChiTietSuKien
    {
        public class Query : IRequest<Result<List<DL_SuKien>>>
        {// su li tham so dau vao
           public int IDSuKien { get; set; }

        }

        public class Handler : IRequestHandler<Query, Result<List<DL_SuKien>>>
        {
            private readonly IConfiguration _configuration;
            public Handler(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            public async Task<Result<List<DL_SuKien>>> Handle(Query request, CancellationToken cancellationToken)
            {
                string spName = "SP_SuKienGet";
             
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@ID", request.IDSuKien);
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<DL_SuKien>(new CommandDefinition(spName, parameters, commandType: System.Data.CommandType.StoredProcedure));
                    return Result<List<DL_SuKien>>.Success(result.ToList());

                }

            }
        }



    }
}
