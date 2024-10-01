﻿using InkGoose.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InkGoose.Notes.Api.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Note> Notes { get; set; }
        public DbSet<User> Users { get; set; }
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
