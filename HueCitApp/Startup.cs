using Application.CoSoChamSocSucKhoeSacDep;
using Application.CoSoKhamChuaBenh;
using Application.CoSoLuuTru;
using Application.DiaDiemAnUong;
using Application.DiaDiemDuLich;
using Application.DiaDiemMuaSamGiaiTri;
using Application.DiemGiaoDich;
using Application.DiemVeSinh;
using Application.HeThongSoHoaTinhCoSoLuuTruDuLichCapNhat;
using Application.HeThongSoHoaTinhDoanhNghiepDaiLiLuHanhCapNhat;
using Application.HeThongSoHoaTinhDuLieuAnUongCapNhat;
using Application.HeThongSoHoaTinhDuLieuMuaSamCapNhat;
using Application.HeThongSoHoaTinhDuLieuTheThaoCapNhat;
using Application.HeThongSoHoaTinhDuLieuVuiChoiGiaiTriCapNhat;
using Application.HeThongSoHoaTinhHuongDanVienCapNhat;

using FluentValidation.AspNetCore;
using HueCitApp.Extensions;
using HueCitApp.MiddleWare;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace HueCitApp
{
    public class Startup
    {
        //public Startup(IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //}

        //public IConfiguration Configuration { get; }
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
           
            services.AddControllersWithViews();

            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TravelApi", Version = "v1" });
            });
            services.AddControllersWithViews().AddFluentValidation(config =>
            {
                config.RegisterValidatorsFromAssemblyContaining<HeThongSoHoaDuLieuAnUongEdit>();
                config.RegisterValidatorsFromAssemblyContaining<HeThongSoHoaDuLieuAnUongAdd>();

                config.RegisterValidatorsFromAssemblyContaining<HeThongSoHoaDoanhNghiepDaiLiLuHanhAdd>();
                config.RegisterValidatorsFromAssemblyContaining<HeThongSoHoaDoanhNghiepDaiLiLuHanhEdit>();

                config.RegisterValidatorsFromAssemblyContaining<HeThongSoHoaCoSoLuuTruAdd>();
                config.RegisterValidatorsFromAssemblyContaining<HeThongSoHoaCoSoLuuTruEdit>();

                config.RegisterValidatorsFromAssemblyContaining<HeThongSoHoaDuLieuMuaSamAdd>();
                config.RegisterValidatorsFromAssemblyContaining<HeThongSoHoaDuLieuMuaSamEdit>();

                config.RegisterValidatorsFromAssemblyContaining<HeThongSoHoaDuLieuTheThaoAdd>();
                config.RegisterValidatorsFromAssemblyContaining<HeThongSoHoaDuLieuTheThaoEdit>();

                config.RegisterValidatorsFromAssemblyContaining<HeThongSoHoaHuongDanVienAdd>();
                config.RegisterValidatorsFromAssemblyContaining<HeThongSoHoaHuongDanVienEdit>();



                config.RegisterValidatorsFromAssemblyContaining<HeThongSoHoaDuLieuVuiChoiGiaiTriAdd>();
                config.RegisterValidatorsFromAssemblyContaining<HeThongSoHoaDuLieuVuiChoiGiaiTriEdit>();



                config.RegisterValidatorsFromAssemblyContaining<CoSoChamSocSucKhoeSacDepGanViTriDuKhachGets>();

                config.RegisterValidatorsFromAssemblyContaining<CoSoKhamChuaBenhGanViTriDuKhachGets>();
             

                config.RegisterValidatorsFromAssemblyContaining<DiemAnUongGanViTriDuKhachGets>();

                config.RegisterValidatorsFromAssemblyContaining<DiaDiemDuLichGanViTriDuKhachGets>();
                config.RegisterValidatorsFromAssemblyContaining<DiaDiemMuaSamGiaiTriGanViTriDuKhachGets>();
                config.RegisterValidatorsFromAssemblyContaining<DiemGiaoDichGanViTriDuKhach>();
                config.RegisterValidatorsFromAssemblyContaining<DiemVeSinhGanViTriDuKhachGets>();








            });
            services.AddApplicationServices(_config);
            services.AddIdentityServices(_config);

            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "TravelApi");
            });

            loggerFactory.AddFile("Logs/mylog-{Date}.txt");
            app.UseMiddleware<ExceptionMiddleWare>();


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            
            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                    
            });
        }
    }
}
