using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
            var board = await _service.GetByIdAsynk(id);

            if (board == null)
            {
                return NotFound();
            }

            return board;
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsynk(CreateBoardDto boardDto)
        {
            await _service.CreateAsync(boardDto);
            return Ok();
        }
    }
}
