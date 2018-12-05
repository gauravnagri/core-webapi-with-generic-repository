using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StarterAPI.Jobs;
using StarterAPI.Models;
using StarterAPI.Repositories;
using StarterAPI.Utils;

namespace StarterAPI
{
    public class Startup
    {
        private readonly ILogger<Startup> _logger;
        public Startup(ILogger<Startup> logger)
        {
            _logger = logger;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<RandomStringProvider>();
            services.AddSingleton<IHostedService, DataRefreshService>();
            services.AddSingleton<HttpClient>();
            services.AddMvc();
            services.AddDbContext<TodoContext>((context) => context.UseInMemoryDatabase("TestDB"));
            services.AddScoped<IGenericRepository<Todo>, GenericRepository<Todo>>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Use(async(context, next) =>
            {
                _logger.LogInformation($"Request url : {context.Request.Path}");
                await next();
            });

            app.UseMvc();

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });            
        }
    }
}
