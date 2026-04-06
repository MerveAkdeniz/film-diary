using FilmDiary.API.Data;
using FilmDiary.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FilmDiary.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActorsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ActorsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetActors()
        {
            var actors = await _context.Actors.ToListAsync();
            return Ok(actors);
        }

        [HttpPost]
        public async Task<IActionResult> AddActor(Actor actor)
        {
            _context.Actors.Add(actor);
            await _context.SaveChangesAsync();
            return Ok(actor);
        }
    }
}