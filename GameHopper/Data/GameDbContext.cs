using System.Reflection;
using GameHopper.Models;
using Microsoft.EntityFrameworkCore;

namespace GameHopper;

public class GameDbContext
{

        public DbSet<Game>? Games { get; set; }

        public DbSet<GameMaster>? GameMaster { get; set; }

        public DbSet<Player>? Players { get; set; }

        public DbSet<Category>? Categories { get; set; }

        public DbSet<Tag>? Tags { get; set; }

        public DbSet<Image>? Images { get; set; }

        public GameDbContext(DbContextOptions<GameDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>()
                .HasOne(c => c.Category)
                .WithMany(g => g.Games);
            
            modelBuilder.Entity<Game>()
                .HasOne(m => m.GameMaster)
                .WithMany(g => g.Games);

            modelBuilder.Entity<Game>()
                .HasMany(u => u.Users)
                .WithMany(g => g.Games);
            
            modelBuilder.Entity<Game>()
                .HasMany(t => t.Tags)
                .WithMany(g => g.Games);
                
            modelBuilder.Entity<Category>()
                .HasMany(t => t.Tags)
                .WithMany(c => c.Categories);
            
            modelBuilder.Entity<GameMaster>()
                .HasMany(o => o.CreatedGames)
                .Withone(g => g.GameMaster)
                .hasforeignkey(g. => g.GMasterId);
            
            modelBuilder.Entity<Game>()
                .HasMany(p => p.Players)
                .withMany(g => g.Games)
                .hasforeignkey(i => i.GameId);
            
            modelBuilder.Entity<Image>()
                .HasOne(u => u.User)
                .withMany(a => a. Images)
                .hasforeignkey(ui => ui.UserId);
            
            }
    }

public class Category
{
}