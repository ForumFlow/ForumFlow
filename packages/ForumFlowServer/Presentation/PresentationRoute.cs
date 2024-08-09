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
        public ActionResult Create([FromBody] PresentationRequest request)
        {
            if (request == null || request.PresentationID <= 0 || string.IsNullOrEmpty(request.Title) || string.IsNullOrEmpty(request.Description))
            {
                return BadRequest("Invalid request data");
            }


            if (presentationDao.PresentationExists(request.PresentationID))
            {
                return BadRequest("Presentation already exists");
            }


            presentationDao.CreatePresentation(request.PresentationID, request.Title, request.Description, request.CreatedDate);
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
            if (request == null || request.PresentationID <= 0 || string.IsNullOrEmpty(request.Title) || string.IsNullOrEmpty(request.Description))
            {
                return BadRequest("Invalid request data");
            }


            if (!presentationDao.PresentationExists(request.PresentationID))
            {
                return NotFound("Presentation not found");
            }


            presentationDao.UpdatePresentation(request.PresentationID, request.Title, request.Description, request.CreatedDate);
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
