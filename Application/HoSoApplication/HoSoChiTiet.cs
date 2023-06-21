using Application.Core;
using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.HoSoApplication
{
    public class HoSoChiTiet
    {
        public class Query : IRequest<Result<Domain.TechLife.HoSoChiTiet>>
        {
            public int Id { get; set; }
        }
        public class Handler : IRequestHandler<Query, Result<Domain.TechLife.HoSoChiTiet>>
        {
            private readonly IConfiguration _configuration;
            public Handler(IConfiguration configuration)
            {
                _configuration = configuration;
            }
            public async Task<Result<Domain.TechLife.HoSoChiTiet>> Handle(Query request, CancellationToken cancellationToken)
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@PID", request.Id);
                string spName = "SPU_HoSoChiTiet";
                using (var connection = new SqlConnection(_configuration.GetConnectionString("TechLifeConnection")))
                {
                    try
                    {
                        await connection.OpenAsync();
                        var result = await connection.QueryFirstOrDefaultAsync<Domain.TechLife.HoSoChiTiet>(new CommandDefinition(spName, parameters, commandType: System.Data.CommandType.StoredProcedure));
                        return Result<Domain.TechLife.HoSoChiTiet>.Success(result);

                    }
                    catch (Exception ex)
                    {
                        return Result<Domain.TechLife.HoSoChiTiet>.Failure(ex.Message);
                    }
                    finally
                    {
                        await connection.CloseAsync();
                    }
                }
            }
        }
    }
}
