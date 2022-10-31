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
    public class HoSoQuery
    {
        public class Query : IRequest<Result<List<int>>>
        {
            public int LinhVucId { get;set;}
            public string HangSao { get; set; }
            public string LoaiHinhId { get; set; }
            public string LoaiDiaDiemAnUong { get; set; }
            public string TienNghi { get; set; }
            public string LoaiPhong { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<List<int>>>
        {
            private readonly IConfiguration _configuration;
            public Handler(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            public async Task<Result<List<int>>> Handle(Query request, CancellationToken cancellationToken)
            {   
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@LinhVucId", request.LinhVucId);
                dynamicParameters.Add("@HangSao", request.HangSao);
                dynamicParameters.Add("@LoaiHinhId", request.LoaiHinhId);
                dynamicParameters.Add("@LoaiDiaDiemAnUong", request.LoaiDiaDiemAnUong);
                dynamicParameters.Add("@TienNghi", request.TienNghi);
                dynamicParameters.Add("@LoaiPhong", request.LoaiPhong);

                string spName = "SP_HoSoFilter";

                using (var connection = new SqlConnection(_configuration.GetConnectionString("DbMainConnection")))
                {   
                    connection.Open();
                    var result = await connection.QueryAsync<int>(new CommandDefinition(spName, parameters: dynamicParameters, commandType: System.Data.CommandType.StoredProcedure));

                    return Result<List<int>>.Success(result.ToList());
                }

            }
        }
    }
}
