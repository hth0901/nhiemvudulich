﻿using Application.Core;
using Dapper;
using Domain.ResponseEntity;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CoSoLuuTru
{
    public class CoSoLuuTruGets
    {
        public class Query : IRequest<Result<List<HoSoCoSoLuuTruItemResponse>>>
        {// su li tham so dau vao
            public int pagesize { get; set; }
            public int pageindex { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<List<HoSoCoSoLuuTruItemResponse>>>
        {
            private readonly IConfiguration _configuration;
            public Handler(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            public async Task<Result<List<HoSoCoSoLuuTruItemResponse>>> Handle(Query request, CancellationToken cancellationToken)
            {
                string spName = "SP_DSCoSoLuuTruChiTietGets";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@PPAGEINDEX", request.pageindex);
                parameters.Add("@PPAGESIZE", request.pagesize);
                using (var connection = new SqlConnection(_configuration.GetConnectionString("HuecitConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<HoSoCoSoLuuTruItemResponse>(new CommandDefinition(spName, parameters, commandType: System.Data.CommandType.StoredProcedure));
                    return Result<List<HoSoCoSoLuuTruItemResponse>>.Success(result.ToList());// compare of list  

                }

            }
        }


    }
}
