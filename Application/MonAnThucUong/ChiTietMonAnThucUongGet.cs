﻿using Application.Core;
using Dapper;
using Domain;
using Domain.HueCit;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.MonAnThucUong
{
    public class ChiTietMonAnThucUongGet
    {

        public class Query : IRequest<Result<DL_MonAnThucUong>>
        {// su li tham so dau vao
            public int ID { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<DL_MonAnThucUong>>
        {
            private readonly IConfiguration _configuration;
            public Handler(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            public async Task<Result<DL_MonAnThucUong>> Handle(Query request, CancellationToken cancellationToken)
            {
                string spName = "[SP_DSAmThucGet]";
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@ID", request.ID);
                using (var connection = new SqlConnection(_configuration.GetConnectionString("HuecitConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryFirstOrDefaultAsync<DL_MonAnThucUong>(new CommandDefinition(spName,dynamicParameters, commandType: System.Data.CommandType.StoredProcedure));
                    return Result<DL_MonAnThucUong>.Success(result);

                }

            }
        }
    }
}
