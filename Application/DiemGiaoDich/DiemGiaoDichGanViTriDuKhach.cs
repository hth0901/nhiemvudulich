using Application.Core;
using Dapper;
using Domain.RequestEntity;
using Domain.ResponseEntity;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DiemGiaoDich
{
    public class DiemGiaoDichGanViTriDuKhach
    {
        public class Query : IRequest<Result<List<DiemGiaoDichItemResponse>>>
        {   public Distance_Request infor { get; set; }
       
        }
        public class CommandValidator : AbstractValidator<Distance_Request>
        {
            public CommandValidator()
            {
                RuleFor(x => x.x).NotEmpty().NotNull();
                RuleFor(x => x.y).NotEmpty().NotNull();




            }
        }
        public class Handler : IRequestHandler<Query, Result<List<DiemGiaoDichItemResponse>>>
        {
            private readonly IConfiguration _configuration;

            public Handler(IConfiguration configuration)
            {
                _configuration = configuration;

            }
            public async Task<Result<List<DiemGiaoDichItemResponse>>> Handle(Query request, CancellationToken cancellationToken)
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@PPAGEINDEX", request.infor.pageindex);
                parameters.Add("@PPAGESIZE", request.infor.pagesize);
                parameters.Add("@X", request.infor.x);
                parameters.Add("@Y", request.infor.y);
                parameters.Add("@Distance", request.infor.distance);
                string spName = "SP_DSDistanceToDiemGiaoDich";
                using (var connection = new SqlConnection(_configuration.GetConnectionString("HuecitConnection")))
                {
                    connection.Open();
                    //var result = await connection.QueryAsync<Place>(spName);
                    var result = await connection.QueryAsync<DiemGiaoDichItemResponse>(new CommandDefinition(spName, parameters, commandType: System.Data.CommandType.StoredProcedure));
                    return Result<List<DiemGiaoDichItemResponse>>.Success(result.ToList());
                }
            }
        }
    }
}
