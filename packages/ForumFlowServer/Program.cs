using ForumFlowServer.CreateTables;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using DotNetEnv;
using JWT;

using System;

namespace MyWebApplication
{
    class Program
    {
        private static SqlUtil db = new SqlUtil();
        private static JWT.JWT jwt = new JWT.JWT();
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
                    case "testToken":
                        var header = "{\"alg\": \"HS256\", \"typ\": \"JWT\"}";
                        var payload = "{\"sub\": \"1234567890\", \"name\": \"John Doe\", \"iat\": 1516239022}";
                        var secret = "your-256-bit-secret";

                        var token = JWT.JWT.CreateToken(header, payload, secret);
                        Console.WriteLine("JWT: " + token);
                        var isValid = JWT.JWT.ValidateToken(token, secret);
                        if (isValid)
                        {
                            Console.WriteLine("Token is valid");
                        }
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
