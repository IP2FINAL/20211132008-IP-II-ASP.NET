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
    public class newsController : ControllerBase
    {
        private readonly ApplicationDbContextApp _context;

        public newsController(ApplicationDbContextApp context)
        {
            _context = context;
        }

        // GET: api/news
        [HttpGet]
        public async Task<ActionResult<IEnumerable<news>>> Getnews()
        {
            if (_context.news == null)
            {
                return NotFound();
            }
            return await _context.news.ToListAsync();
        }

        // GET: api/news/5
        [HttpGet("{id}")]
        public async Task<ActionResult<news>> Getnews(int id)
        {
            if (_context.news == null)
            {
                return NotFound();
            }
            var news = await _context.news.FindAsync(id);

            if (news == null)
            {
                return NotFound();
            }

            return news;
        }

        // PUT: api/news/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putnews(int id, news news)
        {
            if (id != news.id)
            {
                return BadRequest();
            }

            _context.Entry(news).State = EntityState.Modified;

            try
            {
                var ara = _context.kategori_ara.Where(i => i.news_Id == id).FirstOrDefault();
                if (ara != null)
                {
                    ara.kategori_Id = id;
                }
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!newsExists(id))
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

        // POST: api/news
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<news>> Postnews(news news)
        {
            if (_context.news == null)
            {
                return Problem("Entity set 'ApplicationDbContextApp.news'  is null.");
            }
            news newdata = new news();
            newdata.newsCategoryId = news.newsCategoryId;
            newdata.newsDetail = news.newsDetail;
            newdata.newsPhoto = news.newsPhoto;
            newdata.newsTitle = news.newsTitle;
            _context.news.Add(newdata);
            _context.SaveChanges();

            kategori_ara ara = new kategori_ara();
            ara.news_Id = news.id;
            ara.kategori_Id = news.newsCategoryId;
            _context.kategori_ara.Add(ara);
            _context.SaveChanges();

            return CreatedAtAction("Getnews", new { id = news.id }, news);
        }

        // DELETE: api/news/5  
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletenews(int id)
        {
            if (_context.news == null)
            {
                return NotFound();
            }
            var news = await _context.news.FindAsync(id);
            if (news == null)
            {
                return NotFound();
            }

            _context.news.Remove(news);

            var delete_ara_ = _context.kategori_ara.Where(i => i.news_Id == news.id);
            _context.kategori_ara.RemoveRange(delete_ara_);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool newsExists(int id)
        {
            return (_context.news?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
