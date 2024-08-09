using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Services.Dtos;
using ToDoApp.Services.Interfaces;

namespace ToDoApp.Api.Controllers
{
    [Authorize]
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
        public async Task<ActionResult<IEnumerable<GetToDoItemDto>>> GetAsynk()
        {
            var items = await _service.GetAsynk();
            var itemsDto = items.Select(x => new GetToDoItemDto
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                CreatedAt = x.CreatedAt,
                DueDate = x.DueDate,
                StatusId = x.StatusId,
                BoardId = x.BoardId,
                AssigneeId = x.AssigneeId
            });
            return Ok(itemsDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetToDoItemDto>> GetByIdAsynk(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item is null)
            {
                return NotFound();
            }

            var itemDto = new GetToDoItemDto
            {
                Id = item.Id,
                Title = item.Title,
                Description = item.Description,
                CreatedAt = item.CreatedAt,
                DueDate = item.DueDate,
                StatusId = item.StatusId,
                BoardId = item.BoardId,
                AssigneeId = item.AssigneeId
            };
            return Ok(itemDto);
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
        public async Task<ActionResult> UpdateAssigneeAsynk(int id, string newAssignee)
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
