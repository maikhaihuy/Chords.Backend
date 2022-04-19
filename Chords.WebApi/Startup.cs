using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Chords.CoreLib.HelperService.Auth;
using Chords.DataAccess.EntityFramework;
using Chords.WebApi.Configurations;
using Chords.WebApi.GraphQl.Auth;
using Chords.WebApi.GraphQl.Genres;
using GraphQL.Server.Ui.Voyager;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Chords.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public static AppSettings AppSettings = null!;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            AppSettings = Configuration.ConfigureAndGet<AppSettings>(services, AppSettings.SectionName);
            
            services.AddControllers();
            
            // Database
            services.AddPooledDbContextFactory<ChordsDbContext>(option =>
            {
                string connection = Configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
                option.UseNpgsql(connection, x => x.EnableRetryOnFailure());
            });
            
            // Auto mapping
            services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new GraphQlMapping());
            }).CreateMapper());
            
            // DI services
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IJwtManagerConfiguration, JwtManagerConfiguration>();
            services.AddTransient<IJwtManagerService, JwtManagerService>();
            services.AddTransient<AuthService>();
            services.AddTransient<GenreService>();
            
            services.AddAuthentication(AppSettings);
            
            // GraphQL config
            services.AddGraphQL(AppSettings);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Chords.WebApi", Version = "v1"});
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Chords.WebApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGraphQL();
            });
            
            app.UseGraphQLVoyager(new VoyagerOptions()
            {
                GraphQLEndPoint = "/graphql"
            }, "/graphql-voyager");
        }
    }
}