using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Film.Entity
{
    public class FilmContext : DbContext
    {
        public FilmContext(DbContextOptions<FilmContext> options)
            : base(options)
        {
        }
        public DbSet<Film> Film { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var produtoEntityAssembly = AppDomain.CurrentDomain
                .GetAssemblies()
                .Where(a => a.FullName.Contains("Film.Entity"))
                .FirstOrDefault();

            modelBuilder.ApplyConfigurationsFromAssembly(produtoEntityAssembly);

            base.OnModelCreating(modelBuilder);
        }

    }
}
