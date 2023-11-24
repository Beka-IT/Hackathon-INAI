using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Entities;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _db;
        public UsersController(AppDbContext context)
        {
             _db = context;
        }

        [HttpPost]
        public IActionResult SignIn(UserSignInRequest req)
        {
            var user = _db.Users
                .FirstOrDefault(x => x.Pin == req.Pin && x.Password == req.Password);

            return user is null ? Unauthorized() : Ok(user);
        }

        [HttpPost]
        public IActionResult SignUp(SignUpRequest req)
        {
            var user = _db.Users.FirstOrDefault(x => x.Pin == req.Pin);

            if(user is not null)
            {
                return BadRequest("Пользователь с таким ПИНом уже существует!");
            }

            var newUser = new User
            {
                Pin = req.Pin,
                Fullname = req.Fullname,
                Password = req.Password,
                Role = Enums.RoleType.Client
            };

            _db.Add(newUser);
            _db.SaveChanges();

            return Ok();
        }
    }
}
