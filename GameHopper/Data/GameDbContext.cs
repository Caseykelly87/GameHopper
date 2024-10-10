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
        public DbSet<Piece> Pieces { get; set; }
        public DbSet<Gallery> Galleries { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                // Discriminator for inheritance
                entity.HasDiscriminator<string>("Discriminator")
                    .HasValue<Player>("Player")
                    .HasValue<GameMaster>("GameMaster");

                // ProfilePicture column configuration
                entity.Property(e => e.ProfilePicture)
                    .HasColumnType("LONGBLOB")
                    .IsRequired(false); // Nullable column

                // Relationships for the User entity
                entity.HasMany(u => u.CurrentGames)
                    .WithMany(g => g.GamePlayers);

            });

            modelBuilder.Entity<Game>(entity =>
            {
                // GamePicture column configuration
                entity.Property(e => e.GamePicture)
                    .HasColumnType("LONGBLOB")
                    .IsRequired(false); // Nullable column

                entity.Property(e => e.Description)
                    .HasColumnType("LONGTEXT");

                // Relationships for the Game entity
                entity.HasOne(g => g.Category)
                    .WithMany(c => c.Games)
                    .HasForeignKey(g => g.CategoryId);

                entity.HasMany(g => g.Tags)
                    .WithMany(t => t.Games);

                // Indexes for the Game entity
                entity.HasIndex(g => g.CategoryId)
                    .HasDatabaseName("IX_Game_CategoryId");

                // Add composite full-text index for both Game Title and Description
                entity.HasIndex(g => new { g.Title, g.Description })
                    .HasDatabaseName("IX_Game_Title_Description");

            });

            modelBuilder.Entity<GameMaster>()
                .HasMany(gm => gm.CreatedGames)
                .WithOne(g => g.GameMaster)
                .HasForeignKey(g => g.GameMasterId);
            
            modelBuilder.Entity<BlogEntry>()
                .HasOne(b => b.User)
                .WithMany(u => u.BlogEntries)
                .HasForeignKey(b => b.UserId);

        }
    }
    


