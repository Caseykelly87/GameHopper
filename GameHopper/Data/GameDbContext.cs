using System.Reflection;
using GameHopper.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;



namespace GameHopper;

public class GameDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
{

        public DbSet<Game>? Games { get; set; }

        public DbSet<GameMaster>? GameMasters { get; set; }

        public DbSet<Player>? Players { get; set; }

        public DbSet<Category>? Categories { get; set; }

        public DbSet<Tag>? Tags { get; set; }

        // public DbSet<Image>? Images { get; set; }

        public GameDbContext(DbContextOptions<GameDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Game>()
                .HasOne(c => c.Category)
                .WithMany(g => g.Games);
            
            modelBuilder.Entity<Game>()
                .HasOne(m => m.GameMaster)
                .WithMany(g => g.CreatedGames);

            modelBuilder.Entity<Game>()
                .HasMany(p => p.Players)
                .WithMany(g => g.CurrentGames);
            
            modelBuilder.Entity<Game>()
                .HasMany(t => t.Tags)
                .WithMany(g => g.LinkedGames);

            modelBuilder.Entity<Category>()
                .HasMany(t => t.Tags)
                .WithMany(c => c.Categories);
            
            // modelBuilder.Entity<GameMaster>()
            //     .HasMany(o => o.CreatedGames)
            //     .WithOne(g => g.GameMaster)
            //     .hasforeignkey(g. => g.GMasterId);
            
            // modelBuilder.Entity<Game>()
            //     .HasMany(p => p.Players)
            //     .withMany(g => g.Games)
            //     .hasforeignkey(i => i.GameId);
            
            // modelBuilder.Entity<Image>()
            //     .HasOne(u => u.User)
            //     .withMany(a => a. Images)
            //     .HasForeignKey(ui => ui.UserId); 
    
        }
    }



