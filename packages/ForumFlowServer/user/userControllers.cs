using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ForumFlow.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemsController : ControllerBase
    {
        private static readonly List<Item> items = new List<Item>
        {
            new Item { Id = 1, Name = "Item 1" },
            new Item { Id = 2, Name = "Item 2" },
            new Item { Id = 3, Name = "Item 3" }
        };

        // GET: /items
        [HttpGet]
        public ActionResult<List<Item>> GetAll()
        {
            return items;
        }

        // GET: /items/{id}
        [HttpGet("{id}")]
        public ActionResult<Item> Get(int id)
        {
            var item = items.Find(i => i.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }
    }
}
