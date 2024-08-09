using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Data.Context;
using ToDoApp.Services.Dtos;
using ToDoApp.Services.Interfaces;

namespace ToDoApp.Api.Controllers
{
    [Authorize]
    [Route("api/boards")]
    [ApiController]
    public class BoardsController : ControllerBase
    {
        private readonly ToDoContext _context;
        private readonly IBoardService _service;

        public BoardsController(IBoardService service, ToDoContext context)
        {
            _context = context;
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetBoardDto>>> GetAsynk()
        {
            return await _service.GetAsynk();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetBoardDto>> GetByIdAsynk(int id)
        {
            var board = await _context.Boards
                .Include(board => board.ToDoItems)
                .FirstOrDefaultAsync(board => board.Id == id);

            if (board == null)
            {
                return NotFound();
            }

            return new GetBoardDto
            {
                Id = board.Id,
                Name = board.Name,
                CreatedAt = board.CreatedAt,
                ToDoItems = board.ToDoItems.Select(toDoItem => new GetToDoItemDto
                {
                    Id = toDoItem.Id,
                    Title = toDoItem.Title,
                    Description = toDoItem.Description,
                    CreatedAt = toDoItem.CreatedAt,
                    DueDate = toDoItem.DueDate,
                    StatusId = toDoItem.StatusId,
                    BoardId = toDoItem.BoardId,
                    AssigneeId = toDoItem.AssigneeId
                }).ToList()
            };
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsynk(CreateBoardDto boardDto)
        {
            await _service.CreateAsync(boardDto);
            return Ok();
        }
    }
}
