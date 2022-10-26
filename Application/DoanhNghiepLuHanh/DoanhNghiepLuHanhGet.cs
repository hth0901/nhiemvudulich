﻿using Application.Core;
using Dapper;
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

namespace Application.DoanhNghiep
{
    public class DoanhNghiepLuHanhGet
    {
        public class Query : IRequest<Result<List<HoSoLuTruItemResponse>>>
        {// su li tham so dau vao
            public int ID { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<List<HoSoLuTruItemResponse>>>
        {
            private readonly IConfiguration _configuration;
            public Handler(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            public async Task<Result<List<HoSoLuTruItemResponse>>> Handle(Query request, CancellationToken cancellationToken)
            {
                string spName = "[SP_DSDoanhNghiepLuHanhGet]";
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@ID", request.ID);
                using (var connection = new SqlConnection(_configuration.GetConnectionString("HuecitConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<HoSoLuTruItemResponse>(new CommandDefinition(spName, dynamicParameters, commandType: System.Data.CommandType.StoredProcedure));
                    return Result<List<HoSoLuTruItemResponse>>.Success(result.ToList());

                }

            }
        }
    }
}