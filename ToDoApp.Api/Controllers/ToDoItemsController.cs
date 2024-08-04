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

        [HttpPut("{id}/title")]
        public async Task<ActionResult> UpdateTitleAsynk(int id, ChangeToDoItemTitleDto changeToDoItemTitleDto)
        {
            await _service.UpdateTitleAsync(id, changeToDoItemTitleDto);
            return Ok();
        }

        [HttpPut("{id}/description")]
        public async Task<ActionResult> UpdateDescriptionAsynk(int id, ChangeToDoItemDescriptionDto changeToDoItemDescriptionDto)
        {
            await _service.UpdateDescriptionAsync(id, changeToDoItemDescriptionDto);
            return Ok();
        }

        [HttpPut("{id}/status")]
        public async Task<ActionResult> UpdateStatusAsynk(int id, UpdateStatusDto updateStatusDto)
        {
            await _service.UpdateStatusAsync(id, updateStatusDto);
            return Ok();
        }

        [HttpPut("{id}/assignee")]
        public async Task<ActionResult> UpdateAssigneeAsynk(int id, int newAssignee)
        {
            await _service.UpdateAssigneeAsync(id, newAssignee);
            return Ok();
        }
        

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsynk(int id)
        {
            await _service.DeleteAsync(id);
            return Ok();
        }
    }
}
