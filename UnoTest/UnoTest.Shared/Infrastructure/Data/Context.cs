using Microsoft.EntityFrameworkCore;
using UnoTest.Shared.Infrastructure;
using UnoTest.Shared.Models;

namespace UnoTest.Shared.Data
{
    public class Context : DbContext
    {
        public Context() : base() { }
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(Constants.SQLiteFileName);
        }

        //public DbSet<Song> Songs { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<News>()
            //    .Ignore(x => x.PublishDateInFarsi);


        }
    }
}
