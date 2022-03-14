using DeviceManagementAPI.Interfaces;
using DeviceManagementAPI.Models;
using DeviceManagementAPI.Models.Requests;
using DeviceManagementAPI.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace DeviceManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;

        public AuthController(IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest newUser)
        {
            var findUser = await _userService.GetByEmail(newUser.Email);

            if(findUser != null)
            {
                return BadRequest(new BasicResponse()
                {
                    Success = false,
                    Message = "Email address already used!"
                });
            }

            try
            {
                await _userService.Create(new User()
                {
                    Email = newUser.Email,
                    Password = _authService.GetHashedPassword(newUser.Password),
                    Name = newUser.Name,
                    Role = newUser.Role,
                    Location = newUser.Location,
                    Device = null
                });
                return Ok(new BasicResponse()
                {
                    Success = true,
                    Message = "User registered successfully!"
                });
            }
            catch(Exception e)
            {
                return BadRequest(new BasicResponse()
                {
                    Success = false,
                    Message = e.Message
                });
            }
            
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginData)
        {
            var findUser = await _userService.GetByEmail(loginData.Email);

            if (findUser == null)
            {
                return BadRequest(new BasicResponse()
                {
                    Success = false,
                    Message = "Incorrect email address!"
                });
            }

            bool isCorrectPassword = _authService.VerifyPassword(loginData.Password, findUser.Password);

            if (!isCorrectPassword)
            {
                return BadRequest(new BasicResponse()
                {
                    Success = false,
                    Message = "Incorrect password!"
                });
            }

            string jwtToken = _authService.GenerateToken(findUser);

            return Ok(new AuthenticatedUserResponse()
            {
                Success = true,
                AccessToken = jwtToken
            });

        }

    }
}
