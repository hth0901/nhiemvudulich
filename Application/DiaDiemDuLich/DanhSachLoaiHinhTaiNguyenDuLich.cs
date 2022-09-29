using Application.Core;
using Dapper;
using Domain;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Application.DiaDiemDuLich
{
    public  class DanhSachLoaiHinhTaiNguyenDuLich
    {
        public class Query: IRequest<Result<List<DL_TaiNguyen_LoaiDuLieu>>>
        {

        }
        public class Handler: IRequestHandler<Query, Result<List<DL_TaiNguyen_LoaiDuLieu>>>
        {   
            private readonly IConfiguration _configuration; 
            public  Handler (IConfiguration configuration)// constructor
            {
                _configuration = configuration; 
            }

            public async Task<Result<List<DL_TaiNguyen_LoaiDuLieu>>> Handle(Query request, CancellationToken cancellationToken)
            {
                string query = "select * from [dbo].[DL_TaiNguyen_LoaiDuLieu]";
                            

                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<DL_TaiNguyen_LoaiDuLieu>(query);
                  
                    return Result<List<DL_TaiNguyen_LoaiDuLieu>>.Success(result.ToList());
                }
                 
            }
        }

    }
}
