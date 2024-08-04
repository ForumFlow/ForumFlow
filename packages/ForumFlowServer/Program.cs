// var builder = WebApplication.CreateBuilder(args);

// // Add services to the container.
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

// var app = builder.Build();

// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

// app.UseHttpsRedirection();

// // Map routes
// app.MapWeatherForecastRoutes();

// app.Run();

using ForumFlowServer.CreateTables;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace MyWebApplication
{
    class Program
    {
        private static SqlUtil db = new SqlUtil();
        static void Main(string[] args)
        {

            if (args.Length == 1)
            {
                switch (args[0])
                {
                    case "clearTables":
                        Console.WriteLine("Recreating Tables..");
                        db.createTables();
                        break;

                    case "insertTestData":
                        Console.WriteLine("deleteing tables and inserting test data..");
                        db.createTables();
                        db.createTestinData();
                        break;
                    case "showUsers":
                        db.showAllUsers();
                        break;
                }

            }
            else
            {

                var builder = WebApplication.CreateBuilder(args);

                // Add services to the container.
                builder.Services.AddControllers();

                var app = builder.Build();

                // Configure the HTTP request pipeline.
                app.UseAuthorization();
                app.MapControllers();
                Console.WriteLine("Running Server..");
                app.Run();
            }
        }
    }
}
