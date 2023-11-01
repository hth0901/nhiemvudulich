using Application.Core;
using Dapper;
using Domain.ResponseEntity;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.HuongDanVienDuLich
{
    public class HuongDanVienDuLichGets
    {
        public class Query : IRequest<Result<List<HuongDanVienDuLichItemResponse>>>
        {// su li tham so dau vao

            public int pagesize { get; set; }
            public int pageindex { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<List<HuongDanVienDuLichItemResponse>>>
        {
            private readonly IConfiguration _configuration;
            public Handler(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            public async Task<Result<List<HuongDanVienDuLichItemResponse>>> Handle(Query request, CancellationToken cancellationToken)
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@PPAGEINDEX", request.pageindex);
                parameters.Add("@PPAGESIZE", request.pagesize);
                string spName = "SP_DSHuongDanVienDuLichGets";
                using (var connection = new SqlConnection(_configuration.GetConnectionString("HuecitConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<HuongDanVienDuLichItemResponse>(new CommandDefinition(spName, parameters: parameters, commandType: System.Data.CommandType.StoredProcedure));
                    return Result<List<HuongDanVienDuLichItemResponse>>.Success(result.ToList());

                }

            }
        }
    }
}
