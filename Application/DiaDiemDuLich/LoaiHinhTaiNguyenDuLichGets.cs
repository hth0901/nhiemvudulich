using Application.Core;
using Dapper;
using Domain;
using Domain.HueCit;
using Domain.ResponseEntity;
using MediatR;
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

namespace Application.DiaDiemDuLich
{
    public  class LoaiHinhTaiNguyenDuLichGets
    {
        public class Query: IRequest<Result<List<LoaiHinh_ID_Ten>>>
        {

        }
        public class Handler: IRequestHandler<Query, Result<List<LoaiHinh_ID_Ten>>>
        { private readonly DataContext _context;
            private readonly IConfiguration _configuration; 
            public  Handler (IConfiguration configuration,DataContext context)// constructor
            {
                _configuration = configuration;
                _context = context;
            }

            public async Task<Result<List<LoaiHinh_ID_Ten>>> Handle(Query request, CancellationToken cancellationToken)
            {
           
                string spName = "SP_DSLoaiHinhTaiNguyen";
                using (var connection = new SqlConnection(_configuration.GetConnectionString("Huecitconnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<LoaiHinh_ID_Ten>(new CommandDefinition(spName, parameters:null, commandType: System.Data.CommandType.StoredProcedure));
                    return Result<List<LoaiHinh_ID_Ten>>.Success(result.ToList());

                }

            }
        }

    }
}
