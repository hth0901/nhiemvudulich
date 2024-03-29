﻿using Application.Core;
using Dapper;
using Domain;
using Domain.HueCit;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DuBaoThoiTiet
{
    public class DuBaoThoiTietGet
    {
        public class Query : IRequest<Result<DL_ThoiTiet>>
        {
            public string ID { get;set; }
        }
        public class Handler : IRequestHandler<Query, Result<DL_ThoiTiet>>
        {
            private readonly IConfiguration _configuration;

            public Handler(IConfiguration configuration)
            {
                _configuration = configuration;

            }
            public async Task<Result<DL_ThoiTiet>> Handle(Query request, CancellationToken cancellationToken)
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id",request.ID);
                string spName = "SP_ThoiTietGet";
                using (var connection = new SqlConnection(_configuration.GetConnectionString("HuecitConnection")))
                {
                    connection.Open();
                    //var result = await connection.QueryAsync<Place>(spName);
                    var result = await connection.QueryFirstOrDefaultAsync<DL_ThoiTiet>(new CommandDefinition(spName, parameters, commandType: System.Data.CommandType.StoredProcedure));
                    return Result<DL_ThoiTiet>.Success(result);
                }
            }
            
        }

    }
}
