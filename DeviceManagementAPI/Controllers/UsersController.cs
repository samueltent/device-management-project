using DeviceManagementAPI.Interfaces;
using DeviceManagementAPI.Models;
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
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/<UsersController>
        [HttpGet]
        public async Task<IEnumerable<User>> Get()
        {
            return await _userService.Get();
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var findUser = await _userService.Get(id);

            if(findUser == null)
            {
                return NotFound($"User with Id = {id} not found!");
            }

            return Ok(findUser);
        }

        // POST api/<UsersController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User user)
        {
            try
            {
                await _userService.Create(user);
                return CreatedAtAction(nameof(Get), user);
            }
            catch(Exception e)
            {
                return BadRequest("User could not be created!");
            }
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] User user)
        {
            var findUser = await _userService.Get(id);

            if (findUser == null)
            {
                return NotFound($"User with Id = {id} not found!");
            }

            try
            {
                await _userService.Update(id, user);
                return Ok("User updated successfully");
            }
            catch(Exception e)
            {
                return BadRequest("User update failed!");
            }
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var findUser = await _userService.Get(id);

            if (findUser == null)
            {
                return NotFound($"User with Id = {id} not found!");
            }

            try
            {
                await _userService.Delete(id);
                return Ok("User deleted successfully!");
            }
            catch(Exception e)
            {
                return BadRequest("User could not be deleted!");
            }
        }
    }
}
