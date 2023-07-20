using Microsoft.Net.Http.Headers;

namespace MyWeb.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddCors(opts =>
            {
                opts.AddPolicy("AllowSites", builder =>
                {
                    builder.WithOrigins("https://*.example.com").SetIsOriginAllowedToAllowWildcardSubdomains().AllowAnyHeader().AllowAnyMethod();
                });

                opts.AddPolicy("AllowSitesSecond", builder =>
                {
                    builder.WithOrigins("https://localhost:7211").WithMethods("POST","GET").AllowAnyHeader();
                });
                //opts.AddDefaultPolicy(builder =>
                //{
                //    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                //});
            });
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            //app.UseCors("AllowSitesSecond");
            app.UseCors();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}