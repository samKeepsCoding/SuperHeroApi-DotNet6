using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHeroApi.Models;

namespace SuperHeroApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private static List<SuperHero> heros = new List<SuperHero>()
        {
           new SuperHero
           {
               Id= 1,
               Name= "SpiderMan",
               FirstName= "Peter",
               LastName= "Parker",
               Place= "Queens, New York"
           },
           new SuperHero
           {
               Id=2,
               Name= "IronMan",
               FirstName= "Tony",
               LastName= "Stark",
               Place = "New York"
           }
        };
        private readonly DataContext _context;

        public SuperHeroController(DataContext dataContext)
        {
            _context = dataContext;
        }
        
        [HttpGet]
            public async Task<ActionResult<List<SuperHero>>> Get()
            {
                return Ok(await _context.SuperHeroes.ToListAsync());
            }
        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> Get(int id)
        {
            var hero = await _context.SuperHeroes.FindAsync(id);
            if (hero == null)
            {
                return BadRequest("Hero not Found...");
            } 
            else
            {
                return Ok(hero);
            }
            
        }
        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {   _context.SuperHeroes.Add(hero);
         
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero request , int id)
        {
            var hero = await _context.SuperHeroes.FindAsync(id);

            if (hero == null)
            {
                return BadRequest("Hero not Found...");
            }
            else
            {
                hero.Name = request.Name;
                hero.FirstName = request.FirstName;
                hero.LastName = request.LastName;
                hero.Place = request.Place;

                await _context.SaveChangesAsync();

                return Ok(await _context.SuperHeroes.ToListAsync());
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> DeleteHero(int id)
        {
            var hero = await _context.SuperHeroes.FindAsync(id);
            if (hero == null )
            {
                return BadRequest("Hero was not found to be deleted");
            } 
            else
            {
                _context.SuperHeroes.Remove(hero);
                await _context.SaveChangesAsync();
                return Ok(await _context.SuperHeroes.ToListAsync());
            }
        }
       
    }
}
