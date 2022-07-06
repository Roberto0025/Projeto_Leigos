using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Leigos.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Models.Pessoa>? Pessoas { get; set; }
        public DbSet<Models.Genero>? Generos { get; set; }
        public DbSet<Models.Categoria>? Categorias { get; set; }
        public DbSet<Models.Servico>? Servicos { get; set; }
    }
}