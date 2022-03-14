using DeviceManagementAPI.Interfaces;
using DeviceManagementAPI.Models;
using DeviceManagementAPI.Models.Requests;
using DeviceManagementAPI.Models.Resources;
using DeviceManagementAPI.Models.Responses;
using DeviceManagementAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DeviceManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevicesController : ControllerBase
    {
        private readonly IDeviceService _deviceService;
        private readonly IUserService _userService;

        public DevicesController(IDeviceService deviceService, IUserService userService)
        {
            _deviceService = deviceService;
            _userService = userService;
        }
        // GET: api/<DevicesController>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var devices = await _deviceService.Get();

                return Ok(new DeviceResource() {
                    Devices = devices
                });
            }
            catch(Exception e)
            {
                return BadRequest(new BasicResponse() {
                    Success = false,
                    Message = e.Message
                });
            }
        }

        // GET api/<DevicesController>/5
        [Authorize]
        [HttpGet("{id}")] 
        public async Task<IActionResult> Get(string id)
        {
            var findDevice = await _deviceService.Get(id);

            if(findDevice == null)
            {
                return NotFound(new BasicResponse()
                {
                    Success = false,
                    Message = $"Device with Id = {id} not found!"
                });
            }

            return Ok(new DeviceResource() {
                Devices = new List<Device>() { findDevice }
            });
        }

        // POST api/<DevicesController>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Device device)
        {
            try
            {
                await _deviceService.Create(device);

                return CreatedAtAction(nameof(Post), new BasicResponse() {
                    Success = true,
                    Message = "User created successfully!"
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

        // PUT api/<DevicesController>/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Device device)
        {
            var findDevice = await _deviceService.Get(id);

            if(findDevice == null)
            {
                return NotFound(new BasicResponse()
                {
                    Success = false,
                    Message = $"Device with Id = {id} not found!"
                });
            }

            try
            {
                await _deviceService.Update(id, device);

                return Ok(new BasicResponse()
                {
                    Success = true,
                    Message = "Device updated successfully!"
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

        // DELETE api/<DevicesController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var findDevice = await _deviceService.Get(id);
            
            if(findDevice == null)
            {
                return NotFound(new BasicResponse()
                {
                    Success = false,
                    Message = $"Device with Id = {id} not found!"
                });
            }

            try
            {
                await _deviceService.Delete(id);

                return Ok(new BasicResponse()
                {
                    Success = true,
                    Message = "Device deleted successfully!"
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

        // POST api/<DevicesController>/5
        [Authorize]
        [HttpPost("{id}")]
        public async Task<IActionResult> AssignUser(string id, [FromBody] UserRequest userToBind)
        {
            var findDevice = await _deviceService.Get(id);

            if (findDevice == null)
            {
                return NotFound(new BasicResponse()
                {
                    Success = false,
                    Message = "Device not found!"
                });
            }

            var findUser = await _userService.Get(userToBind.UserId);

            if (findUser == null)
            {
                return NotFound(new BasicResponse()
                {
                    Success = false,
                    Message = "User not found!"
                });
            }

            if (userToBind.RemoveAssignment)
            {
                findDevice.User = null;
            }
            else
            {
                findDevice.User = findUser.Id;
            }

            try
            {
                await _deviceService.Update(findDevice.Id, findDevice);

                return Ok(new BasicResponse()
                {
                    Success = true,
                    Message = "User assigned successfully!"
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
