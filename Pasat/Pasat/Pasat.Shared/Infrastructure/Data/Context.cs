using Microsoft.EntityFrameworkCore;
using System;
using Uno.Extensions;
using Pasat.Infrastructure;
using Pasat.Models;
using Olive;
using Pasat.Extentions;
using Windows.Storage;
using System.Text.Json;
using System.Threading.Tasks;

namespace Pasat.Data
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
            // User to identifier setup
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

            modelBuilder.Entity<TestIndentifier>()
                .Ignore(x => x.ValidationContext);

            //Fragment-Pre fragment setup

            modelBuilder.Entity<TestFragment>()
                .HasOne(x => x.FragmentOf)
                .WithOne(x => x.TestFragment)
                .HasForeignKey<TestAnswer>(x => x.TestFragmentId);

            modelBuilder.Entity<TestFragment>()
               .HasOne(x => x.PreFragmentOf)
               .WithOne(x => x.PreFragment)
               .HasForeignKey<TestAnswer>(x => x.PreFragmentId);

            modelBuilder.Entity<User>()
                .Ignore(x => x.TestCount)
                .Ignore(x=>x.Age);


            // Seed

            modelBuilder.Entity<User>()
                .HasData(new User
                {
                    Id=1,
                    FullName = "Public",
                    Gender = Gender.NoAnswer,
                    Username = "public",
                    Education = Education.NoAnswer,
                    YearBorn = null,
                    CreatedAt = DateTimeOffset.UtcNow,
                    MaritalStatus=MaritalStatus.NoAnswer
                }) ;


        }

    }
}
