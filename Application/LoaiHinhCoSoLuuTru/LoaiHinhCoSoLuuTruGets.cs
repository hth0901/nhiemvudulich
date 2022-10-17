using Application.Core;
using Dapper;
using Domain;
using Domain.ResponseEntity;
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

namespace Application.LoaiHinhCoSoLuuTru
{
    public class LoaiHinhCoSoLuuTruGets
    {
        public class Query : IRequest<Result<List<LoaiHinhItemResponse>>>
        {// su li tham so dau vao
         

        }

        public class Handler : IRequestHandler<Query, Result<List<LoaiHinhItemResponse>>>
        {
            private readonly IConfiguration _configuration;
            public Handler(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            public async Task<Result<List<LoaiHinhItemResponse>>> Handle(Query request, CancellationToken cancellationToken)
            {
  
                string spName = "SP_DSLoaiHinh_LuuTruGets";
                using (var connection = new SqlConnection(_configuration.GetConnectionString("HuecitConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<LoaiHinhItemResponse>(new CommandDefinition(spName, parameters:null, commandType: System.Data.CommandType.StoredProcedure));
                    return Result<List<LoaiHinhItemResponse>>.Success(result.ToList());

                }

            }
        }
    }
}
