using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BloodManagement.Models;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;

namespace BloodManagement.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BeneficiariesController : ControllerBase
    {
        private readonly DbContext1 _context;

        public BeneficiariesController(DbContext1 context)
        {
            _context = context;
        }

        // GET: api/Beneficiaries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Beneficiary>>> GetBeneficiary()
        {
            return await _context.Beneficiary.ToListAsync();
        }

        // GET: api/Beneficiaries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Beneficiary>> GetBeneficiary(long id)
        {
            var beneficiary = await _context.Beneficiary.FindAsync(id);

            if (beneficiary == null)
            {
                return NotFound();
            }

            return beneficiary;
        }

        // PUT: api/Beneficiaries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBeneficiary(long id, Beneficiary beneficiary)
        {
            if (id != beneficiary.BeneficiaryId)
            {
                return BadRequest();
            }

            _context.Entry(beneficiary).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BeneficiaryExists(id))
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

        // POST: api/Beneficiaries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{id1}/{id2}")]
        public async Task<ActionResult<Beneficiary>> PostBeneficiary(long id1,long id2)
        {
            var user = await _context.User1.FindAsync(id1);
            Beneficiary beneficiary = new Beneficiary();
            beneficiary.User1Id = id1;
            beneficiary.HospitalId = id2;
            if (user == null) { return NotFound("User Not Found With This User Id"); }
            else
            {
                beneficiary.User1 = user;
            }
            var hospital = await _context.Hospital.FindAsync(id2);
            if (hospital == null)
            {
                return NotFound("Hospital Not Found With This Hospital Id");
            }
            else
            {
                beneficiary.Hospital = hospital;
            }
            beneficiary.Date= DateTime.Now;
            _context.Beneficiary.Add(beneficiary);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBeneficiary", new { id = beneficiary.BeneficiaryId }, beneficiary);
        }

        // DELETE: api/Beneficiaries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBeneficiary(long id)
        {
            var beneficiary = await _context.Beneficiary.FindAsync(id);
            if (beneficiary == null)
            {
                return NotFound();
            }

            _context.Beneficiary.Remove(beneficiary);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BeneficiaryExists(long id)
        {
            return _context.Beneficiary.Any(e => e.BeneficiaryId == id);
        }

        [HttpGet("hospital/{city}/{blood_group}")]
        public async Task<List<Hospital>> GetHospital(string city,string blood_group)
        {
            List<Hospital> hospital = null;
            if(blood_group == "A")
            {
                hospital = await _context.Hospital.Where(x => x.city.Contains(city) && x.A).ToListAsync();
            }
            else if (blood_group == "A+")
            {
                hospital = await _context.Hospital.Where(x => x.city.Contains(city) && x.A_positve).ToListAsync();
            }
            else if (blood_group == "B")
            {
                hospital = await _context.Hospital.Where(x => x.city.Contains(city) && x.B).ToListAsync();
            }
            else if (blood_group == "B+")
            {
                hospital = await _context.Hospital.Where(x => x.city.Contains(city) && x.B_positve).ToListAsync();
            }
            else if (blood_group == "C")
            {
                hospital = await _context.Hospital.Where(x => x.city.Contains(city) && x.C).ToListAsync();

            }   
            else if (blood_group == "C+")
            {
                hospital = await _context.Hospital.Where(x => x.city.Contains(city) && x.C_positve).ToListAsync();
            }
            else if (blood_group == "AB")
            {
                hospital = await _context.Hospital.Where(x => x.city.Contains(city) && x.AB).ToListAsync();
            }
            else if (blood_group == "AB+")
            {
                hospital = await _context.Hospital.Where(x => x.city.Contains(city) && x.AB_positve).ToListAsync();
            }
            if (hospital != null) { return hospital; }

            return null;
        }

        [HttpPost("mail/{email1}/{hospitalId}")]
        public async Task<string> Mail(string email1,long hospitalId)
        {
            try
            {
                var hospital = await _context.Hospital.FindAsync(hospitalId);
                if (hospital == null)
                {
                    return "Sorry Can Not Find Hospital";
                }

                string tex = "Hi This Is Vatsal Vasani Greeting From Blood Menagement System I Am Happy To Knowing That We Can Help You " +
                    "I Attach Detail Of Hospital Where You Can Get Your Requested Blood " + hospital.Hospital_Name + " " + hospital.address
                    + " " + hospital.city + " " + hospital.state;
                string sub = "Blood Need";

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Blood Management", "vatsalextra0@gmail.com"));
                message.To.Add(new MailboxAddress("Donor", email1));

                message.Subject = sub;
                message.Body = new TextPart("Plain")
                {
                    Text = tex
                };
                using (var Client = new SmtpClient())
                {
                    Client.Connect("smtp.gmail.com", 587, false);
                    Client.Authenticate("vatsalextra0@gmail.com", "ldmcbkvlmikbgzcm");
                    Client.Send(message);
                    Client.Disconnect(true);

                }
                return "Check Your Email!";
            }
            catch (Exception ex) { return ex.ToString(); }
        }
        [HttpGet("benefit/{id}")]
        public async Task<ActionResult<IEnumerable<Beneficiary>>> GetBeneficiaary(long id)
        {
            return await _context.Beneficiary.Where(x => x.User1Id==id).ToListAsync();
        }
    }
}
