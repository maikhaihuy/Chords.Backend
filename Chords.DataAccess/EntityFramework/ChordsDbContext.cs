using Chords.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Chords.DataAccess.EntityFramework
{
    public partial class ChordsDbContext : DbContext
    {
        public ChordsDbContext(DbContextOptions<ChordsDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Performance> Performances { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Token> Tokens { get; set; }
        
        // protected override void OnConfiguring(DbContextOptionsBuilder options)
        // {
        //     _ = options.UseNpgsql("Host=localhost;Database=chords;Username=postgres;Password=1234");
        // }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}