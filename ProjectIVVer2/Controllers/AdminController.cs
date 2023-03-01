using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectIVVer2.Model;
using ProjectIVVer2.Repository;
using ProjectIVVer2.Services;

namespace ProjectIVVer2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private IAdmin service;

        public AdminController(Repository.IAdmin _service)
        {
            service = _service;
        }

        [HttpGet("{username}/{password}")]
        public async Task<Admin> Login(string username, string password)
        {
           
            return await service.LoginAsync(username, password);

            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Admin>> Get(int id)
        {
            var admin = await service.GetByIdAsync(id);

            if (admin == null)
            {
                return NotFound();
            }

            return admin;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Admin>>> GetAll()
        {
            var admins = await service.GetAllAsync();
            return Ok(admins);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Admin admin)
        {
            var result = await service.CreateAsync(admin);

            if (result)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        //[HttpDelete("{username}")]
        //public async Task<ActionResult> Delete(string username)
        //{
        //    var result = await service.DeleteAdmin(username);

        //    if (result)
        //    {
        //        return Ok();
        //    }

        //    return NotFound();
        //}

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] Admin admin)
        {
            var result = await service.UpdateAsync(id, admin);

            if (result)
            {
                return Ok();
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await service.DeleteAsync(id);

            if (result)
            {
                return Ok();
            }

            return NotFound();
        }
    }
}
