using System;
using System.Text;
using Chords.WebApi.GraphQl._Actions;
using Chords.WebApi.GraphQl.Auth;
using Chords.WebApi.GraphQl.Genres;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;


namespace Chords.WebApi.Configurations
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAuthentication(this IServiceCollection services, AppSettings appSettings)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = appSettings.Jwt.Issuer,
                        ValidAudience = appSettings.Jwt.Audience,
                        //ClockSkew = TimeSpan.Zero,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.Jwt.Key))
                    };
                });
            
            services.AddAuthorization(options =>
            {
                // require all users to be authenticated by default
                options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build();
                
                // Register our age limit policy
                // options.AddPolicy("Over18YearsOld", policy => policy.RequireAssertion(context =>
                //     context.User.HasClaim(c =>
                //         (c.Type == "DateOfBirth" && DateTime.Now.Year - DateTime.Parse(c.Value).Year >= 18)
                //     )));
            });
            
            // Register our custom Authorization handler
            //services.AddSingleton<IAuthorizationHandler, PermissionHandler>();
            
            // Overrides the DefaultAuthorizationPolicyProvider with our own
            // https://github.com/dotnet/aspnetcore/blob/main/src/Security/Authorization/Core/src/DefaultAuthorizationPolicyProvider.cs
            //services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();
            
            return services;
        }
        
        public static IServiceCollection AddGraphQL(this IServiceCollection services, AppSettings appSettings)
        {
            services
                .AddGraphQLServer()
                // .AddFairyBread()
                .AddAuthorization()
                .RegisterService<AuthService>()
                .RegisterService<GenreService>()
                .AddQueryType<QueryType>()
                .AddMutationType<MutationType>()
                .AddFiltering()
                .AddSorting()
                .AddProjections()
                ;

            return services;
        }
        
        public static T ConfigureAndGet<T>(
            this IConfiguration configuration, IServiceCollection services, string sectionName) where T: class
        {
            if (string.IsNullOrWhiteSpace(sectionName))
                throw new ArgumentException("Section name cannot be empty", nameof(sectionName));

            var section = configuration.GetSection(sectionName);
            services.Configure<T>(section);

            return section.Get<T>();
        }
    }

}