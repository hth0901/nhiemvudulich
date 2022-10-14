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
        public class Query: IRequest<Result<List<LoaiHinhTaiNguyenItemResponse>>>
        {
            public int pagesize { get; set; }
            public int pageindex { get; set; }
        }
        public class Handler: IRequestHandler<Query, Result<List<LoaiHinhTaiNguyenItemResponse>>>
        { private readonly DataContext _context;
            private readonly IConfiguration _configuration; 
            public  Handler (IConfiguration configuration,DataContext context)// constructor
            {
                _configuration = configuration;
                _context = context;
            }

            public async Task<Result<List<LoaiHinhTaiNguyenItemResponse>>> Handle(Query request, CancellationToken cancellationToken)
            {


                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@PPAGEINDEX", request.pageindex);
                parameters.Add("@PPAGESIZE", request.pagesize);
                string spName = "SP_DSLoaiHinhTaiNguyen";
                using (var connection = new SqlConnection(_configuration.GetConnectionString("HuecitConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<LoaiHinhTaiNguyenItemResponse>(new CommandDefinition(spName, parameters, commandType: System.Data.CommandType.StoredProcedure));
                    return Result<List<LoaiHinhTaiNguyenItemResponse>>.Success(result.ToList());

                }

            }
        }

    }
}
