using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TourOfHeroes.Backend.Models;

namespace TourOfHeroes.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeroesController : ControllerBase
    {
        private readonly ApiContext _context;

        public HeroesController(ApiContext context)
        {
            _context = context;
        }

        // GET: api/Heroes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hero>>> GetHeroes()
        {
            return await _context.Heroes.ToListAsync();
        }

        // GET: api/Heroes?id=5
        [HttpGet]
        [ExactQueryParam("id")]
        public async Task<ActionResult<Hero>> GetHeroNo404([FromQuery] int id)
        {
            var hero = await _context.Heroes.FindAsync(id);
            return hero;
        }

        // GET: api/Heroes?name=xxx
        [HttpGet]
        [ExactQueryParam("name")]
        public async Task<ActionResult<IEnumerable<Hero>>> SearchHeroes([FromQuery] string name)
        {
            return await _context.Heroes.Where(x => x.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToListAsync();
        }

        // GET: api/Heroes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Hero>> GetHero(int id)
        {
            var hero = await _context.Heroes.FindAsync(id);

            if (hero == null)
            {
                return NotFound();
            }

            return hero;
        }

        // PUT: api/Heroes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHero(int id, Hero hero)
        {
            if (id != hero.Id)
            {
                return BadRequest();
            }

            _context.Entry(hero).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HeroExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Heroes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Hero>> PostHero(Hero hero)
        {
            _context.Heroes.Add(hero);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHero", new { id = hero.Id }, hero);
        }

        // DELETE: api/Heroes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Hero>> DeleteHero(int id)
        {
            var hero = await _context.Heroes.FindAsync(id);
            if (hero == null)
            {
                return NotFound();
            }

            _context.Heroes.Remove(hero);
            await _context.SaveChangesAsync();

            return hero;
        }

        private bool HeroExists(int id)
        {
            return _context.Heroes.Any(e => e.Id == id);
        }
    }
}
