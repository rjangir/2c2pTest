using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoSample.Core.Abstractions.Factories;
using DemoSample.Core.Abstractions.FileParsers;
using DemoSample.Core.Abstractions.Services;
using DemoSample.Core.Abstractions.UnitOfWork;
using DemoSample.Core.Abstractions.Validators;
using DemoSample.Domain.EF.Repositories.Data;
using DemoSample.Infrastructure.Factories;
using DemoSample.Infrastructure.Services;
using DemoSample.Infrastructure.UnitOfWork;
using DemoSample.Infrastructure.Validators;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;



namespace DemoSample.Web
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
