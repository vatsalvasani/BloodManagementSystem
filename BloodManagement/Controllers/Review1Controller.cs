using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BloodManagement.Models;
using Microsoft.AspNetCore.Authorization;

namespace BloodManagement.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class Review1Controller : ControllerBase
    {
        private readonly DbContext1 _context;

        public Review1Controller(DbContext1 context)
        {
            _context = context;
        }

        // GET: api/Review1
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Review1>>> GetReview1()
        {
            return await _context.Review1.ToListAsync();
        }

        // GET: api/Review1/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Review1>> GetReview1(long id)
        {
            var review1 = await _context.Review1.FindAsync(id);

            if (review1 == null)
            {
                return NotFound();
            }

            return review1;
        }

        // PUT: api/Review1/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReview1(long id, Review1 review1)
        {
            if (id != review1.Review1Id)
            {
                return BadRequest();
            }

            _context.Entry(review1).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Review1Exists(id))
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

        // POST: api/Review1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("review/{id}/{description}")]
        public async Task<ActionResult<Review1>> PostReview1(long id,string description)
        {
            Review1 review = new Review1();
            review.User1Id = id;
            review.Description = description;

            var user = await _context.User1.FindAsync(id);
            if (user == null) { return NotFound("User Not Found With This User Id"); }
            review.User1 = user;
            _context.Review1.Add(review);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReview1", new { id = review.Review1Id }, review);
        }
        // DELETE: api/Review1/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview1(long id)
        {
            var review1 = await _context.Review1.FindAsync(id);
            if (review1 == null)
            {
                return NotFound();
            }

            _context.Review1.Remove(review1);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Review1Exists(long id)
        {
            return _context.Review1.Any(e => e.Review1Id == id);
        }

        [HttpGet("getreview/{id}")]
        public async Task<List<Review1>> GetMyReview(long id)
        {
            if (await _context.Review1.Where(x => x.User1Id == id).ToListAsync() != null)
            {
                return await _context.Review1.Where(x => x.User1Id == id).ToListAsync();
            }
            else
            {
                return null;
            }
        }

        [HttpPut("review/{id}/{description}")]
        public async Task<IActionResult> GetMyReview(long id,string description)
        {
            var review = await _context.Review1.FindAsync(id);

            if (review == null)
            {
                return BadRequest();
            }
            review.Description = description;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Review1Exists(id))
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
    }
}
