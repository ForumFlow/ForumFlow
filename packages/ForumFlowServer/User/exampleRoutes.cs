using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Cryptography;


// TODO
namespace ForumFlow.exampleControllers
{
    [ApiController]
    [Route("products")]
    public class ItemsController : ControllerBase
    {
        private static readonly List<Item> items = new List<Item>
        {
            new Item { Id = 1, Name = "Item 1" },
            new Item { Id = 2, Name = "Item 2" },
            new Item { Id = 3, Name = "Item 3" }
        };

        // GET: /products
        [HttpGet]
        public ActionResult<List<Item>> GetAll()
        {
            return items;
        }

        // GET: /products/{id}
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
