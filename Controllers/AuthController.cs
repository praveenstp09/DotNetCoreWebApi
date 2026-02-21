using FirstWebAPI.Config;
using FirstWebAPI.DTO;
using FirstWebAPI.Models;
using FirstWebAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FirstWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly FirstWebAPIDbContext _dbContext;
        private readonly JwtService _jwtService;
        public AuthController(FirstWebAPIDbContext dbContext, JwtService jwtService)
        {
            _dbContext = dbContext;
            _jwtService = jwtService;
        }
        [HttpPost("login")]
        public ActionResult Login()
        {
            return Ok("Login");
        }


        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterUserDTO dto)
        {
            if (!ModelState.IsValid) { 
                return BadRequest("Data Validation Error");
            }
            if (dto == null) {
                return BadRequest();
            }
            var ExistUser = await _dbContext.Users.FirstOrDefaultAsync(user => user.Email == dto.Email);


            if (ExistUser != null) {
                return BadRequest("User Already Exist");
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var validateRoleIds=await _dbContext.Roles.Where(r => dto.RoleIds.Contains(r.Id)).Select(r=>r.Id).ToListAsync();
            if(validateRoleIds.Count != dto.RoleIds.Count) {
                return BadRequest("One of Role Id is inValid");
            }

            UserModel newUser = new UserModel
            {
                Name= dto.Name,
                Email= dto.Email,
                Password= hashedPassword,
                Age= dto.Age,
                //CreatedAt= DateTime.UtcNow,
                //UpdatedAt= DateTime.UtcNow,
                UserRoles=dto.RoleIds.Select(rid=> new UserRoleModel {RoleId=rid}).ToList(),
            };
            await _dbContext.Users.AddAsync(newUser);
            _dbContext.SaveChanges();
            var token = _jwtService.GenerateToken(dto);
            return Ok(new {token});
        }
    }
}
