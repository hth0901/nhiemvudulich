using Application.Core;
using Dapper;
using Domain;
using Domain.ResponseEntity;
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
    public class CoSoDichVuVanChuyenGets
    {
        public class Query : IRequest<Result<List<HoSoLuTruItemResponse>>>
        {
            public int pagesize { get; set; }
            public int pageindex { get; set; }

        }
        public class Handler : IRequestHandler<Query, Result<List<HoSoLuTruItemResponse>>>
        {
            private readonly IConfiguration _configuration;
            private readonly DataContext _context;

            public Handler(DataContext context, IConfiguration configuration)
            {
                _context = context;
                _configuration = configuration;

            }

            public async Task<Result<List<HoSoLuTruItemResponse>>> Handle(Query request, CancellationToken cancellationToken)
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@PPAGEINDEX", request.pageindex);
                parameters.Add("@PPAGESIZE", request.pagesize);
                string spName = "SP_DanhSachCoSoDichVuVanChuyenGets";
                using (var connection = new SqlConnection(_configuration.GetConnectionString("HuecitConnetion")))
                {
                    connection.Open();
                    //var result = await connection.QueryAsync<Place>(spName);
                    var result = await connection.QueryAsync<HoSoLuTruItemResponse>(new CommandDefinition(spName, parameters, commandType: System.Data.CommandType.StoredProcedure));
                    return Result<List<HoSoLuTruItemResponse>>.Success(result.ToList());
                }
            }
        }

    }
}
