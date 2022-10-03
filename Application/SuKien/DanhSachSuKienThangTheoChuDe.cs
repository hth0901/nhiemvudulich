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
    public class DanhSachSuKienThangTheoChuDe
    {
        public class Query : IRequest<Result<List<SuKienChuDeThang>>>
        {// su li tham so dau vao
            public int IDChuDe { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<List<SuKienChuDeThang>>>
        {
            private readonly IConfiguration _configuration;
            public Handler(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            public async Task<Result<List<SuKienChuDeThang>>> Handle(Query request, CancellationToken cancellationToken)
            {
                string spName = "SP_SuKienThangChuDeGets";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@PIDChuDe", request.IDChuDe);
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {   
                    connection.Open();
                    var result = await connection.QueryAsync<SuKienChuDeThang>(new CommandDefinition(spName, parameters, commandType: System.Data.CommandType.StoredProcedure));
                    return Result<List<SuKienChuDeThang>>.Success(result.ToList());

                }

            }
        }


    }
}
