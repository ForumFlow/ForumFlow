using ForumFlowServer.CreateTables;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using DotNetEnv;
using ForumFlowServer.JWT;
using Microsoft.Data.Sqlite;
using PresentationDao;
using UserDao;

namespace MyWebApplication
{

  class Program
  {
    private static SqlUtil db = new SqlUtil();
    private static PresentationDao.PresentationDao presentationDao = new();
    private static UserDao.UserDao userD = new();
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
          case "showFaqs":
            db.showAllFaqs();
            break;

          case "insertTestData":
            Console.WriteLine("deleteing tables and inserting test data..");
            db.createTables();
            db.createTestinData();
            break;
          case "showUsers":
            db.showAllUsers();
            break;

          case "showAllPresentations":
            Console.WriteLine("Showing all presentations...");
            db.showAllPresentations();
            break;
          case "testToken":
            var header = "{\"alg\": \"HS256\", \"typ\": \"JWT\"}";
            var payload = "{\"sub\": \"1234567890\", \"name\": \"John Doe\", \"iat\": 1516239022}";
            var secret = "your-256-bit-secret";
            var token = JWT.CreateToken(header, payload, secret);
            Console.WriteLine("JWT: " + token);
            var isValid = JWT.ValidateToken(token, secret);
            if (isValid)
            {
              Console.WriteLine("Token is valid");
            }
            else
            {
              Console.WriteLine("Token is invalid");
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

        app.MapControllers();

        // server side sessions
        app.UseSession();
        app.UseCors
        (options => options.WithOrigins("http://localhost:5173")
          .AllowAnyMethod()
          .AllowAnyHeader()
          .AllowCredentials()
        );



        Console.WriteLine("Running Server..");
        app.Run();
      }
    }
  }
}

