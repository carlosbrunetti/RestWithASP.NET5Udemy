using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using RestWithASP.NET5Udemy.Business;
using RestWithASP.NET5Udemy.Business.Implementations;
using RestWithASP.NET5Udemy.Configurations;
using RestWithASP.NET5Udemy.Hypermedia.Enricher;
using RestWithASP.NET5Udemy.Hypermedia.Filters;
using RestWithASP.NET5Udemy.Model.Context;
using RestWithASP.NET5Udemy.Repository;
using RestWithASP.NET5Udemy.Repository.Generic;
using RestWithASP.NET5Udemy.Services;
using RestWithASP.NET5Udemy.Services.Implementations;
using Serilog;

namespace RestWithASP.NET5Udemy
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //security
            var tokenConfigurations = new TokenConfiguration();
            new ConfigureFromConfigurationOptions<TokenConfiguration>
                (
                    Configuration.GetSection("TokenConfigurantions")
                ).Configure(tokenConfigurations);
            services.AddSingleton(tokenConfigurations);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = tokenConfigurations.Issuer,
                    ValidAudience = tokenConfigurations.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfigurations.Secret))
                };
            });

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });

            //CORS
            services.AddCors(options => options.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            }));
            services.AddControllers();

            var connection = Configuration["MySQLConnection:MySQLConnetionString"];
            services.AddDbContext<MySQLContext>(options => options.UseMySql(connection));

            //if (Environment.IsDevelopment())
            //{
            //    MigrateDataBase(connection);
            //}

            services.AddMvc(options =>
            {
                options.RespectBrowserAcceptHeader = true;
                options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("application/xml"));
                options.FormatterMappings.SetMediaTypeMappingForFormat("json", MediaTypeHeaderValue.Parse("application/json"));
            }).AddXmlSerializerFormatters();

            var filterOptions = new HyperMediaFIlterOptions();
            filterOptions.ContenResponseEnricherList.Add(new PersonEnricher());
            filterOptions.ContenResponseEnricherList.Add(new BookEnricher());
            services.AddSingleton(filterOptions);

            //Versioning
            services.AddApiVersioning();

            //swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                new OpenApiInfo
                {
                    Title = "Rest API's From 0 to Azure with ASP.NET Core 5 and Docker",
                    Version = "v1",
                    Description = "API RESTful developed in course 'Rest API's From 0 to Azure with ASP.NET Core 5 and Docker'",
                    Contact = new OpenApiContact
                    {
                        Name = "Carlos Henrique Brunetti Junior",
                        Url = new Uri("https://github.com/carlosbrunetti")
                    }
                }); ;
            });
            //Dependency Injection

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IPersonBusiness, PersonBusinessImplementation>();
            services.AddScoped<IBooksBusiness, BooksBusinessImplementation>();
            services.AddScoped<ILoginBusiness, LoginBusinessImplementation>();
            services.AddScoped<IFileBusiness, FileBusinessImplementation>();

            services.AddTransient<ITokenService, TokenService>();

            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            //depois de UseRouting,UseHttpsRedirection e antes de UseEndpoints
            app.UseCors();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json"
                    , "Rest API's From 0 to Azure with ASP.NET Core 5 and Docker V1");
            });
            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");
            app.UseRewriter(option);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute("DefaultApi", "{controller=values}/{id?}");
            });
        }

        private void MigrateDataBase(string connection)
        {
            try
            {
                var evolveConnection = new MySql.Data.MySqlClient.MySqlConnection(connection);
                var evolve = new Evolve.Evolve(evolveConnection, msg => Log.Information(msg))
                {
                    Locations = new List<string> { "db/migrations", "db/dataset" },
                    IsEraseDisabled = true,
                };
                evolve.Migrate();
            }
            catch (Exception ex)
            {
                Log.Error("Database migration falied: ", ex);
                throw;
            }
        }
    }
}
