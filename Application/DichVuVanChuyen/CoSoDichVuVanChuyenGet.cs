using Application.Core;
using Dapper;
using Domain;
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
        public class Query : IRequest<Result<List<HoSo>>>
        {
            public int ID { get; set; } 
        }
        public class Handler : IRequestHandler<Query, Result<List<HoSo>>>
        {
            private readonly IConfiguration _configuration;
            private readonly DataContext _context;

            public Handler(DataContext context, IConfiguration configuration)
            {
                _context = context;
                _configuration = configuration;

            }

            public async Task<Result<List<HoSo>>> Handle(Query request, CancellationToken cancellationToken)
            {   DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@ID", request.ID);
                string spName = "SP_DanhSachCoSoDichVuVanChuyenGet";
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();
                    //var result = await connection.QueryAsync<Place>(spName);
                    var result = await connection.QueryAsync<HoSo>(new CommandDefinition(spName, dynamicParameters, commandType: System.Data.CommandType.StoredProcedure));
                    return Result<List<HoSo>>.Success(result.ToList());
                }
            }
        }

    }
}
