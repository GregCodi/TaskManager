using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.API.Models;
using TaskManager.API.Models.Data;
using TaskManager.API.Models.Services;
using TaskManager.Common.Models;

namespace TaskManager.API.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationContext _db;
        private readonly UserService _userService;
        public UsersController(ApplicationContext db)
        {
            _db = db;
            _userService = new UserService(db);
        }

        [HttpGet("test")]
        [AllowAnonymous]
        public IActionResult TestApi()
        {
            return Ok("API WORK!");  
        }

        [HttpPost("create")]
        public IActionResult CreateUser([FromBody] UserModel userModel)
        {
            if(userModel != null)
            {
                bool result = _userService.Create(userModel);
                return result ? Ok() : NotFound();
            }
            return BadRequest();
        }

        [HttpPatch("update/{id}")]
        public IActionResult UpdateUser(int id, [FromBody] UserModel userModel)
        {
            if (userModel != null)
            {
                bool result = _userService.Update(id, userModel);
                return result ? Ok() : NotFound();
            }
            return BadRequest();
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteUser(int id)
        {
            bool result = _userService.Delete(id);
            return result ? Ok() : NotFound();
        }

        [HttpGet]
        public async Task<IEnumerable<UserModel>> GetUsers()
        {
            return await _db.Users.Select(u => u.ToDto()).ToListAsync();
        }

        [HttpPost("create/all")]
        public async Task<IActionResult> CreateMultipleUsers([FromBody] List<UserModel> userModels)
        {
            if (userModels != null && userModels.Count > 0)
            {
                bool result = _userService.CreateMultipleUsers(userModels);
                return result ? Ok() : NotFound(); ;
            }
            return BadRequest();
        }
    }
}
