using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RahulTest.Core.Abstractions.Factories;
using RahulTest.Core.Abstractions.FileParsers;
using RahulTest.Core.Abstractions.Services;
using RahulTest.Core.Abstractions.UnitOfWork;
using RahulTest.Core.Abstractions.Validators;
using RahulTest.Domain.EF.Core.Abstractions;
using RahulTest.Domain.EF.Repositories.Data;
using RahulTest.Domain.EF.Repositories.Repositories;
using RahulTest.Infrastructure.Factories;
using RahulTest.Infrastructure.FileParsers;
using RahulTest.Infrastructure.Services;
using RahulTest.Infrastructure.UnitOfWork;
using RahulTest.Infrastructure.Validators;

namespace RahulTest.Web
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("ApplicationContext")));

            services.AddControllersWithViews();

            services.AddSwaggerGen();

            services.AddSingleton<IUploadedFIleInfoValidator, UploadedFIleInfoValidator>();

            services.AddTransient<IParseManager, ParseManager>();
            services.AddTransient<IParserFactory, ParserFactory>();
            // services.AddTransient<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<ITransactionsService, TransactionsService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationDbContext dataContext)
        {
            dataContext.Database.EnsureCreated();
            dataContext.Database.Migrate();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSwagger();
            
                app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseRouting();

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
