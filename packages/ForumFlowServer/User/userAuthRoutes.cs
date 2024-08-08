using Microsoft.AspNetCore.Mvc;
using UserDao;
using ForumFlowServer.HashUtils;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Schema;
using ForumFlowServer.JWT;

using DotNetEnv;
namespace ForumFlow.userAuthenticationControllers
{
  [ApiController]
  [Route("user")]
  public class UserController : ControllerBase
  {
    private static readonly UserDao.UserDao userDao = new();
    private static string GetSalt()
    {
      var randomNumber = new byte[32];
      RandomNumberGenerator.Fill(randomNumber);
      return Convert.ToBase64String(randomNumber);
    }

    private static string GetHash(string text)
    {
      using (var sha256 = SHA256.Create())
      {
        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(text));
        return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
      }
    }

    // curl -X POST http://localhost:5152/user/createUser -H "Content-Type: application/json" -d '{
    //   "username": "exampleUser",
    //   "password": "examplePassword",
    //   "firstName": "John",
    //   "lastName": "Doe"
    // }'
    // ^^ This is the curl command to test the post request

    // POST: /user/{username}/{passwordHash}
    // implement authentication with this endpoint
    [HttpGet("testToken")]
    [ValidateJwt]
    public IActionResult testToken()
    {
      return Ok("This is a secure data response.");
    }


    [HttpPost("login")]
    public ActionResult<newUsersPostRequest> Login([FromBody] newUsersPostRequest request)
    {
      if (request == null || request.username == null || request.password == null)
      {
        Console.WriteLine("Bad Request");
        return BadRequest();
      }
      if (!userDao.userExists(request.username))
      {
        Console.WriteLine("User does not exist");
        return BadRequest("User does not exist");
      }
      else
      {
        var salt = userDao.getUserSalt(request.username);
        var passwordHash = GetHash(request.password + salt);
        if (userDao.authenticateUser(request.username, passwordHash))
        {
          Env.Load();
          var secretKey = Environment.GetEnvironmentVariable("JWT_SECRET");
          if (secretKey == null)
          {
            Console.WriteLine("JWT_SECRET not found in .env file");
            return BadRequest("JWT_SECRET not found in .env file");
          }
          else
          {
            var token = JWT.CreateToken("{\"alg\": \"HS256\", \"typ\": \"JWT\"}", "{\"sub\": \"" + request.username + "\", \"name\": \"" + request.firstName + " " + request.lastName + "\", \"iat\": " + DateTime.Now.Ticks + "}", secretKey);

            // how to set a cookie in .net
            Response.Cookies.Append("jwt", token, new CookieOptions
            {
              HttpOnly = true
            });
            Console.WriteLine("Good Request");
            Console.WriteLine("Token: " + token);
            return Ok(token);
          }
        }
        else
        {
          Console.WriteLine("Bad Request");
          return BadRequest("Password is incorrect");
        }
      }
    }


    [HttpPost("create")]
    public ActionResult<newUsersPostRequest> Create([FromBody] newUsersPostRequest request)
    {
      if (request == null || request.username == null || request.password == null || request.firstName == null || request.lastName == null)
      {
        Console.WriteLine("Bad Request");
        return BadRequest();
      }
      // TODO implement userExists function
      if (userDao.userExists(request.username))
      {
        Console.WriteLine("User already exists");
        return BadRequest("User already exists");
      }
      else
      {
        var salt = GetSalt();
        var passwordHash = GetHash(request.password + salt);
        Env.Load();
        var secretKey = Environment.GetEnvironmentVariable("JWT_SECRET");
        if (secretKey == null)
        {
          Console.WriteLine("JWT_SECRET not found in .env file");
          return BadRequest("JWT_SECRET not found in .env file");
        }
        else
        {

          var token = JWT.CreateToken("{\"alg\": \"HS256\", \"typ\": \"JWT\"}", "{\"sub\": \"" + request.username + "\", \"name\": \"" + request.firstName + " " + request.lastName + "\", \"iat\": " + DateTime.Now.Ticks + "}", secretKey);

          userDao.createUser(request.username, salt, passwordHash, request.firstName, request.lastName);
          Console.WriteLine("Good Request");
          Console.WriteLine("Token: " + token);
          Response.Cookies.Append("jwt", token, new CookieOptions
          {
            HttpOnly = true, // Important: Makes the cookie inaccessible to JavaScript
            Secure = true, // Transmits the cookie over HTTPS only
            SameSite = SameSiteMode.Strict, // CSRF protection
            Expires = DateTimeOffset.UtcNow.AddDays(1) // Set your cookie expiration as needed
          });
          return Ok(token);


        }


        // return CreatedAtAction("GetUser", new { id = user.Id }, user);

      }
    }

  }
}
