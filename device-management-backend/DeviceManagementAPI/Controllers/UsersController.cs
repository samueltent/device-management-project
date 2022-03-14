using DeviceManagementAPI.Interfaces;
using DeviceManagementAPI.Models;
using DeviceManagementAPI.Models.Requests;
using DeviceManagementAPI.Models.Resources;
using DeviceManagementAPI.Models.Responses;
using DeviceManagementAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DeviceManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IDeviceService _deviceService;

        public UsersController(IUserService userService, IDeviceService deviceService)
        {
            _userService = userService;
            _deviceService = deviceService;
        }

        // GET: api/<UsersController>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var users = await _userService.Get();

                return Ok(new UserResource() {
                    Users = users
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

        // GET api/<UsersController>/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var findUser = await _userService.Get(id);

            if(findUser == null)
            {
                return NotFound(new BasicResponse()
                {
                    Success = false,
                    Message = $"User with Id = {id} not found!"
                });
            }

            return Ok(new UserResource()
            {
                Users = new List<User> { findUser }
            });
        }

        // POST api/<UsersController>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User user)
        {

            var findUser = await _userService.GetByEmail(user.Email);

            if(findUser != null)
            {
                return BadRequest(new BasicResponse()
                {
                    Success = false,
                    Message = "Email already registered!"
                });
            }

            try
            {
                await _userService.Create(user);

                return CreatedAtAction(nameof(Post), new BasicResponse()
                {
                    Success = true,
                    Message = "User succesfully registered!"
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

        // PUT api/<UsersController>/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] User user)
        {
            var findUser = await _userService.Get(id);

            if (findUser == null)
            {
                return NotFound(new BasicResponse()
                {
                    Success = false,
                    Message = $"User with Id = {id} not found!"
                });
            }

            try
            {
                await _userService.Update(id, user);

                return Ok(new BasicResponse(){
                    Success = true,
                    Message = "User updated successfully"
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

        // DELETE api/<UsersController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var findUser = await _userService.Get(id);

            if (findUser == null)
            {
                return NotFound(new BasicResponse()
                {
                    Success = false,
                    Message = $"User with Id = {id} not found!"
                });
            }

            try
            {
                await _userService.Delete(id);

                return Ok(new BasicResponse()
                {
                    Success = true,
                    Message = "User deleted successfully!"
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

        // POST api/<UsersController>/5
        [Authorize]
        [HttpPost("{id}")]
        public async Task<IActionResult> AssignDevice(string id, [FromBody] DeviceRequest deviceToBind)
        {
            var findUser = await _userService.Get(id);

            if (findUser == null)
            {
                return NotFound(new BasicResponse()
                {
                    Success = false,
                    Message = "User not found!"
                });
            }

            var findDevice = await _deviceService.Get(deviceToBind.DeviceId);

            if(findDevice == null)
            {
                return NotFound(new BasicResponse()
                {
                    Success = false,
                    Message = "Device not found!"
                });
            }

            if(deviceToBind.RemoveAssignment)
            {
                findUser.Device = null;
            }
            else
            {
                findUser.Device = findDevice.Id;
            }

            try
            {
                await _userService.Update(findUser.Id, findUser);

                return Ok(new BasicResponse()
                {
                    Success = true,
                    Message = "Device assigned successfully!"
                });
            }
            catch (Exception e)
            {
                return BadRequest(new BasicResponse()
                {
                    Success = false,
                    Message = e.Message
                });
            }
        }
    }
}
