using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Ninject.Activation;
using Stripe;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopeApi.Data;
using WebShopeApi.IServices;
using WebShopeApi.Models;
using WebShopeApi.Services;
using WebShopeApi.Settings;
using ProductService = WebShopeApi.Services.ProductService;

namespace WebShopeApi
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
            // Connection Database
            services.AddDbContext<DbShopContext>(
             options => options.UseSqlServer(Configuration.GetConnectionString("DBConnection"))
             );
            // Configure Result Controller
            services.AddControllers().AddNewtonsoftJson(options =>
             options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
           );

            //Configure Stripe API Payment methode 
            services.Configure<StripeSettings>(Configuration.GetSection("Stripe"));
           
            //Configure JWT 

            var jwtTokenConfig = Configuration.GetSection("jwtTokenConfig").Get<JwtTokenConfig>();
            services.AddSingleton(jwtTokenConfig);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtTokenConfig.Issuer,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtTokenConfig.Secret)),
                    ValidAudience = jwtTokenConfig.Audience,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(1)
                };
            });

            //configure  application services
            services.AddHostedService<JwtRefreshTokenCache>();
            services.AddSingleton<IJwtAuthManager, JwtAuthManager>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ISalesService, SalesService>();

            // CORS Origini 
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                        builder =>
                        {
                            builder
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                        });
            });

            //Configure SWAGGER 
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebShopeApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            StripeConfiguration.ApiKey = "sk_test_51JCCnzIdfeWid4k8gogkOR6aER3KDImbVLlLBjpWpmbrDdq3i16sIev665bqafGreW78OXLJtz7HMzYxNUrIBPaI00gBpFVWdU";
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
           

            app.UseSwagger(c => { c.RouteTemplate = "WebShopeApi/swagger/{documentName}/swagger.json"; });
            app.UseSwaggerUI(c => {
         
                c.SwaggerEndpoint("WebShopeApi/swagger/v1/swagger.json", "WebShopeApi v1");
                c.RoutePrefix = string.Empty;
            });
            
            app.UseRouting();
            app.UseCors("AllowSpecificOrigin");

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
