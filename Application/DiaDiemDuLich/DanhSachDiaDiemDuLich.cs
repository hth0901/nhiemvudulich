using Application.Core;
using Dapper;
using Domain;
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
    public  class DanhSachDiaDiemDuLich
    {
        public class Query : IRequest<Result<List<LoaiHinh>>>
        {

        }
        public class Handler : IRequestHandler<Query, Result<List<LoaiHinh>>>
        {
            private readonly IConfiguration _configuration;
            private readonly DataContext _context;

            public Handler(IConfiguration configuration, DataContext context)
            {
                _configuration = configuration;
                _context = context;
            }
            public async Task<Result<List<LoaiHinh>>>  Handle(Query request, CancellationToken cancellationToken)
            {
                string spName = "PS_DiemDuLichGets";
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {      
                    connection.Open();
                    //var result = await connection.QueryAsync<Place>(spName);
                    var result = await connection.QueryAsync<LoaiHinh>(new CommandDefinition(spName, parameters: null, commandType: System.Data.CommandType.StoredProcedure));
                    return Result<List<LoaiHinh>>.Success(result.ToList());
                }



                // var result = await _context.VeDiTich_DiaDiem.ToListAsync<VeDiTich_DiaDiem>();
                // return Result<List<VeDiTich_DiaDiem>>.Success(result);
            }

            
        }
    }
  
}
