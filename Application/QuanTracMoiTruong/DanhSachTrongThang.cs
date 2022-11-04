using Application.Core;
using Dapper;
using Domain.RequestEntity;
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

namespace Application.QuanTracMoiTruong
{
    public class DanhSachTrongThang
    {
        public class Query : IRequest<Result<List<QuanTracMoiTruongThongKe>>>
        {
            public int pageSize { get; set; }
            public int pageIndex { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<List<QuanTracMoiTruongThongKe>>>
        {
            private readonly IConfiguration _configuration;
            public Handler(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            public async Task<Result<List<QuanTracMoiTruongThongKe>>> Handle(Query request, CancellationToken cancellationToken)
            {
                string spName = "SP_QUANTRACMOITRUONG_THONGKE_TRONGTHANG";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@PPAGEINDEX", request.pageIndex);
                parameters.Add("@PPAGESIZE", request.pageSize);
                using (var connection = new SqlConnection(_configuration.GetConnectionString("HuecitConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<QuanTracMoiTruongThongKe>(new CommandDefinition(spName, parameters, commandType: System.Data.CommandType.StoredProcedure));
                    return Result<List<QuanTracMoiTruongThongKe>>.Success(result.ToList());

                }
            }
        }
    }
}
