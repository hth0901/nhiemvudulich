using Application.Core;
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

namespace Application.LoaiCoSoVuiChoiGiaiTri
{
    public class LoaiHinhCoSoVuiChoiGiaiTriGets
    {
        public class Query : IRequest<Result<List<DanhMuc>>>
        {// su li tham so dau vao

        }

        public class Handler : IRequestHandler<Query, Result<List<DanhMuc>>>
        {
            private readonly IConfiguration _configuration;
            public Handler(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            public async Task<Result<List<DanhMuc>>> Handle(Query request, CancellationToken cancellationToken)
            {
                string spName = "DanhMucChuDeVuiChoiGiaiTriGets";
                using (var connection = new SqlConnection(_configuration.GetConnectionString("TechLifeConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<DanhMuc>(new CommandDefinition(spName, parameters: null, commandType: System.Data.CommandType.StoredProcedure));
                    return Result<List<DanhMuc>>.Success(result.ToList());

                }

            }
        }
    }
}
