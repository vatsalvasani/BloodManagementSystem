using Microsoft.AspNetCore.Mvc;
using BloodManagement.Data;
using BloodManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace BloodManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepo _authRepo;
        public AuthController(IAuthRepo authRepo)
        {
            _authRepo = authRepo;
        }
        [HttpPost("Register")]
        public async Task<ActionResult> Register(UserRegisterDTO user)
        {
            var res = await _authRepo.Register(new User1() { 
                User_Name = user.User_Name,
                Contact_no = user.Contact_no,
                Address= user.Address,
                Blood_type= user.Blood_type,
                State= user.State,
                City= user.City,
                Email = user.Email }, user.Password);
            if(res == 0)
            {
                return BadRequest($"Can not register {user.User_Name}" );

            }
            return Ok($"User Registered Successfully");
        }
        [HttpPost("login")]
        public async Task<ActionResult> Login(UserLoginDTO userDTO)
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
