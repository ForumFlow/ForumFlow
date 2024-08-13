using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging; // For logging
using System.Threading.Tasks;
using ForumFlowServer.JWT;
using Microsoft.AspNetCore.Http.HttpResults; // Your custom JWT namespace
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

public class ValidateJwtAttribute : Attribute, IAsyncActionFilter
{
  public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
  {
    // Accessing HttpContext
    HttpContext httpContext = context.HttpContext;

    // Accessing Request
    HttpRequest request = httpContext.Request;

    // Accessing Headers
    IHeaderDictionary headers = request.Headers;

    // Example: Fetch a specific header
    string value = headers["Authorization"];  // Replace "HeaderName" with the actual header key you're interested in
    Console.WriteLine("Authorization: " + value);
    if (value == null)
    {
      context.HttpContext.Response.StatusCode = 400; // BadRequest
      await context.HttpContext.Response.WriteAsync("JWT token is missing");
      return;
    }
    var token = value != null ? value.Split(" ") : null;
    string[] parts = value.Split(" ");
    if (parts.Length != 2 || parts[0] != "Bearer")
    {
      context.HttpContext.Response.StatusCode = 400; // BadRequest
      await context.HttpContext.Response.WriteAsync("JWT token is missing");
      return;
    }

    if (!IsValidJwt(parts[1]))
    {
      context.HttpContext.Response.StatusCode = 401; // Not Authorized
      await context.HttpContext.Response.WriteAsync("Invalid or missing JWT token");
      return;
    }
    await next(); // Continue execution if JWT is valid

  }

  private bool IsValidJwt(string token)
  {
    var secretKey = Environment.GetEnvironmentVariable("JWT_SECRET");
    if (secretKey == null)
    {
      return false;
    }
    else
    {

      var isValid = JWT.ValidateToken(token, secretKey);
      if (isValid)
      {
        Console.WriteLine("JWT is valid");
      }
      else
      {

        Console.WriteLine("JWT is invalid");
      }
    }
    // This method should implement actual JWT validation logic
    // For demonstration, it just checks if the token contains a dot (.)
    return true;

  }
}
