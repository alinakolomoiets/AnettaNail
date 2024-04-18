using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AnettaNail.Data;
using AnettaNail.Models;
using BCrypt.Net;
using System.Xml.Linq;

namespace AnettaNail.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RollController
    {
        private readonly ApplicationDbContext _context;
        public RollController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public List<Roll> GetRoll()
        {
            var roll = _context.Roll.ToList();
            return roll;
        }
        [HttpDelete("delete/{id}")]
        public List<Roll> DeleteProduct(int id)
        {
            var toode = _context.Roll.Find(id);

            if (toode == null)
            {
                return _context.Roll.ToList();
            }

            _context.Roll.Remove(toode);
            _context.SaveChanges();
            return _context.Roll.ToList();
        }
    }
}
