using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using School.API.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace School.API
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
            //Adicionando o contexto - database
            services.AddDbContext<DataContext>(
                context => context.UseSqlite(Configuration.GetConnectionString("Default"))
            );

            //Procura dentro dos assemblies quais arquivos herdam classe Profile - do Automapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //Adicionando repository do tipo Scoped usa a mesma instancia para todas solicitações em uma mesma requisição
            services.AddScoped<IRepository, Repository>();

            services.AddSwaggerGen(options => {
                options.SwaggerDoc("schoolapi",
                    new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                         Title = "School API",
                         Version = "1.0",
                         TermsOfService = new Uri("http://TermoDeUso.com"),
                         Description = "WebAPI escola",
                         License = new Microsoft.OpenApi.Models.OpenApiLicense
                         {
                             Name = "School License",
                             Url = new Uri("http://TermoDeUso.com")
                         }
                    });
                var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);
                options.IncludeXmlComments(xmlCommentsFullPath);
            });

            //Loop de jsons com newtonsoft
            services.AddControllers().AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseSwagger().UseSwaggerUI(opt => {
                opt.SwaggerEndpoint("swagger/schoolapi/swagger.json", "schoolapi");
                opt.RoutePrefix = "";
            });

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
