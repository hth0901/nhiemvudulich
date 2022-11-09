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
    public class Image_FileUploadGet
    {
        
    
        public class Query : IRequest<Result<string>>
        {// su li tham so dau vao
            public int Id { get; set; }
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
                parameters.Add("@Id",request.Id);
                string spName = "SP_Image_FileUpLoadGet";
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

