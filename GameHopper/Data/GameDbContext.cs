using GameHopper.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;



namespace GameHopper;

public class GameDbContext : IdentityDbContext<User, IdentityRole, string>
{
    public GameDbContext(DbContextOptions<GameDbContext> options) : base(options)
    {
    }

        public DbSet<Game>? Games { get; set; }

        public DbSet<Category>? Categories { get; set; }

        public DbSet<Tag>? Tags { get; set; }
        public DbSet<Request>? Requests { get; set; }

        public DbSet<BlogEntry>? Blogs { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<User>()
        .HasMany(u => u.CurrentGames)
        .WithMany(g => g.GamePlayers)
        .UsingEntity<Dictionary<string, object>>(
            "UserGames",
            j => j
                .HasOne<Game>()
                .WithMany()
                .HasForeignKey("GameId"),
            j => j
                .HasOne<User>()
                .WithMany()
                .HasForeignKey("UserId")
        );

            modelBuilder.Entity<User>()
                .HasMany(p => p.CurrentGames)
                .WithMany(g => g.GamePlayers);

            modelBuilder.Entity<GameMaster>()
                .HasMany(gm => gm.CreatedGames)
                .WithOne(g => g.GameMaster)
                .HasForeignKey(g => g.GameMasterId);

            modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.ProfilePicture)
                .HasColumnType("BLOB")
                .IsRequired(false); // Ensure the column is nullable
        });    

        
        }
    }
            // modelBuilder.Entity<Game>()
            //     .HasOne(g => g.Category)
            //     .WithMany(c => c.Games)
            //     .HasForeignKey(g => g.CategoryId);

            // modelBuilder.Entity<Game>()
            //     .HasOne(g => g.GameMaster)
            //     .WithMany(gm => gm.CreatedGames)
            //     .HasForeignKey(g => g.GameMasterId);

            // modelBuilder.Entity<Game>()
            //     .HasMany(g => g.Tags)
            //     .WithMany(t => t.Games)
            //     .UsingEntity<Dictionary<string, object>>(
            //         "GameTags",
            //         j => j.HasOne<Tag>().WithMany().HasForeignKey("TagId"),
            //         j => j.HasOne<Game>().WithMany().HasForeignKey("GameId"));

            // modelBuilder.Entity<Game>()
            //     .HasMany(g => g.GamePlayers)
            //     .WithMany(p => p.CurrentGames)
            //     .UsingEntity<Dictionary<string, object>>(
            //         "GamePlayers",
            //         j => j.HasOne<Player>().WithMany().HasForeignKey("PlayerId"),
            //         j => j.HasOne<Game>().WithMany().HasForeignKey("GameId"));

            // modelBuilder.Entity<User>()
            //     .HasMany(u => u.CurrentGames)
            //     .WithMany(g => g.GamePlayers)
            //     .UsingEntity<Dictionary<string, object>>(
            //         "UserGames",
            //         j => j.HasOne<Game>().WithMany().HasForeignKey("GameId"),
            //         j => j.HasOne<User>().WithMany().HasForeignKey("UserId"));


            // modelBuilder.Entity<User>()
            //     .HasMany(u => u.CurrentGames)
            //     .WithOne()
            //     .HasForeignKey(g => g.UserId)
            //     .OnDelete(DeleteBehavior.Cascade);

            
            // modelBuilder.Entity<Player>()
            //     .HasMany(p => p.CurrentGames)
            //     .WithOne()
            //     .HasForeignKey(g => g.UserId)
            //     .OnDelete(DeleteBehavior.Cascade);
        
            // modelBuilder.Entity<Player>()
            //     .HasOne(u => u.Blog)
            //     .WithOne(b => b.Author)
            //     .HasForeignKey<BlogEntry>(b => b.AuthorId);
    



