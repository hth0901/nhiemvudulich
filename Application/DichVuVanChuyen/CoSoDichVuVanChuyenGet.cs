using Application.Core;
using Dapper;
using Domain;
using Domain.TechLife;
using MediatR;
using Microsoft.Extensions.Configuration;
using Persistence;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DichVuVanChuyen
{
    public class CoSoDichVuVanChuyenGet
    {
        public class Query : IRequest<Result<HoSo>>
        {
            public int ID { get; set; } 
        }
        public class Handler : IRequestHandler<Query, Result<HoSo>>
        {
            private readonly IConfiguration _configuration;
            private readonly DataContext _context;

            public Handler(DataContext context, IConfiguration configuration)
            {
                _context = context;
                _configuration = configuration;

            }

            public async Task<Result<HoSo>> Handle(Query request, CancellationToken cancellationToken)
            {   DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@ID", request.ID);
                string spName = "SP_DSCoSoDichVuVanChuyenGet";
                using (var connection = new SqlConnection(_configuration.GetConnectionString("HuecitConnection")))
                {
                    connection.Open();
                    //var result = await connection.QueryAsync<Place>(spName);
                    var result = await connection.QueryFirstOrDefaultAsync<HoSo>(new CommandDefinition(spName, dynamicParameters, commandType: System.Data.CommandType.StoredProcedure));
                    return Result<HoSo>.Success(result);
                }
            }
        }

    }
}
