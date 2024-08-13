using Microsoft.AspNetCore.Mvc;
using PresentationDao; 
using System;


namespace ForumFlow.PresentationControllers
{
    [ApiController]
    [Route("presentation")]
    public class PresentationController : ControllerBase
    {
        private static readonly PresentationDao.PresentationDao presentationDao = new();


        [HttpPost("create")]
        public ActionResult<PresentationRequest> Create([FromBody] PresentationRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.title) || string.IsNullOrEmpty(request.description) || request.authorId <= 0)
            {
                return BadRequest("Invalid request data");
            }


            // if (presentationDao.PresentationExists(request.presentationId))
            // {
            //     return BadRequest("Presentation already exists");
            // }

            presentationDao.CreatePresentation(request.title, request.description, request.authorId);
            return Ok("Presentation created successfully");
        }


        [HttpGet("{id}")]
        public ActionResult Get(int id) // GET presentation to retrieve presentation by id
        {
            if (id <= 0)
            {
                return BadRequest("Invalid ID");
            }


            var presentation = presentationDao.GetPresentation(id);
            if (presentation == null)
            {
                return NotFound("Presentation not found");
            }


            return Ok(presentation);
        }


        [HttpPut("update")]
        public ActionResult Update([FromBody] PresentationRequest request)
        {
            if (request == null || request.presentationId <= 0 || string.IsNullOrEmpty(request.title) || string.IsNullOrEmpty(request.description) || request.authorId <= 0)
            {
                return BadRequest("Invalid request data");
            }


            if (!presentationDao.PresentationExists(request.presentationId))
            {
                return NotFound("Presentation not found");
            }


            presentationDao.UpdatePresentation(request.presentationId, request.title, request.description, request.authorId);
            return Ok("Presentation updated successfully");
        }


        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid ID");
            }


            if (!presentationDao.PresentationExists(id))
            {
                return NotFound("Presentation not found");
            }


            presentationDao.DeletePresentation(id);
            return Ok("Presentation deleted successfully");
        }
    }
}
