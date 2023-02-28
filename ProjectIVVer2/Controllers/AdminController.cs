using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectIVVer2.Model;
using ProjectIVVer2.Services;

namespace ProjectIVVer2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly AdminService _adminService;

        public AdminController(AdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet("{username}/{password}")]
        public async Task<ActionResult> Login(string username, string password)
        {
            var admin = await _adminService.LoginAsync(username, password);

            if (admin == null)
            {
                return Unauthorized();
            }

            return Ok(admin);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Admin>> Get(int id)
        {
            var admin = await _adminService.GetByIdAsync(id);

            if (admin == null)
            {
                return NotFound();
            }

            return admin;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Admin>>> GetAll()
        {
            var admins = await _adminService.GetAllAsync();
            return Ok(admins);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Admin admin)
        {
            var result = await _adminService.CreateAsync(admin);

            if (result)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete("{username}")]
        public async Task<ActionResult> Delete(string username)
        {
            var result = await _adminService.DeleteAdmin(username);

            if (result)
            {
                return Ok();
            }

            return NotFound();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] Admin admin)
        {
            var result = await _adminService.UpdateAsync(id, admin);

            if (result)
            {
                return Ok();
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _adminService.DeleteAsync(id);

            if (result)
            {
                return Ok();
            }

            return NotFound();
        }
    }
}
