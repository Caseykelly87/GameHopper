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
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Comment>? Comments { get; set; }
        public DbSet<SubComment>? SubComments { get; set; }
        public DbSet<Rating>? Ratings { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasDiscriminator<string>("Discriminator")
                .HasValue<Player>("Player")
                .HasValue<GameMaster>("GameMaster");
            
            modelBuilder.Entity<User>()
                .HasMany(u => u.CurrentGames)
                .WithMany(g => g.GamePlayers)
                .UsingEntity<Dictionary<string, object>>(
                "UserGames",
                j => j.HasOne<Game>()
                    .WithMany()
                    .HasForeignKey("GameId"),
                j => j.HasOne<User>()
                    .WithMany()
                    .HasForeignKey("UserId")
            );

            modelBuilder.Entity<GameMaster>()
                .HasMany(gm => gm.CreatedGames)
                .WithOne(g => g.GameMaster)
                .HasForeignKey(g => g.GameMasterId);

            modelBuilder.Entity<Game>()
                .HasOne(g => g.Category)
                .WithMany(c => c.Games)
                .HasForeignKey(g => g.CategoryId);

            modelBuilder.Entity<Game>()
                .HasIndex(g => g.CategoryId)  // Apply the index directly on the Game entity
                .HasDatabaseName("IX_Game_CategoryId");

                
            modelBuilder.Entity<Game>()
                .HasMany(g => g.Tags)
                .WithMany(t => t.Games)
                .UsingEntity<Dictionary<string, object>>(
                "GameTag",  // The name of the join table
                j => j.HasOne<Tag>()
                    .WithMany()
                    .HasForeignKey("TagsId"),
                j => j.HasOne<Game>()
                    .WithMany()
                    .HasForeignKey("GamesId"))
                    .HasIndex("GamesId", "TagsId")  // Adding composite index by column name
                    .HasDatabaseName("IX_GameTag_Composite");
                    
            // Add composite full-text index for both Game Title and Description
            modelBuilder.Entity<Game>()
                .HasIndex(g => new { g.Title, g.Description })
                .HasDatabaseName("IX_Game_Title_Description");    
            
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.ProfilePicture)
                    .HasColumnType("LONGBLOB")
                    .IsRequired(false); // Ensures the column is nullable
            
            });        
            
            modelBuilder.Entity<Game>(entity =>
            {            
                entity.Property(e => e.GamePicture)
                    .HasColumnType("LONGBLOB")
                    .IsRequired(false); // Ensures the column is nullable
            });   
            
            modelBuilder.Entity<BlogEntry>()
                .HasOne(b => b.User)
                .WithMany(u => u.BlogEntries)
                .HasForeignKey(b => b.UserId);

        }
    }
    


