﻿using Application.Core;
using Dapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.NganHangDiemGiaoDich
{
    public class DanhSachDiemGiaoDich
    {
        public class Query: IRequest<Result<List<DL_DiemGiaoDich>>>
        {


        }
        public class Handler : IRequestHandler<Query, Result<List<DL_DiemGiaoDich>>>
        {
            private readonly IConfiguration _configuration;
            private readonly DbContext _dbContext;
            public Handler(IConfiguration configuration)
            {
                _configuration = configuration;
        
            }
            public async Task<Result<List<DL_DiemGiaoDich>>> Handle(Query request, CancellationToken cancellationToken)
            {
                string spName = "SP_DiemGiaoDichGets";
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();
                    //var result = await connection.QueryAsync<Place>(spName);
                    var result = await connection.QueryAsync<DL_DiemGiaoDich>(new CommandDefinition(spName, commandType: System.Data.CommandType.StoredProcedure));
                    return Result<List<DL_DiemGiaoDich>>.Success(result.ToList());
                }
            }
        }
       
    }
}
