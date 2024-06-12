using Blindness_API.Models;
using Blindness_API.Models.DTO;
using Blindness_API.Repository.IRepository;
using MagicVilla_VillaAPI.Models.Dto;
using MagicVilla_VillaAPI.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace Blindness_API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;


        private readonly IUserRepository _userRepository;
        protected APIResponse _response;
        public UsersController(IUserRepository userRepository, UserManager<ApplicationUser> userManager, IEmailSender emailSender)
        {
            _userRepository = userRepository;
            this._response = new APIResponse();

            _userManager = userManager;
            _emailSender = emailSender;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO model)
        {
            var LoginResponse = await _userRepository.Login(model);
            if (LoginResponse.User == null || string.IsNullOrEmpty(LoginResponse.Token))
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("UserName or Password is Incorrect");
                return BadRequest(_response);
            }
            _response.StatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;
            _response.Result = LoginResponse;
            return Ok(_response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterationRequestDTO model)
        {
            bool ifUserNameUnique = _userRepository.IsUniqueUser(model.UserName);
            if (!ifUserNameUnique)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("UserName already exists");
                return BadRequest(_response);
            }
            var user = await _userRepository.Register(model);
            if (user == null)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("error while registeration ");
                return BadRequest(_response);

            }
            _response.StatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;
            return Ok(_response);
        }

        [HttpGet("personal-info")]
        [Authorize]
        public IActionResult GetUserInfo()
        {
            var user = this.User;
            if (user == null || !user.Identity.IsAuthenticated)
            {
                return BadRequest();
            }

            var claims = user.Claims.ToList();
          
            return Ok(new
            {
                fullName = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value,
                email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value,


            });
        }
        //[HttpGet("personal-info")]
        //[Authorize]
        //public IActionResult GetPersonalInfo()
        //{
        //    //var userName = User.FindFirstValue(ClaimTypes.Name);
        //    var userName=User.
        //    var userEmail = User.FindFirstValue(ClaimTypes.Name);

        //    if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(userEmail))
        //    {
        //        return Unauthorized();
        //    }

        //    var personalInfo = new
        //    {
        //        Name = userName,
        //        Email = userEmail
        //    };

        //    return Ok(personalInfo);
        //}

        [HttpPost("request-otp")]
        public async Task<IActionResult> RequestOtp([FromBody] OtpRequestModel model)
        {
            if (string.IsNullOrEmpty(model.Email))
            {
                return BadRequest("Email is required.");
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return Ok();
            }

            var otp = new Random().Next(100000, 999999).ToString();
            // Save the OTP and its expiration in a persistent store (like a database)
            // For simplicity, you can use a custom user property
            user.OtpCode = otp;
            user.OtpExpiryTime = DateTime.UtcNow.AddMinutes(10); // OTP valid for 10 minutes
            await _userManager.UpdateAsync(user);

            await _emailSender.SendEmailAsync(user.Email, "Your OTP Code", $"Your OTP code is {otp}");

            return Ok();
        }


        [HttpPost("reset-password-with-otp")]
        public async Task<IActionResult> ResetPasswordWithOtp([FromBody] ResetPasswordWithOtpModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return BadRequest("Invalid request.");
            }

            if (model.Password != model.ConfirmPassword)
            {
                return BadRequest("Passwords do not match.");
            }

            if (user.OtpCode != model.Otp || user.OtpExpiryTime < DateTime.UtcNow)
            {
                return BadRequest("Invalid or expired OTP.");
            }

            var result = await _userManager.ResetPasswordAsync(user, await _userManager.GeneratePasswordResetTokenAsync(user), model.Password);
            if (result.Succeeded)
            {
                user.OtpCode = "";
                user.OtpExpiryTime = null;
                await _userManager.UpdateAsync(user);
                return Ok();
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return BadRequest(ModelState);
        }


    }
}
