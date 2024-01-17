using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;
using TestApp.API.Model;
using TestApp.API.Model.Validators;
using AutoMapper;
using TestApp.Repo.Repositories;
using TestApp.Services.Contracts;
using TestApp.Repo.DataStores;
using TestApp.Services.Impl;

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

            services.AddAutoMapper(typeof(ApiMappingProfile));
            services.AddTransient<IValidator<Person>, PersonValidator>();

            services.AddValidatorsFromAssemblyContaining<PersonValidator>();
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();

          

            RegisterApplicationContracts(services);

            services.AddControllers();
        }

        private static void RegisterApplicationContracts(IServiceCollection services)
        {
            services.AddSingleton<IPerson, Services.Impl.Person>();
            services.AddSingleton<IPersonRepository, PersonRepository>();
            services.AddSingleton<IPersonService, PersonService>();

            services.AddSingleton<IDataStore<Services.Impl.Person>>(provider =>
            {
                var env = provider.GetRequiredService<IWebHostEnvironment>();
                var basePath = env.ContentRootPath + "\\Data"; 
                var fileName = "persons.json"; 

                return new JsonDataStore<Services.Impl.Person>(basePath, fileName);
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
