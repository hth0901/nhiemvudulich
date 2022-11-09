using Application.Core;
using Dapper;
using Domain.ResponseEntity;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Image_Url
{
    public class Image_ThuVienMediaGet
    {
        public class Query : IRequest<Result<string>>
        {// su li tham so dau vao
         public string IDDoiTuong { get; set; }
         public string LoaiDoiTuong { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<string>>
        {
            private readonly IConfiguration _configuration;
            public Handler(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            public async Task<Result<string>> Handle(Query request, CancellationToken cancellationToken)
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@IDDoiTuong",request.IDDoiTuong);
                parameters.Add("@LoaiDoiTuong", request.LoaiDoiTuong);
                string spName = "SP_Image_ThuVienMediaGet";
                using (var connection = new SqlConnection(_configuration.GetConnectionString("HuecitConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryFirstOrDefaultAsync<string>(new CommandDefinition(spName, parameters: parameters, commandType: System.Data.CommandType.StoredProcedure));
                    return Result<string>.Success(result);

                }

            }
        }
    }
}
