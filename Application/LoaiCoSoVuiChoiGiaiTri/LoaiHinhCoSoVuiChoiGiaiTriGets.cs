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

namespace Application.LoaiCoSoVuiChoiGiaiTri
{
    public class LoaiHinhCoSoVuiChoiGiaiTriGets
    {
        public class Query : IRequest<Result<List<LoaiHinh_ID_Ten>>>
        {// su li tham so dau vao
            public int pagesize { get; set; }
            public int pageindex { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<List<LoaiHinh_ID_Ten>>>
        {
            private readonly IConfiguration _configuration;
            public Handler(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            public async Task<Result<List<LoaiHinh_ID_Ten>>> Handle(Query request, CancellationToken cancellationToken)
            {
                DynamicParameters parameters = new DynamicParameters();
          
                string spName = "SP_DSDanhMucChuDeVuiChoiGiaiTriGets";
                using (var connection = new SqlConnection(_configuration.GetConnectionString("HuecitConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<LoaiHinh_ID_Ten>(new CommandDefinition(spName, parameters: null, commandType: System.Data.CommandType.StoredProcedure));
                    return Result<List<LoaiHinh_ID_Ten>>.Success(result.ToList());

                }

            }
        }
    }
}
