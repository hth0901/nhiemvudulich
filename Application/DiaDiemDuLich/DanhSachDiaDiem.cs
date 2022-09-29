using Application.Core;
using Dapper;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Application.DiaDiemDuLich
{
    public  class DanhSachDiaDiem
    {
        public class Query : IRequest<Result<List<VeDiTich_DiaDiem>>>
        {

        }
        public class Handler : IRequestHandler<Query, Result<List<VeDiTich_DiaDiem>>>
        {
            private readonly IConfiguration _configuration;
            public Handler(IConfiguration configuration)
            {
                _configuration = configuration;
            }
            public async Task<Result<List<VeDiTich_DiaDiem>>>  Handle(Query request, CancellationToken cancellationToken)
            {
                string spName = "SP_VeDiTichDiaDiemGets";
                using(var connection=new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();
                    //var result = await connection.QueryAsync<Place>(spName);
                    var result = await connection.QueryAsync<VeDiTich_DiaDiem>(new CommandDefinition(spName, commandType: System.Data.CommandType.StoredProcedure));
                    return Result<List<VeDiTich_DiaDiem>>.Success(result.ToList());
                }

            }

            
        }
    }
  
}
