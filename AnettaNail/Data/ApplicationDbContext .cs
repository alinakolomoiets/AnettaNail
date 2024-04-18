using Microsoft.EntityFrameworkCore;
using AnettaNail.Models;
using System.Collections.Generic;

namespace AnettaNail.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Kasutajad> Kasutajad { get; set; }
        public DbSet<Roll> Roll { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
    }
}