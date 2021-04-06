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
    }
}
