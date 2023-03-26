using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BloodManagement.Models;
using System.Collections;
using System.ComponentModel;
using System.Collections.Specialized;
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.AspNetCore.Authorization;

namespace BloodManagement.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DonorsController : ControllerBase
    {
        private readonly DbContext1 _context;

        public DonorsController(DbContext1 context)
        {
            _context = context;
        }

        // GET: api/Donors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Donor>>> GetDonor()
        {
            return await _context.Donor.ToListAsync();
        }

        // GET: api/Donors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Donor>> GetDonor(long id)
        {
            var donor = await _context.Donor.FindAsync(id);

            if (donor == null)
            {
                return NotFound();
            }

            return donor;
        }

        // PUT: api/Donors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDonor(long id, Donor donor)
        {
            if (id != donor.DonorId)
            {
                return BadRequest();
            }

            _context.Entry(donor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DonorExists(id))
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

        // POST: api/Donors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{id1}/{id2}")]
        public async Task<ActionResult<Donor>> PostDonor(long id1,long id2)
        {
            Donor donor = new Donor();
            donor.User1Id = id1;
            donor.HospitalId = id2;
            var user = await _context.User1.FindAsync(id1);
            if (user == null) { return NotFound("User Not Found With This User Id"); }
            else
            {
                donor.User1= user;
            }
            var hospital = await _context.Hospital.FindAsync(id2);
            if(hospital == null)
            {
                return NotFound("Hospital Not Found With This Hospital Id");
            }
            else
            {
                donor.Hospital = hospital;
            }
            donor.DonationDate= DateTime.Now;

            _context.Donor.Add(donor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDonor", new { id = donor.DonorId }, donor);
        }

        // DELETE: api/Donors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDonor(long id)
        {
            var donor = await _context.Donor.FindAsync(id);
            if (donor == null)
            {
                return NotFound();
            }

            _context.Donor.Remove(donor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DonorExists(long id)
        {
            return _context.Donor.Any(e => e.DonorId == id);
        }

        //[HttpGet("hospital")]
        //public async Task<List<Hospital>> GetHospital(string city)
        //{
            //var hospital =  await _context.Hospital.Where(x => x.city.Contains(city)).ToListAsync();
          //  return hospital;
        //}
        [HttpGet("hospital/{city}/{blood_group}")]
        public async Task<List<Hospital>> GetHospital(string city, string blood_group)
        {
            List<Hospital> hospital =  null;
            if (blood_group == "A")
            {
                hospital = await _context.Hospital.Where(x => x.city.Contains(city) && !x.A).ToListAsync();
            }
            else if (blood_group == "A+")
            {
                hospital = await _context.Hospital.Where(x => x.city.Contains(city) && x.A_positve==false).ToListAsync();
            }
            else if (blood_group == "B")
            {
                hospital = await _context.Hospital.Where(x => x.city.Contains(city) && x.B == false).ToListAsync();
            }
            else if (blood_group == "B+")
            {
                hospital = await _context.Hospital.Where(x => x.city.Contains(city) && x.B_positve == false).ToListAsync();
            }
            else if (blood_group == "C")
            {
                hospital = await _context.Hospital.Where(x => x.city.Contains(city) && x.C == false).ToListAsync();
            }
            else if (blood_group == "C+")
            {
                hospital = await _context.Hospital.Where(x => x.city.Contains(city) && x.C_positve == false).ToListAsync();
            }
            else if (blood_group == "AB")
            {
                hospital = await _context.Hospital.Where(x => x.city.Contains(city) && x.AB == false).ToListAsync();
            }
            else if (blood_group == "AB+")
            {
                hospital = await _context.Hospital.Where(x => x.city.Contains(city) && x.AB_positve == false).ToListAsync();
            }
            if (hospital.Count == 0) { hospital = await _context.Hospital.ToListAsync(); }
            return hospital;
        }

        [HttpPost("mail/{email}/{hospitalid}")]
        public async Task<string> Mail(string email,long hospitalid)
        {
            try
            {
                var hospital = await _context.Hospital.FindAsync(hospitalid);

                if (hospital == null)
                {
                    return "Sorry Can Not Find Hospital";
                }

                string tex = "Hi This Is Vatsal Vasani Greeting From Blood Menagement System I Am Happy Hearing That You Want To Contribute Towards " +
                    "The Society Here I Attach Detail Of Hospital Where You Can Donate Your Blood " + hospital.Hospital_Name + " " + hospital.address
                    + " " + hospital.city + " " + hospital.state;
                string sub = "Blood Donation";

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Blood Management", "vatsalextra0@gmail.com"));
                message.To.Add(new MailboxAddress("Donor", email));

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
            catch (Exception ex) { return "Try Again!"; }
        }
        [HttpGet("donate/{id}")]
        public async Task<ActionResult<IEnumerable<Donor>>> GetBeneficiaary(long id)
        {
            return await _context.Donor.Where(x => x.User1Id == id).ToListAsync();
        }
    }
}
