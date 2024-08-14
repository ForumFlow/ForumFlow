using Microsoft.AspNetCore.Mvc;
using FaqDao;
using ForumFlowServer.HashUtils;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Schema;
using ForumFlowServer.JWT;

using DotNetEnv;
using System.Net;
namespace ForumFlow.faqControllers{
    [ApiController]
    [Route("faq")]
    public class faqController : ControllerBase{
        private static readonly FaqDao.FaqDao faqDao = new();

        [HttpPost("createFaq")]
        public ActionResult<newFaqPostRequest> CreateFaq([FromBody] newFaqPostRequest request){
            if (request == null || request.presentationId == 0 || request.question == null || request.answer == null || request.displayOrder == null){
                Console.WriteLine("Bad Request");
                return BadRequest();
            }
            // if (!faqDao.FaqExists(request.presentationId)){
            //     Console.WriteLine("User does not exist");
            //     return BadRequest();
            // }
            // var faqID = faqDao.GetFaq(request.ID);
            // if (faqID == null){
            //     Console.WriteLine("User does not have a faq");
            //     return BadRequest();
            // }
            int ID = 1;
            int presentationId = 1;
            faqDao.CreateFaq(ID, presentationId, request.question, request.answer, request.displayOrder);
            return Ok();
        }
    }
}