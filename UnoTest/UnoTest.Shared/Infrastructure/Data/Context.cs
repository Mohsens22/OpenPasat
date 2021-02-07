using Microsoft.EntityFrameworkCore;
using System;
using Uno.Extensions;
using UnoTest.Infrastructure;
using UnoTest.Models;
using Olive;
using UnoTest.Extentions;
using Windows.Storage;
using System.Text.Json;
using System.Threading.Tasks;

namespace UnoTest.Data
{
    public class Context : DbContext
    {
        public Context() : base() { }
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
#if DEBUG
             optionsBuilder.UseLoggerFactory(LogExtensionPoint.AmbientLoggerFactory);
             optionsBuilder.EnableSensitiveDataLogging(true);
#endif
            optionsBuilder.UseSqlite(Constants.SQLiteFileName);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<TestIndentifier> Tests { get; set; }
        public DbSet<TestFragment> Fragments { get; set; }
        public DbSet<TestAnswer> Answers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany<TestIndentifier>(x => x.Tests)
                .WithOne(x => x.User)
                .HasForeignKey(x=>x.UserId);


            modelBuilder.Entity<TestIndentifier>()
                .HasMany<TestFragment>(x => x.TestFragments)
                .WithOne(x => x.Indentifier)
                .HasForeignKey(x => x.IndentifierId);

            modelBuilder.Entity<TestIndentifier>()
                .HasMany<TestAnswer>(x => x.Answers)
                .WithOne(x => x.Indentifier)
                .HasForeignKey(x => x.IndentifierId);

            modelBuilder.Entity<User>()
                .Ignore(x => x.TestCount);

            // Seed

            modelBuilder.Entity<User>()
                .HasData(new User
                {
                    Id=1,
                    FullName = "Anonymous",
                    Gender = Gender.NoAnswer,
                    Username = "anonymous",
                    Education = Education.NoAnswer,
                    YearBorn = null,
                    CreatedAt = DateTimeOffset.UtcNow,
                    MaritalStatus=MaritalStatus.NoAnswer
                }) ;


        }

    }
}
