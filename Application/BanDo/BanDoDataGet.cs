using Application.Core;
using Dapper;
using Domain;
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

namespace Application.BanDo
{
    public class BanDoDataGet
    {
        public class Query : IRequest<Result<HoSo>>
        {
            public int ID { get; set; }
            public int LinhVuc { get; set; }
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
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@LinhVuc", request.LinhVuc);
                dynamicParameters.Add("@ID", request.ID);

                string spName = "SP_BanDoGetData";
                using (var connection = new SqlConnection(_configuration.GetConnectionString("TechLifeConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryFirstOrDefaultAsync<HoSo>(new CommandDefinition(spName, parameters: dynamicParameters, commandType: System.Data.CommandType.StoredProcedure));
                    if (result != null)
                    {
                        DynamicParameters param = new DynamicParameters();
                        param.Add("@ID", request.ID);
                        List<FileUploadModel> files = (connection.Query<FileUploadModel>("SP_BanDoGetFiles", param, commandType: System.Data.CommandType.StoredProcedure)).ToList();
                        result.Images = files;
                    }

                    return Result<HoSo>.Success(result);
                }
            }
        }
    }
}
