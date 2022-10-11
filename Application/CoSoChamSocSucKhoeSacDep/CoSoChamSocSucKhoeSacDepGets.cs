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

namespace Application.CoSoChamSocSucKhoeSacDep
{
    public class CoSoChamSocSucKhoeSacDepGets
    {
        public class Query : IRequest<Result<List<HoSo>>>
        {// su li tham so dau vao

        }

        public class Handler : IRequestHandler<Query, Result<List<HoSo>>>
        {
            private readonly IConfiguration _configuration;
            public Handler(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            public async Task<Result<List<HoSo>>> Handle(Query request, CancellationToken cancellationToken)
            {
                string spName = "SP_CoSoChamSocSucKhoeSacDepGets";
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<HoSo>(new CommandDefinition(spName, parameters: null, commandType: System.Data.CommandType.StoredProcedure));
                    return Result<List<HoSo>>.Success(result.ToList());// compare of list  

                }

            }
        }

    }
}
