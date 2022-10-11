using Application.Core;
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

namespace Application.LoaiMonAnThucUong
{
    public class LoaiMonAnThucUongGets
    {


        public class Query : IRequest<Result<List<DL_MonAnThucUong_Loai>>>
        {// su li tham so dau vao

        }

        public class Handler : IRequestHandler<Query, Result<List<DL_MonAnThucUong_Loai>>>
        {
            private readonly IConfiguration _configuration;
            public Handler(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            public async Task<Result<List<DL_MonAnThucUong_Loai>>> Handle(Query request, CancellationToken cancellationToken)
            {
                string spName = "SP_LoaiAmThucGets";
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<DL_MonAnThucUong_Loai>(new CommandDefinition(spName, parameters: null, commandType: System.Data.CommandType.StoredProcedure));
                    return Result<List<DL_MonAnThucUong_Loai>>.Success(result.ToList());

                }

            }
        }
    }
}
