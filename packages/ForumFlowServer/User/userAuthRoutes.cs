using Microsoft.AspNetCore.Mvc;
using ForumFlowServer.UserDao;

namespace ForumFlow.userAuthenticationControllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private static readonly UserDao userDao = new();

        // GET: /user
        // [HttpGet]
        // public ActionResult<userAuthenticationControllers/> GetAll()
        // {
        //     return items;
        // }

        // GET: /user/{id}

        // curl -X POST http://localhost:5000/api/users -H "Content-Type: application/json" -d '{
        //   "username": "exampleUser",
        //   "password": "examplePassword",
        //   "firstName": "John",
        //   "lastName": "Doe"
        // }'

        // ^^ This is the curl command to test the post request

        // POST: /user/{username}/{passwordHash}
        // implement authentication with this endpoint
        [HttpPost]
        public ActionResult<newUsersPostRequest> Create([FromBody] newUsersPostRequest request)
        {
            if (request == null || request.username == null || request.password == null || request.firstName == null || request.lastName == null)
            {
                Console.WriteLine("Bad Request");
                return BadRequest();
            }
            else
            {
                Console.WriteLine("Good Request");
                return Ok(request);
            }
        }

    }
}
