using InternetShop.DAL.Contracts;
using InternetShop.DAL.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using InternetShop.BAL.AuthService;
using InternetShop.BAL.Contracts;
using InternetShop.BAL.Services.ProductService;
using InternetShop.BAL.Services.OrderService;
using InternetShop.BAL.Services.GroupService;
using InternetShop.BAL.Services.OrderDetailService;
using System.Net.Http.Headers;
using InternetShop.BAL.Services.FileStorageService;
using InternetShop.BAL.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace InternetShop.API.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "My API",
                    Version = "v1"
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                   {
                     new OpenApiSecurityScheme
                     {
                       Reference = new OpenApiReference
                       {
                         Type = ReferenceType.SecurityScheme,
                         Id = "Bearer"
                       }
                      },
                      new string[] { }
                    }
                });
            });
        }
        public static void ConfigureCookieAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
        }
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IBrandService, BrandService>();
            services.AddTransient<IImageUploader, ImageUploader>();
            services.AddTransient<ISeasonsService, SeasonsService>();
            services.AddTransient<IOrderDetailService, OrderDetailService>();
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        }
        public static void ConfigureJWT(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                var Key = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["JWT:Issuer"],
                    ValidAudience = configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Key)
                };
            });

        }
        public static void ConfigureHttpClient(this IServiceCollection services)
        {
            services.AddHttpClient("FileStorage", c =>
            {
                c.BaseAddress = new Uri("https://localhost:7098/api/");
                c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));
            }
            );
        }
    }
}
