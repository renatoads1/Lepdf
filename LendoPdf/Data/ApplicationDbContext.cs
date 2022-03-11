using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore;

namespace LendoPdf.Data
{
    class ApplicationDbContext : DbContext
    {
        public DbSet<Publicacao> Publicacaos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseMySql(@"Server=localhost;database=publicacao;uid=root;pwd=r3n4t0321");

    }
}
