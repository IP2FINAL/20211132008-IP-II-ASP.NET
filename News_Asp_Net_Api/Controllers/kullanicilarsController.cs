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
    public class kullanicilarsController : ControllerBase
    {
        private readonly ApplicationDbContextApp _context;

        public kullanicilarsController(ApplicationDbContextApp context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public IActionResult Login(LoginModel model)
        {
            // Kullanıcı adı ve şifreyi alın
            string username = model.username;
            string password = model.password;

            // Kullanıcıyı kontrol etmek için örnek bir sorgu yapılıyor
            var user = _context.Kullanicilar.FirstOrDefault(u => u.kullaniciAdi == username && u.sifre == password);
            if (user == null)
            {
                // Kullanıcı bulunamazsa hata mesajı döndür
                return NotFound("Kullanıcı bulunamadı");
            }

            // Kullanıcı bulunursa başarılı yanıt döndür
            return Ok(user.RolId);
        }


        // GET: api/kullanicilars
        [HttpGet]
        public async Task<ActionResult<IEnumerable<kullanicilar>>> GetKullanicilar()
        {
            if (_context.Kullanicilar == null)
            {
                return NotFound();
            }
            return await _context.Kullanicilar.ToListAsync();
        }

        // GET: api/kullanicilars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<kullanicilar>> Getkullanicilar(int id)
        {
            if (_context.Kullanicilar == null)
            {
                return NotFound();
            }
            var kullanicilar = await _context.Kullanicilar.FindAsync(id);

            if (kullanicilar == null)
            {
                return NotFound();
            }

            return kullanicilar;
        }

        // PUT: api/kullanicilars/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putkullanicilar(int id, kullanicilar kullanicilar)
        {
            if (id != kullanicilar.Id)
            {
                return BadRequest();
            }

            _context.Entry(kullanicilar).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!kullanicilarExists(id))
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

        // POST: api/kullanicilars
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<kullanicilar>> Postkullanicilar(kullanicilar kullanicilar)
        {
            if (_context.Kullanicilar == null)
            {
                return Problem("Entity set 'ApplicationDbContextApp.Kullanicilar'  is null.");
            }
            _context.Kullanicilar.Add(kullanicilar);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getkullanicilar", new { id = kullanicilar.Id }, kullanicilar);
        }

        // DELETE: api/kullanicilars/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletekullanicilar(int id)
        {
            if (_context.Kullanicilar == null)
            {
                return NotFound();
            }
            var kullanicilar = await _context.Kullanicilar.FindAsync(id);
            if (kullanicilar == null)
            {
                return NotFound();
            }

            _context.Kullanicilar.Remove(kullanicilar);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool kullanicilarExists(int id)
        {
            return (_context.Kullanicilar?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
