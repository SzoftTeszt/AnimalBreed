using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AnimalsAPI.Data;
using AnimalsAPI.Models;

namespace AnimalsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        private readonly AnimalsAPIContext _context;

        public AnimalsController(AnimalsAPIContext context)
        {
            _context = context;
        }

        // GET: api/Animals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Animal>>> GetAnimal()
        {
            if (_context.Animal == null)
            {
                return NotFound();
            }
            var lista = await _context.Animal.ToListAsync(); 

            foreach (var item in lista)
            {
                item.BreedName = await _context.Breed.Where(x => x.BreedId == item.BreedId).FirstOrDefaultAsync();

            }

            return lista;
        }

        // GET: api/Animals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Animal>> GetAnimal(int id)
        {
            if (_context.Animal == null)
            {
                return NotFound();
            }
            var animal = await _context.Animal.FindAsync(id);

            if (animal == null)
            {
                return NotFound();
            }

            var fajta= await _context.Breed.Where(x=>animal.BreedId==x.BreedId).FirstOrDefaultAsync();
            animal.BreedName = fajta;

            return animal;
        }

        // PUT: api/Animals/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnimal(int id, Animal animal)
        {
            if (id != animal.Id)
            {
                return BadRequest();
            }

            _context.Entry(animal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnimalExists(id))
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

        // POST: api/Animals
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Animal>> PostAnimal(Animal animal)
        {
            if (_context.Animal == null)
            {
                return Problem("Entity set 'AnimalsAPIContext.Animal'  is null.");
            }
            //LINQ
            var fajta = _context.Breed.Where(x => x.BreedId == animal.BreedId).FirstOrDefault();
            animal.BreedName = fajta;

            _context.Animal.Add(animal);
            
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAnimal", new { id = animal.Id }, animal);
        }

        // DELETE: api/Animals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnimal(int id)
        {
            if (_context.Animal == null)
            {
                return NotFound();
            }
            var animal = await _context.Animal.FindAsync(id);
            if (animal == null)
            {
                return NotFound();
            }

            _context.Animal.Remove(animal);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AnimalExists(int id)
        {
            return (_context.Animal?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
