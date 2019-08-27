using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AttendanceApi.Domain;
using AttendanceApi.Reps;

namespace AttendanceApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
 
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

            services.Configure<IISServerOptions>(options =>
            {
                options.AutomaticAuthentication = false;

            });
            services.Configure<IISOptions>(options =>
            {
                options.ForwardClientCertificate = false;
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddEntityFrameworkSqlite().AddDbContext<AtdDbContext>();

            // Reps
            services.AddScoped<IPersonnelRepository, PersonnelRepository>();
            services.AddScoped<ILoginRepository, LoginRepository>();
            services.AddScoped<IAttendanceTimeRepository, AttendanceTimeRepository>();
            services.AddScoped<IMonthRepository, MonthRepository>();
            services.AddScoped<IDayRepository, DayRepository>();
            services.AddScoped<IAttendanceSpanRepository, AttendanceSpanRepository>();
            services.AddScoped<IDeveloperRepository, DeveloperRepository>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, AtdDbContext context)
        {
            app.UseCors("CorsPolicy");

            using (var client = new AtdDbContext())
            {
                client.Database.EnsureCreated();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                DataLayer.DbInitializer.Seed(context);
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
