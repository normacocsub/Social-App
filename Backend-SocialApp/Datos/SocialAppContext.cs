using System;
using Entity;
using Microsoft.EntityFrameworkCore;

namespace Datos
{
    public class SocialAppContext : DbContext
    {
        public SocialAppContext(DbContextOptions options):base(options)
        {
            
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Publicacion> Publicacions { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<Reaccion> Reacciones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Publicacion>().HasOne<Usuario>().WithMany()
            .HasForeignKey(p => p.IdUsuario);

            modelBuilder.Entity<Comentario>().HasOne<Usuario>().WithMany()
            .HasForeignKey(c => c.IdUsuario);

            modelBuilder.Entity<Reaccion>().HasOne<Usuario>().WithMany()
            .HasForeignKey(r => r.IdUsuario);
        }
    }
}
