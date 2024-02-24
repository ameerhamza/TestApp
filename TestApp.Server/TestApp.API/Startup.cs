using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;
using AutoMapper;
using TestApp.Repo.Repositories;
using TestApp.Services.Contracts;
using TestApp.Repo.DataStores;
using TestApp.Repo.Model;
using TestApp.Services.Contracts.Business;
using TestApp.Services.Contracts.Model;
using TestApp.Services.Impl;
using TestApp.Services.Impl.Business;

namespace TestApp.API
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
           
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TestApp.API", Version = "v1" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            services.AddCors(options =>
            {
                options.AddPolicy("ClientSide",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });


            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("Data/logs/log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            services.AddAutoMapper(typeof(RepoMapperProfile));

            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();


            RegisterApplicationContracts(services);

            services.AddControllers();
        }

        private static void RegisterApplicationContracts(IServiceCollection services)
        {
            services.AddSingleton<ICheckoutService, CheckoutService>();
            services.AddSingleton<ICartService, CartService>();
            services.AddSingleton<IItemService, ItemService>();

            services.AddSingleton<IDataStore<CartRule>>(provider =>
            {
                var env = provider.GetRequiredService<IWebHostEnvironment>();
                var basePath = env.ContentRootPath + "\\Data"; 
                var fileName = "rule2.json"; 

                return new JsonDataStore<CartRule>(basePath, fileName);
            });

            services.AddSingleton<IDataStore<Item>>(provider =>
            {
                var env = provider.GetRequiredService<IWebHostEnvironment>();
                var basePath = env.ContentRootPath + "\\Data";
                var fileName = "items.json";

                return new JsonDataStore<Item>(basePath, fileName);
            });
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
               
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API Name v1"));
            }

           
            app.UseSerilogRequestLogging();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors("ClientSide");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
