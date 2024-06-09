using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmacyApp.Domain.Entities;
using PharmacyApp.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PharmacyApp.Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IDataStorage<User> _userStorage;

        public UsersController(IDataStorage<User> userStorage)
        {
            _userStorage = userStorage;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userStorage.GetAllAsync();
            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            await _userStorage.AddAsync(user);
            return CreatedAtAction(nameof(GetUsers), new { id = user.Id }, user);
        }
    }
}
