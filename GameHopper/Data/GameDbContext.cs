using System.Reflection;

namespace GameHopper;

public class GameDbContext
{

        public DbSet<Game> Games { get; set; }

        public DbSet<GameMaster> GameMasters { get; set; }

        public DbSet<Player> Players { get; set; }

        public DbSet<GameSystem> GameSystems { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<Image> Images { get; set; }

        public GameDbContext(DbContextOptions<EventDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<Game>()
            //     .HasOne(m => m.GameMaster)
            //     .WithMany(t => t.Tags);

            // modelBuilder.Entity<Game>()
            //     .HasMany(t => t.Tags)
            //     .WithMany(p => p.Players)
            //     .UsingEntity(j => j.ToTable(""));
        }
    }



