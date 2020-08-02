using Microsoft.EntityFrameworkCore;
using projetoApiRestCSharp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projetoApiRestCSharp
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Categoria>().HasKey(t => t.id);
            
            modelBuilder.Entity<Produto>().HasKey(t => t.id);
            modelBuilder.Entity<Produto>().HasMany<Categoria>().WithOne();
            
        }

        public DbSet<projetoApiRestCSharp.Models.Categoria> Categoria { get; set; }

        public DbSet<projetoApiRestCSharp.Models.Produto> Produto { get; set; }
    }
}
