using BloodManagement.Data;
using BloodManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace BloodManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HAuthController : ControllerBase
    {
           private readonly HIAuthRepo _authRepo;
            public HAuthController(HIAuthRepo authRepo)
            {
                _authRepo = authRepo;
            }
            [HttpPost("Register")]
            public async Task<ActionResult> Register(HospitalRegisterDTO user)
            {
            var res = await _authRepo.Register(new Hospital()
            {
                Hospital_Name = user.Hospital_Name,
                Email = user.Email,
                contat_no = user.contat_no,
                address = user.address,
                city = user.city,
                state = user.state,
                A = user.A,
                A_positve = user.A_positve,
                B= user.B,
                B_positve= user.B_positve,
                C= user.C,
                C_positve= user.C_positve,
                AB= user.AB,
                AB_positve= user.AB_positve,
            }, user.Password); ;
                if (res == 0)
                {
                    return BadRequest($"Can not register {user.Hospital_Name}");

                }
                return Ok($"User Registered Successfully");
            }
            [HttpPost("login")]
            public async Task<ActionResult> Login(HospitalLoginDTO userDTO)
            {
                var res = await _authRepo.Login(userDTO.Email, userDTO.Password);
                if (res == null)
                {
                    return BadRequest($"UserName Or Password Is Incorrect");
                }
                return Ok(res);
            }
        }
    }

