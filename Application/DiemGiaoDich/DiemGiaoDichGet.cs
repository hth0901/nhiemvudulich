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
    public class DiemGiaoDichGet
    {
        public class Query: IRequest<Result<DiemGiaoDichTrinhDien>>
        {
            public int ID { get; set; }
        }
        public class Handler : IRequestHandler<Query, Result<DiemGiaoDichTrinhDien>>
        {
            private readonly IConfiguration _configuration;
        
            public Handler(IConfiguration configuration)
            {
                _configuration = configuration;
            }
            public async Task<Result<DiemGiaoDichTrinhDien>> Handle(Query request, CancellationToken cancellationToken)
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@ID", request.ID);

                string spName = "SP_DiemGiaoDichGets";

                using (var connection = new SqlConnection(_configuration.GetConnectionString("HuecitConnection")))
                {
                    connection.Open();
                    //var result = await connection.QueryAsync<Place>(spName);
                    var result = await connection.QueryFirstOrDefaultAsync<DiemGiaoDichTrinhDien>(new CommandDefinition(spName, parameters, commandType: System.Data.CommandType.StoredProcedure));
                    return Result<DiemGiaoDichTrinhDien>.Success(result);
                }
            }
        }
       
    }
}
