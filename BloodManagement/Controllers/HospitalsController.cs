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
    public class HospitalsController : ControllerBase
    {
        private readonly DbContext1 _context;

        public HospitalsController(DbContext1 context)
        {
            _context = context;
        }

        // GET: api/Hospitals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hospital>>> GetHospital()
        {
            return await _context.Hospital.ToListAsync();
        }

        // GET: api/Hospitals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Hospital>> GetHospital(long id)
        {
            var hospital = await _context.Hospital.FindAsync(id);

            if (hospital == null)
            {
                return NotFound();
            }

            return hospital;
        }

        // PUT: api/Hospitals/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHospital(long id, Hospital hospital)
        {
            if (id != hospital.HospitalId)
            {
                return BadRequest();
            }

            _context.Entry(hospital).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HospitalExists(id))
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

        // POST: api/Hospitals
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Hospital>> PostHospital(Hospital hospital)
        {
            if (_context.User1.Any(e => e.Email == hospital.Email))
            {
                return BadRequest("User Already Exist With This Email Id");
            }
            else
            {
                _context.Hospital.Add(hospital);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetHospital", new { id = hospital.HospitalId }, hospital);
            }
            
        }

        // DELETE: api/Hospitals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHospital(long id)
        {
            var hospital = await _context.Hospital.FindAsync(id);
            if (hospital == null)
            {
                return NotFound();
            }

            _context.Hospital.Remove(hospital);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HospitalExists(long id)
        {
            return _context.Hospital.Any(e => e.HospitalId == id);
        }

        [HttpGet("/hemail/{email}")]
        public async Task<ActionResult<long>> GetUser1(string email)
        {
            if (_context.Hospital.Any(e => e.Email == email))
            {
                var user = await _context.Hospital.Where(e => e.Email == email).ToListAsync();

                return user[0].HospitalId;
            }
            else
            {
                return NoContent();
            }

        }

    }
}
