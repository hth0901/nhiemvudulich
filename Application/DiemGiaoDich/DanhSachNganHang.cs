﻿using Application.Core;
using Dapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Persistence;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DiemGiaoDich
{
    public class DanhSachNganHang
    {
        public class Query : IRequest<Result<List<DL_DiemGiaoDich>>>
        {
        }
        public class Handler : IRequestHandler<Query, Result<List<DL_DiemGiaoDich>>>
        {
            private readonly IConfiguration _configuration;
            private readonly DataContext _context;

            public Handler(DataContext context, IConfiguration configuration)
            {
                _context = context;
                _configuration = configuration;

            }

            public Task<Result<List<DL_DiemGiaoDich>>> Handle(Query request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }

    }
}