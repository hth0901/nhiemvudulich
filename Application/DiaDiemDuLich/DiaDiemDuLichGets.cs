using Application.Core;
using Dapper;
using Domain;
using Domain.ResponseEntity;
using Domain.TechLife;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Application.DiaDiemDuLich
{
    public  class DiaDiemDuLichGets
    {
        public class Query : IRequest<Result<List<HoSoLuTruItemResponse>>>
        {
            public int pagesize { get; set; }
            public int pageindex { get; set; }

        }
        public class Handler : IRequestHandler<Query, Result<List<HoSoLuTruItemResponse>>>
        {
            private readonly IConfiguration _configuration;
            private readonly DataContext _context;

            public Handler(IConfiguration configuration, DataContext context)
            {
                _configuration = configuration;
                _context = context;
            }
            public async Task<Result<List<HoSoLuTruItemResponse>>>  Handle(Query request, CancellationToken cancellationToken)
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@PPAGEINDEX", request.pageindex);
                parameters.Add("@PPAGESIZE", request.pagesize);
                string spName = "SP_DiemDuLichGets";
                using (var connection = new SqlConnection(_configuration.GetConnectionString("HuecitConnection")))
                {      
                    connection.Open();
                    //var result = await connection.QueryAsync<Place>(spName);
                    var result = await connection.QueryAsync<HoSoLuTruItemResponse>(new CommandDefinition(spName, parameters, commandType: System.Data.CommandType.StoredProcedure));
                    return Result<List<HoSoLuTruItemResponse>>.Success(result.ToList());
                }



                // var result = await _context.VeDiTich_DiaDiem.ToListAsync<VeDiTich_DiaDiem>();
                // return Result<List<VeDiTich_DiaDiem>>.Success(result);
            }

            
        }
    }
  
}
