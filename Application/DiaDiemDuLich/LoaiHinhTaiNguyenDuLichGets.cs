using Application.Core;
using Dapper;
using Domain;
using Domain.HueCit;
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
        public class Query: IRequest<Result<List<DL_TaiNguyen_LoaiDuLieu>>>
        {

        }
        public class Handler: IRequestHandler<Query, Result<List<DL_TaiNguyen_LoaiDuLieu>>>
        { private readonly DataContext _context;
            private readonly IConfiguration _configuration; 
            public  Handler (IConfiguration configuration,DataContext context)// constructor
            {
                _configuration = configuration;
                _context = context;
            }

            public async Task<Result<List<DL_TaiNguyen_LoaiDuLieu>>> Handle(Query request, CancellationToken cancellationToken)
            { 
                
                            

                using (var connection = new SqlConnection(_configuration.GetConnectionString("HuecitConnection")))
                {
                    connection.Open();
                    var result = from st in _context.DL_TaiNguyen_LoaiDuLieu
                                select st;
                    return Result<List<DL_TaiNguyen_LoaiDuLieu>>.Success(result.ToList());
                }
                 
            }
        }

    }
}
