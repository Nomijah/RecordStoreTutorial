using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RecordStore.Core.Models;

namespace RecordStore.Infrastructure.Data
{
    public class RecordStoreDbContext : DbContext
    {
        public RecordStoreDbContext(DbContextOptions<RecordStoreDbContext> options) : base(options)
        {
        }

        public DbSet<Album> Albums { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Genre> Genres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Album entity
            modelBuilder.Entity<Album>(entity =>
            {
                entity.HasKey(a => a.AlbumId);
                entity.Property(a => a.AlbumId).ValueGeneratedOnAdd();
                entity.Property(a => a.Price).HasColumnType("decimal(10,2)");
                entity.HasIndex(a => a.CatalogNumber).IsUnique().HasFilter("[CatalogNumber] IS NOT NULL");

                // Configure relationships
                entity.HasOne(a => a.Artist)
                      .WithMany(ar => ar.Albums)
                      .HasForeignKey(a => a.ArtistId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(a => a.Genre)
                      .WithMany(g => g.Albums)
                      .HasForeignKey(a => a.GenreId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Configure Artist entity
            modelBuilder.Entity<Artist>(entity =>
            {
                entity.HasIndex(a => a.Name);
            });

            // Configure Genre entity
            modelBuilder.Entity<Genre>(entity =>
            {
                entity.HasIndex(g => g.Name).IsUnique();
            });

            // Seed data
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            var genres = new[]
            {
                new Genre { Id = 1, Name = "Rock", Description = "Rock music genre" },
                new Genre { Id = 2, Name = "Pop", Description = "Pop music genre" },
                new Genre { Id = 3, Name = "Jazz", Description = "Jazz music genre" },
                new Genre { Id = 4, Name = "Electronic", Description = "Electronic music genre" },
                new Genre { Id = 5, Name = "Classical", Description = "Classical music genre" }
            };

            var artists = new[]
            {
                new Artist { Id = 1, Name = "The Beatles", Country = "United Kingdom", FormedDate = new DateTime(1960, 8, 1), Biography = "English rock band formed in Liverpool in 1960" },
                new Artist { Id = 2, Name = "Pink Floyd", Country = "United Kingdom", FormedDate = new DateTime(1965, 1, 1), Biography = "English rock band formed in London in 1965" },
                new Artist { Id = 3, Name = "Miles Davis", Country = "United States", FormedDate = new DateTime(1944, 1, 1), Biography = "American jazz trumpeter, bandleader, and composer" },
                new Artist { Id = 4, Name = "Daft Punk", Country = "France", FormedDate = new DateTime(1993, 1, 1), Biography = "French electronic music duo", IsActive = false },
                new Artist { Id = 5, Name = "Ludwig van Beethoven", Country = "Germany", FormedDate = new DateTime(1770, 12, 16), Biography = "German composer and pianist", IsActive = false }
            };

            var albums = new[]
            {
                new Album {
                    AlbumId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Title = "Abbey Road",
                    Price = 24.99m,
                    ReleaseDate = new DateTime(1969, 9, 26),
                    CatalogNumber = "PCS7088",
                    Description = "The eleventh studio album by The Beatles",
                    Stock = 50,
                    TrackCount = 17,
                    DurationMinutes = 47,
                    ArtistId = 1,
                    GenreId = 1
                },
                new Album {
                    AlbumId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    Title = "The Dark Side of the Moon",
                    Price = 29.99m,
                    ReleaseDate = new DateTime(1973, 3, 1),
                    CatalogNumber = "SHVL804",
                    Description = "The eighth studio album by Pink Floyd",
                    Stock = 30,
                    TrackCount = 9,
                    DurationMinutes = 43,
                    ArtistId = 2,
                    GenreId = 1
                },
                new Album {
                    AlbumId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    Title = "Kind of Blue",
                    Price = 19.99m,
                    ReleaseDate = new DateTime(1959, 8, 17),
                    CatalogNumber = "CL1355",
                    Description = "Studio album by Miles Davis",
                    Stock = 25,
                    TrackCount = 5,
                    DurationMinutes = 46,
                    ArtistId = 3,
                    GenreId = 3
                },
                new Album {
                    AlbumId = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                    Title = "Random Access Memories",
                    Price = 27.99m,
                    ReleaseDate = new DateTime(2013, 5, 17),
                    CatalogNumber = "88883716861",
                    Description = "Fourth studio album by Daft Punk",
                    Stock = 40,
                    TrackCount = 13,
                    DurationMinutes = 74,
                    ArtistId = 4,
                    GenreId = 4
                },
                new Album {
                    AlbumId = Guid.Parse("55555555-5555-5555-5555-555555555555"),
                    Title = "Symphony No. 9",
                    Price = 15.99m,
                    ReleaseDate = new DateTime(1824, 5, 7),
                    CatalogNumber = "OP125",
                    Description = "Beethoven's final complete symphony",
                    Stock = 20,
                    TrackCount = 4,
                    DurationMinutes = 65,
                    ArtistId = 5,
                    GenreId = 5
                }
            };

            modelBuilder.Entity<Genre>().HasData(genres);
            modelBuilder.Entity<Artist>().HasData(artists);
            modelBuilder.Entity<Album>().HasData(albums);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is BaseEntity && e.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                ((BaseEntity)entry.Entity).UpdatedAt = DateTime.UtcNow;
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
