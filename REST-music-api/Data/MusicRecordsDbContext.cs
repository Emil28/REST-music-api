using Microsoft.EntityFrameworkCore;
using REST_music_api.Model;

namespace REST_music_api.Data;

public class MusicRecordsDbContext : DbContext
{
    public MusicRecordsDbContext(DbContextOptions<MusicRecordsDbContext> options)
        : base(options)
    {
    }

    public DbSet<MusicRecord> MusicRecords => Set<MusicRecord>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var records = modelBuilder.Entity<MusicRecord>();

        records.ToTable("MusicRecords");
        records.HasKey(record => record.Id);
        records.Property(record => record.Id).ValueGeneratedOnAdd();
        records.Property(record => record.Title).HasMaxLength(200).IsRequired();
        records.Property(record => record.Artist).HasMaxLength(200).IsRequired();
        records.Property(record => record.Duration).IsRequired();
        records.Property(record => record.PublicationYear).IsRequired();

        records.HasData(
            new MusicRecord
            {
                Id = 1,
                Title = "Blinding Lights",
                Artist = "The Weeknd",
                Duration = 420,
                PublicationYear = 2019
            },
            new MusicRecord
            {
                Id = 2,
                Title = "Levitating",
                Artist = "Dua Lipa",
                Duration = 200,
                PublicationYear = 2020
            },
            new MusicRecord
            {
                Id = 3,
                Title = "Levitating",
                Artist = "Dua Lipa",
                Duration = 4724,
                PublicationYear = 2020
            },
            new MusicRecord
            {
                Id = 4,
                Title = "Shape of You",
                Artist = "Ed Sheeran",
                Duration = 69,
                PublicationYear = 2017
            },
            new MusicRecord
            {
                Id = 5,
                Title = "Bad Guy",
                Artist = "Billie Eilish",
                Duration = 194,
                PublicationYear = 2019
            },
            new MusicRecord
            {
                Id = 6,
                Title = "As It Was",
                Artist = "Harry Styles",
                Duration = 242,
                PublicationYear = 2022
            });
    }
}
