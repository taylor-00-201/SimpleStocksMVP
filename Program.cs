using SimpleStocks.Interfaces;
using SimpleStocks.Repositories;

namespace SimpleStocks
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddTransient<IStockUserRepository, StockUserRepository>();
            builder.Services.AddTransient<ILoginRepository, LoginRepository>();
            builder.Services.AddTransient<IAssetRepository, AssetRepository>();
            builder.Services.AddTransient<ITransactionsRepository, TransactionsRepository>();

            var app = builder.Build();

            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}