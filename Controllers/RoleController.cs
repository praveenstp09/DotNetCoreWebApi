using FirstWebAPI.Config;
using FirstWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirstWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly FirstWebAPIDbContext _Context;

        public RoleController(FirstWebAPIDbContext context)
        {
            _Context = context;
        }

        [HttpGet]
        public ActionResult GetAllRoles()
        {
            var roles = _Context.Roles.ToList();
            return Ok(roles);
        }

        [HttpPost]
        public ActionResult Create([FromBody] RoleModel role)
        {
            if (role == null)
            {
                return BadRequest("Role data is null.");
            }
            RoleModel newRole = new RoleModel
            {
                Name = role.Name
            };
            _Context.Roles.Add(newRole);
            _Context.SaveChanges();
            return Ok("Yes");
        }


    }
}
