using Application.Core;
using Dapper;
using Domain;
using Domain.HueCit;
using Domain.ResponseEntity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DiemGiaoDich
{
    public class DiemGiaoDichGets
    {
        public class Query: IRequest<Result<List<DiemGiaoDichItemResponse>>>
        {
            public int pagesize { get; set; }
            public int pageindex { get; set; }
        }
        public class Handler : IRequestHandler<Query, Result<List<DiemGiaoDichItemResponse>>>
        {
            private readonly IConfiguration _configuration;
        
            public Handler(IConfiguration configuration)
            {
                _configuration = configuration;
        
            }
            public async Task<Result<List<DiemGiaoDichItemResponse>>> Handle(Query request, CancellationToken cancellationToken)
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@PPAGEINDEX", request.pageindex);
                parameters.Add("@PPAGESIZE", request.pagesize);
                string spName = "SP_DSDiemGiaoDichGets";
                using (var connection = new SqlConnection(_configuration.GetConnectionString("HuecitConnection")))
                {
                    connection.Open();
                    //var result = await connection.QueryAsync<Place>(spName);
                    var result = await connection.QueryAsync<DiemGiaoDichItemResponse>(new CommandDefinition(spName, parameters, commandType: System.Data.CommandType.StoredProcedure));
                    return Result<List<DiemGiaoDichItemResponse>>.Success(result.ToList());
                }
            }
        }
       
    }
}
