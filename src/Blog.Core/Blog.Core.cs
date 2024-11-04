using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Blog.Core
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //public DbSet<Post> Posts { get; set; }
        // Outras DbSets...
    }
}
