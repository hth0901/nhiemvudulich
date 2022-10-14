using Application.Core;
using Dapper;
using Domain;
using Domain.HueCit;
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

namespace Application.LoaiMonAnThucUong
{
    public class LoaiMonAnThucUongGets
    {


        public class Query : IRequest<Result<List<LoaiHinhItemResponse>>>
        {// su li tham so dau vao
            public int pagesize { get; set; }
            public int pageindex { get; set; }

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
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@PPAGEINDEX", request.pageindex);
                parameters.Add("@PPAGESIZE", request.pagesize);
                string spName = "SP_DSLoaiAmThucGets";
                using (var connection = new SqlConnection(_configuration.GetConnectionString("HuecitConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<LoaiHinhItemResponse>(new CommandDefinition(spName, parameters, commandType: System.Data.CommandType.StoredProcedure));
                    return Result<List<LoaiHinhItemResponse>>.Success(result.ToList());

                }

            }
        }
    }
}
