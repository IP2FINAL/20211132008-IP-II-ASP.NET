using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using News_Asp_Net_Api.Data;
using News_Asp_Net_Api.Models;

namespace News_Asp_Net_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class kategorilersController : ControllerBase
    {
        private readonly ApplicationDbContextApp _context;

        public kategorilersController(ApplicationDbContextApp context)
        {
            _context = context;
        }

        // GET: api/kategorilers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<kategoriler>>> Getkategoriler()
        {
          if (_context.kategoriler == null)
          {
              return NotFound();
          }
            return await _context.kategoriler.ToListAsync();
        }

        // GET: api/kategorilers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<kategoriler>> Getkategoriler(int id)
        {
          if (_context.kategoriler == null)
          {
              return NotFound();
          }
            var kategoriler = await _context.kategoriler.FindAsync(id);

            if (kategoriler == null)
            {
                return NotFound();
            }

            return kategoriler;
        }

        // PUT: api/kategorilers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putkategoriler(int id, kategoriler kategoriler)
        {
            if (id != kategoriler.Id)
            {
                return BadRequest();
            }

            _context.Entry(kategoriler).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!kategorilerExists(id))
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

        // POST: api/kategorilers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<kategoriler>> Postkategoriler(kategoriler kategoriler)
        {
          if (_context.kategoriler == null)
          {
              return Problem("Entity set 'ApplicationDbContextApp.kategoriler'  is null.");
          }
            _context.kategoriler.Add(kategoriler);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getkategoriler", new { id = kategoriler.Id }, kategoriler);
        }

        // DELETE: api/kategorilers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletekategoriler(int id)
        {
            if (_context.kategoriler == null)
            {
                return NotFound();
            }
            var kategoriler = await _context.kategoriler.FindAsync(id);
            if (kategoriler == null)
            {
                return NotFound();
            }

            _context.kategoriler.Remove(kategoriler);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool kategorilerExists(int id)
        {
            return (_context.kategoriler?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
