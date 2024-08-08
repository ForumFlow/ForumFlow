using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging; // For logging
using System.Threading.Tasks;
using ForumFlowServer.JWT;
using Microsoft.AspNetCore.Http.HttpResults; // Your custom JWT namespace
using Microsoft.AspNetCore.Mvc;

public class ValidateJwtAttribute : Attribute, IAsyncActionFilter
{
  public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
  {
    var cookie = context.HttpContext.Request.Cookies["jwt"];
    var token = cookie != null ? cookie.Split(" ").LastOrDefault() : null;
    if (token == null)
    {
      context.HttpContext.Response.StatusCode = 400; // BadRequest
      await context.HttpContext.Response.WriteAsync("JWT token is missing");
      return;
    }
    if (!IsValidJwt(token))
    {
      context.HttpContext.Response.StatusCode = 401; // Not Authorized
      await context.HttpContext.Response.WriteAsync("Invalid or missing JWT token");
      return;
      ;
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
