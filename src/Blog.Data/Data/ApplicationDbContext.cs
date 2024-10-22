using Blog.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data.Data
{
    public class ApplicationDbContextData : IdentityDbContext
    {
        public ApplicationDbContextData(DbContextOptions<ApplicationDbContextData> options)
            : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
    }
}
