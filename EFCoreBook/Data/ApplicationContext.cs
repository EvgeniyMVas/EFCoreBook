using EFCoreBook.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCoreBook.Data
{
    public class ApplicationContext:DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }
        public DbSet<Book>Books { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
