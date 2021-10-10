using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Film.Infra
{
    public class FilmContext : DbContext
    {
        public FilmContext(DbContextOptions<FilmContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var produtoEntityAssembly = AppDomain.CurrentDomain
                .GetAssemblies()
                .Where(a => a.FullName.Contains("Film.Entity"))
                .FirstOrDefault();

            modelBuilder.ApplyConfigurationsFromAssembly(produtoEntityAssembly);

            //var cascadeFKs = modelBuilder.Model.GetEntityTypes()
            //    .SelectMany(t => t.GetForeignKeys())
            //    .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            //foreach (var fk in cascadeFKs)
            //    fk.DeleteBehavior = DeleteBehavior.Restrict;

            base.OnModelCreating(modelBuilder);
        }

    }
}
