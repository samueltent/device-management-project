using DeviceManagementAPI.Models;
using DeviceManagementAPI.Services;
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

        public DevicesController(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }
        // GET: api/<DevicesController>
        [HttpGet]
        public async Task<IEnumerable<Device>> Get()
        {
            return await _deviceService.Get();
        }

        // GET api/<DevicesController>/5
        [HttpGet("{id}")] 
        public async Task<IActionResult> Get(string id)
        {
            var findDevice = await _deviceService.Get(id);

            if(findDevice == null)
            {
                return NotFound($"Device with Id = {id} not found!");
            }

            return Ok(findDevice);
        }

        // POST api/<DevicesController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Device device)
        {
            try
            {
                await _deviceService.Create(device);
                return CreatedAtAction(nameof(Get), device);
            }
            catch(Exception e)
            {
                return BadRequest("Device could not be created!");
            }
        }

        // PUT api/<DevicesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Device device)
        {
            var findDevice = await _deviceService.Get(id);

            if(findDevice == null)
            {
                return NotFound($"Device with Id = {id} not found!");
            }

            try
            {
                await _deviceService.Update(id, device);
                return Ok("Device updated successfully!");
            }
            catch(Exception e)
            {
                return BadRequest("Device update failed!");
            }
        }

        // DELETE api/<DevicesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var findDevice = await _deviceService.Get(id);
            
            if(findDevice == null)
            {
                return NotFound($"Device with Id = {id} not found!");
            }

            try
            {
                await _deviceService.Delete(id);
                return Ok("Device deleted successfully!");
            }
            catch(Exception e)
            {
                return BadRequest("Device could not be deleted!");
            }
        }
    }
}
