using Microsoft.AspNetCore.Mvc;
using ToDoApp.Data.Models;
using ToDoApp.Services.Dtos;
using ToDoApp.Services.Interfaces;

namespace ToDoApp.Api.Controllers
{
    [Route("api/to-do-items")]
    [ApiController]
    public class ToDoItemsController : ControllerBase
    {
        private readonly IToDoItemService _service;

        public ToDoItemsController(IToDoItemService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDoItem>>> GetAsynk()
        {
            var items = await _service.GetAsynk();
            return Ok(items);
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsynk(CreateToDoItemDto itemDto)
        {
            await _service.CreateAsync(itemDto);
            return Ok();
        }

        [HttpPut("{id}/status")]
        public async Task<ActionResult> UpdateStatusAsynk(int id)
        {
            await _service.UpdateStatusAsync(id);
            return Ok();
        }
    }
}
