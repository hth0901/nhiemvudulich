﻿using Application.Core;
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
    public class LoaiLeHoiGets
    {
        public class Query : IRequest<Result<List<LoaiLeHoi>>>
        {

        }

        public class Handler : IRequestHandler<Query, Result<List<LoaiLeHoi>>>
        {
            private readonly IConfiguration _configuration;
            public Handler(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            public async Task<Result<List<LoaiLeHoi>>> Handle(Query request, CancellationToken cancellationToken)
            {   
                string spName = "SP_LeHoiLoaiGets";

                using (var connection = new SqlConnection(_configuration.GetConnectionString("HuecitConnection")))
                {   
                    connection.Open();
                    var result = await connection.QueryAsync<LoaiLeHoi>(new CommandDefinition(spName, commandType: System.Data.CommandType.StoredProcedure));

                    return Result<List<LoaiLeHoi>>.Success(result.ToList());
                }

            }
        }
    }
}
