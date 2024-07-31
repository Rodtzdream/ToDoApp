using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Data.Context;
using ToDoApp.Data.Models;
using ToDoApp.Services.Dtos;
using ToDoApp.Services.Interfaces;

namespace ToDoApp.Api.Controllers
{
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
        public async Task<ActionResult<IEnumerable<Board>>> GetAsynk()
        {
            var boards = await _context.Boards.Include(b => b.ToDoItems).ToListAsync();
            return Ok(boards);
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsynk(CreateBoardDto boardDto)
        {
            await _service.CreateAsync(boardDto);
            return Ok();
        }
    }
}
