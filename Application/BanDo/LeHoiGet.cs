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
    public class LeHoiGet
    {
        public class Query : IRequest<Result<LeHoiModel>>
        {
            public string ID { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<LeHoiModel>>
        {
            private readonly IConfiguration _configuration;
            public Handler(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            public async Task<Result<LeHoiModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@ID", request.ID);

                string spName = "SP_LeHoiGet";
                using (var connection = new SqlConnection(_configuration.GetConnectionString("HuecitConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryFirstOrDefaultAsync<LeHoiModel>(new CommandDefinition(spName, parameters: dynamicParameters, commandType: System.Data.CommandType.StoredProcedure));
                    if (result != null)
                    {
                        DynamicParameters param = new DynamicParameters();
                        param.Add("@LoaiDoiTuong", "CN_LeHoi");
                        param.Add("@IDDoiTuong", request.ID);
                        List<FileUpload> files = (connection.Query<FileUpload>("SP_ThuVienMediaGets", param, commandType: System.Data.CommandType.StoredProcedure)).ToList();
                        result.Files = files;
                    }

                    return Result<LeHoiModel>.Success(result);
                }
            }
        }
    }
}
