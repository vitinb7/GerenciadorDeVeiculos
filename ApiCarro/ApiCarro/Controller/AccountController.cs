using ApiCarro.Data;
using ApiCarro.Models;
using ApiCarro.Services;
using ApiCarro.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;


namespace ApiCarro.Controller
{
    [ApiController]
    public class AccountController : ControllerBase
    {
        [HttpPost("account/login")]
        public IActionResult Login(
            [FromBody] UserLoginViewModel model,
            [FromServices] AppDbContext context,
            [FromServices] TokenService tokenService)
        {
            var user = context.Users.FirstOrDefault(x => x.Usuario == model.Usuario);

            if (user == null)
                return StatusCode(401, new { message = "Usuário ou senha inválidos" });

            if (Settings.GenerateHash(model.Password) != user.Password)
                return StatusCode(401, new { message = "Usuário ou senha inválidos" });

            try
            {
                var token = tokenService.CreateToken(user);
                return Ok(new { token = token });
            }
            catch
            {
                return StatusCode(500, new { message = "Falha interna no servidor" });
            }
        }


        [HttpPost("account/signup")]
        public IActionResult Signup(
            [FromBody] UserSignupViewModel model,
            [FromServices] AppDbContext context)
        {
            var user = context.Users.FirstOrDefault(x => x.Usuario == model.Usuario);

            if (user != null)
                return StatusCode(401, new { message = "Nome de usuário já cadastrado" });

            try
            {
                var userNew = new User
                {
                    Usuario = model.Usuario,
                    Password = Settings.GenerateHash(model.Password),
                    Role = model.Role
                };

                context.Users.Add(userNew);
                context.SaveChanges();

                return Ok(new { userId = userNew.Id });
            }
            catch
            {
                return StatusCode(500, new { message = "Falha interna no servidor" });
            }
        }
        [Authorize(Roles = "Administrador")]
        [HttpGet("account/user")]
        public IActionResult Get(
            [FromServices] AppDbContext context)
        {
            try
            {
                var users = context.Users.ToList().Select(x => new UserReturnViewModel
                {
                    Id = x.Id,
                    Usuario = x.Usuario,
                    Role = x.Role,
                    Password = x.Password
                });

                return Ok(users);
            }
            catch
            {
                return StatusCode(500, new { message = "Falha interna no servidor" });
            }
        }
    }
}
