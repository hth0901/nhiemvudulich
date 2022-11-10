using Application.Core;
using Dapper;
using Domain;
using Domain.HueCit;
using Domain.TechLife;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.BanDo
{
    public class UserAdd
    {
        public class Command : IRequest<Result<SYS_User>>
        {
            public SYS_User Request { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<SYS_User>>
        {
            private readonly IConfiguration _configuration;
            public Handler(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            public async Task<Result<SYS_User>> Handle(Command request, CancellationToken cancellationToken)
            {   
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@PHOTEN", request.Request.HoTen);
                dynamicParameters.Add("@PDIACHI", request.Request.DiaChi);
                dynamicParameters.Add("@PDIENTHOAI", request.Request.DienThoai);
                dynamicParameters.Add("@PHOPTHU", request.Request.HopThu);
                dynamicParameters.Add("@PTENDANGNHAP", request.Request.TenDangNhap);
                dynamicParameters.Add("@PMATKHAU", request.Request.MatKhau);
                dynamicParameters.Add("@PQUYEN", request.Request.Quyen);
                dynamicParameters.Add("@PTRANGTHAI", request.Request.TrangThai.ToString());

                string spName = "SP_UserAdd";

                using (var connection = new SqlConnection(_configuration.GetConnectionString("HuecitConnection")))
                {   
                    connection.Open();
                    var result = await connection.QueryFirstOrDefaultAsync<SYS_User>(new CommandDefinition(spName, parameters: dynamicParameters, commandType: System.Data.CommandType.StoredProcedure));

                    return Result<SYS_User>.Success(result);
                }

            }
        }
    }
}
