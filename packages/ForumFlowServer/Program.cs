using ForumFlowServer.CreateTables;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;

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

                // add server side sessions
                builder.Services.AddDistributedMemoryCache();

                builder.Services.AddSession(options =>
                {
                    options.IdleTimeout = TimeSpan.FromMinutes(30);
                    options.Cookie.HttpOnly = true;
                    options.Cookie.IsEssential = true;
                });






                var app = builder.Build();

                // Configure the HTTP request pipeline.
                app.UseAuthorization();
                app.MapControllers();

                // server side sessions
                app.UseSession();


                Console.WriteLine("Running Server..");
                app.Run();
            }
        }
    }
}
