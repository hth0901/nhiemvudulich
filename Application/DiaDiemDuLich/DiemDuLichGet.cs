using Application.Core;
using Dapper;
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

namespace Application.DiaDiemDuLich
{
    public class DiemDuLichGet 
    {
        public class Query : IRequest<Result<HoSo>>
        {// su li tham so dau vao
            public int ID { get; set; }

        }

        public class Handler : IRequestHandler<Query, Result<HoSo>>
        {
            private readonly IConfiguration _configuration;
            public Handler(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            public async Task<Result<HoSo>> Handle(Query request, CancellationToken cancellationToken)
            {
                string spName = "SP_DSDiemDuLichGet";

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@ID", request.ID);
                using (var connection = new SqlConnection(_configuration.GetConnectionString("HuecitConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryFirstOrDefaultAsync<HoSo>(new CommandDefinition(spName, parameters, commandType: System.Data.CommandType.StoredProcedure));
                    return Result<HoSo>.Success(result);

                }

            }
        }
    }
}
