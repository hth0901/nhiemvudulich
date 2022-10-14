using Application.Core;
using Dapper;
using Domain.HueCit;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.HuongDanVienDuLich
{
    public class HuongDanVienDuLichGet
    {
        public class Query : IRequest<Result<List<Domain.HueCit.HuongDanVienDuLich>>>
        {// su li tham so dau vao
            public int ID { get; set; }

        }

        public class Handler : IRequestHandler<Query, Result<List<Domain.HueCit.HuongDanVienDuLich>>>
        {
            private readonly IConfiguration _configuration;
            public Handler(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            public async Task<Result<List<Domain.HueCit.HuongDanVienDuLich>>> Handle(Query request, CancellationToken cancellationToken)
            {
                string spName = "SP_DSHuongDanVienDuLichGet";

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@ID", request.ID);
                using (var connection = new SqlConnection(_configuration.GetConnectionString("HuecitConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<Domain.HueCit.HuongDanVienDuLich>(new CommandDefinition(spName, parameters, commandType: System.Data.CommandType.StoredProcedure));
                    return Result<List<Domain.HueCit.HuongDanVienDuLich>>.Success(result.ToList());

                }

            }
        }
    }
}
