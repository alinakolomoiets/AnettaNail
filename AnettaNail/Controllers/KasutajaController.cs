using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AnettaNail.Data;
using AnettaNail.Models;
using BCrypt.Net;

namespace AnettaNail.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KasutajaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public KasutajaController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("user/register")]
        public IActionResult RegisterUser([FromBody] Kasutajad kasutajad)
        {
            try
            {
                if (_context.Kasutajad.Any(k => k.Nimi == kasutajad.Nimi))
                {
                    return BadRequest(new { Message = "Selle sisselogimisega kasutaja on juba olemas" });
                }

                // Хэшируем пароль
                kasutajad.Parool = BCrypt.Net.BCrypt.HashPassword(kasutajad.Parool);

                // Проверяем, существует ли роль с указанным RollId
                var existingRole = _context.Roll.FirstOrDefault(r => r.RollId == 3); // Роль с id 3
                if (existingRole == null)
                {
                    return BadRequest(new { Message = "Указанная роль не существует" });
                }

                // Устанавливаем RollId
                kasutajad.RollId = existingRole.RollId;

                _context.Kasutajad.Add(kasutajad);
                _context.SaveChanges();

                return Ok(new { Message = "Registreerimine edukas" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Произошла внутренняя ошибка сервера" });
            }
        }



        [HttpPost("login")]
        public IActionResult Login([FromBody] Kasutajad kasutajad)
        {
            try
            {
                var existingUser = _context.Kasutajad.Include(k => k.Roll).FirstOrDefault(k => k.Nimi == kasutajad.Nimi);

                if (existingUser != null && BCrypt.Net.BCrypt.Verify(kasutajad.Parool, existingUser.Parool))
                {
                    // Проверка роли пользователя
                    if (existingUser.Roll != null && existingUser.Roll.Nimi == kasutajad.Nimi)
                    {
                        return Ok(new { Message = "Autentimine õnnestus" });
                    }
                    else
                    {
                        return Unauthorized(new { Message = "Teil puuduvad õigused sisse logida" });
                    }
                }
                else
                {
                    return Unauthorized(new { Message = "Vale sisselogimine või salasõna" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Autentimisviga: {ex.Message}" });
            }
        }
    }
}
