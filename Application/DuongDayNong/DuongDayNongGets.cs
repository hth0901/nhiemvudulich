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
using System.Xml.Linq;

namespace Application.DuongDayNong
{



    public class DuongDayNongGets
    {
        public class Query : IRequest<Result<List<DuongDayNongItemResponse>>>
        {// su li tham so dau vao

            public int pagesize { get; set; }
            public int pageindex { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<List<DuongDayNongItemResponse>>>
        {
            private readonly IConfiguration _configuration;
            public Handler(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            public async Task<Result<List<DuongDayNongItemResponse>>> Handle(Query request, CancellationToken cancellationToken)
            {   DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@@PPAGEINDEX", request.pagesize);
                parameters.Add("@PPAGESIZE",request.pageindex);
                string spName = "SP_DSDuongDayNongGets";
                using (var connection = new SqlConnection(_configuration.GetConnectionString("HuecitConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<DuongDayNongItemResponse>(new CommandDefinition(spName, parameters: null, commandType: System.Data.CommandType.StoredProcedure));
                    return Result<List<DuongDayNongItemResponse>>.Success(result.ToList());

                }

            }
        }
    }
}


