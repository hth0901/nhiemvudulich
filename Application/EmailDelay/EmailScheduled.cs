
using Application.Core;
using Dapper;
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

namespace Application.EmailDelay
{
    public class EmailScheduled
    {

    
        private readonly IConfiguration _configuration;

        public EmailScheduled(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<Result<List<string>>> Handle()
            {
                string spName = "SP_EMAIL_QuanTracMoiTruong";
                using (var connection = new SqlConnection(_configuration.GetConnectionString("HuecitConnection")))
                {
                Console.WriteLine("aloalao");
                    connection.Open();
                    var result = await connection.QueryAsync<string>(new CommandDefinition(spName, null, commandType: System.Data.CommandType.StoredProcedure));
                var check = result;     
                    return Result<List<string>>.Success(result.ToList());// compare of list  


                    
                }

            }
        }

    }

