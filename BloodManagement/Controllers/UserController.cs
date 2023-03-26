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
    public class UserController : ControllerBase
    {
        private readonly DbContext1 _context;

        public UserController(DbContext1 context)
        {
            _context = context;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User1>>> GetUser()
        {
            return await _context.User1.ToListAsync();
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User1>> GetUser(long id)
        {
            var user = await _context.User1.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/User/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(long id, User1 user)
        {
            if (id != user.User1Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/User
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User1>> PostUser(User1 user)
        {
            if (_context.User1.Any(e => e.Email == user.Email))
            {
                return BadRequest("User Already Exist With This Email Id");
            }
            else
            {
                _context.User1.Add(user);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetUser", new { id = user.User1Id }, user);
            }

        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            var user = await _context.User1.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.User1.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(long id)
        {
            return _context.User1.Any(e => e.User1Id == id);
        }
        [HttpGet("/email/{email}")]
        public async Task<ActionResult<long>> GetUser1(string email)
        {
            if (_context.User1.Any(e => e.Email == email))
            {
                var user = await _context.User1.Where(e => e.Email == email).ToListAsync();

                return user[0].User1Id;
            }
            else
            {
                return NoContent();
            }

        }



    }
}
