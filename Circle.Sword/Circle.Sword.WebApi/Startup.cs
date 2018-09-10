// ***********************************************************************
// Solution         : Damon.Core
// Project          : WebApi
// File             : Startup.cs
// ***********************************************************************
// <copyright>
//     Copyright © 2016 - 2018 Kolibre Credit Team. All rights reserved.
// </copyright>
// ***********************************************************************

using System.Data.SqlClient;
using Circle.Sword.Domain.Identity;
using Circle.Sword.Domain.Identity.Interface;
using Damon.Domain.Foundation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApi;

namespace Circle.Sword.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // 注册认证服务
            //services.AddAuthentication("Bearer")
            //    .AddIdentityServerAuthentication(options =>
            //    {
            //        options.Authority = "http://localhost:5000";
            //        options.RequireHttpsMetadata = false;
            //        options.ApiName = "Circle.Sword";
            //    });

            //services.AddAuthorization(options => options.AddPolicy("Check", policy => policy.RequireClaim("readonly")));

            services.AddMvc();

            services.Configure<AzureStorageProviderOptions>(Configuration.GetSection("AzureStorageProvider"));
            services.AddSingleton<AzureStorageProvider>();

            services.Configure<DataOptions>(Configuration.GetSection("DataOptions"));
            services.AddSingleton<IUserRepository, UserRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseAuthentication(); // 添加认证中间件
            app.UseStaticFiles();

            app.UseMvc();
        }
    }
}