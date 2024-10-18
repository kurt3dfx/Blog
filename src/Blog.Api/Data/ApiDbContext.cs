using Blog.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blog.Api.Data
{
    public class ApiDbContext : IdentityDbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) 
        { 
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
    }
}
