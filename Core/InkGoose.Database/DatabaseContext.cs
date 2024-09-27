using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InkGoose.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InkGoose.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Note> Notes { get; set; }
        public DatabaseContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            // TODO: Get connection details from env.
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=inkgoose;Username=postgres;Password=44435777");
    }
}
