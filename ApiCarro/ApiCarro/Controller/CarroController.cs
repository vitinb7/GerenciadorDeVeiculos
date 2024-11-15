using ApiCarro.Attributes;
using ApiCarro.Data;
using ApiCarro.Models;
using ApiCarro.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCarro.Controller
{
    [ApiKey]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [Authorize(Roles = "Cliente, Administrador")]
        [HttpGet("carro")]
        public async Task<IActionResult> Get(
            [FromServices] AppDbContext context)
        {
            try
            {
                var todoCarro = await context.carros.ToListAsync();
                return Ok(todoCarro);
            }
            catch
            {
                return StatusCode(500, new { message = "Falha interna no servidor" });
            }
        }
        [Authorize(Roles = "Cliente, Administrador")]
        [HttpGet("carro/{id:int}")]
        public async Task<IActionResult> GetPorId(int id,
            [FromServices] AppDbContext context)
        {
            try
            {
                var umCarro = await context.carros.FindAsync(id);
                return Ok(umCarro);
            }
            catch
            {
                return StatusCode(500, new { message = "Falha interna no servidor" });
            }
        }
        [Authorize(Roles = "Administrador")]
        [HttpPost("carro")]
        public async Task<IActionResult> Post(
            [FromBody] CarroViewModel model,
            [FromServices] AppDbContext context)
        {
            try
            {
                var umCarro = new Carro
                {
                    Marca = model.Marca,
                    Modelo = model.Modelo,
                    Ano = model.Ano
                };

                await context.carros.AddAsync(umCarro);
                await context.SaveChangesAsync();

                return Created($"/{umCarro.Id}", umCarro);
            }
            catch
            {
                return StatusCode(500, new { message = "Falha interna no servidor" });
            }
        }
        [Authorize(Roles = "Administrador")]
        [HttpPut("carro/{id:int}")]
        public async Task<IActionResult> Put(int id,
        [FromBody] CarroViewModel model,
        [FromServices] AppDbContext context)
        {
            try
            {
                var carroVelho = await context.carros.FindAsync(id);
                if (carroVelho == null)
                {
                    return NotFound();
                }

                carroVelho.Marca = model.Marca;
                carroVelho.Modelo = model.Modelo;
                carroVelho.Ano = model.Ano;

                await context.SaveChangesAsync();

                return NoContent();
            }
            catch
            {
                return StatusCode(500, new { message = "Falha interna no servidor" });
            }
        }
        [Authorize(Roles = "Administrador")]
        [HttpDelete("carro/{id:int}")]
        public async Task<IActionResult> Delete(int id,
            [FromServices] AppDbContext context)
        {
            try
            {
                var carroVelho = await context.carros.FindAsync(id);
                if (carroVelho == null)
                {
                    return NotFound();
                }

                context.carros.Remove(carroVelho);
                await context.SaveChangesAsync();

                return NoContent();
            }
            catch
            {
                return StatusCode(500, new { message = "Falha interna no servidor" });
            }
        }
    }
}