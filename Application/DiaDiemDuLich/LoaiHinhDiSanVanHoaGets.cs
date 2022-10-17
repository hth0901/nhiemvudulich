using Application.Core;
using Dapper;
using Domain.ResponseEntity;
using MediatR;
using Microsoft.Extensions.Configuration;
using Persistence;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DiaDiemDuLich
{
    public class LoaiHinhDiSanVanHoaGets
    {
        public class Query : IRequest<Result<List<LoaiHinh_ID_Ten>>>
        {
        
        }
        public class Handler : IRequestHandler<Query, Result<List<LoaiHinh_ID_Ten>>>
        {
            private readonly DataContext _context;
            private readonly IConfiguration _configuration;
            public Handler(IConfiguration configuration, DataContext context)// constructor
            {
                _configuration = configuration;
                _context = context;
            }

            public async Task<Result<List<LoaiHinh_ID_Ten>>> Handle(Query request, CancellationToken cancellationToken)
            {

 
                string spName = "SP_DSLoaiHinhDiSanVanHoa";
                using (var connection = new SqlConnection(_configuration.GetConnectionString("HuecitConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<LoaiHinh_ID_Ten>(new CommandDefinition(spName, parameters:null, commandType: System.Data.CommandType.StoredProcedure));
                    return Result<List<LoaiHinh_ID_Ten>>.Success(result.ToList());

                }

            }
        }
    }
}
