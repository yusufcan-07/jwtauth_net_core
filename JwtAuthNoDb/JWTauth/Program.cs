using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JWTauth.Interfaces;
using JWTauth.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace JWTauth
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureServices((hostContext, services) =>
                    {
                        services.AddControllers();
                        services.AddSwaggerGen();
                        services.AddTransient<IAuthService, AuthService>();
                        services.AddTransient<ITokenService, TokenService>();
                        services.AddTransient<IDocumentService, DocumentService>();

                        services.AddAuthentication(options =>
                        {
                            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                        }).AddJwtBearer(o => o.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidIssuer = hostContext.Configuration["JWT:ValidIssuer"],
                            ValidAudience = hostContext.Configuration["JWT:ValidAudience"],
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(hostContext.Configuration["JWT:Secret"])),
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true
                        });
                    }).Configure(app =>
                    {
                        var env = app.ApplicationServices.GetService<IWebHostEnvironment>();
                        if (env.IsDevelopment())
                        {
                            app.UseDeveloperExceptionPage();
                            app.UseSwagger();
                            app.UseSwaggerUI();
                        }

                        app.UseRouting();
                        app.UseAuthentication();
                        app.UseAuthorization();
                        
                        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
                    });
                });
    }
}
