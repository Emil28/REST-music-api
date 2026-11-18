using Microsoft.EntityFrameworkCore;
using REST_music_api.Data;
using REST_music_api.Model;

namespace REST_music_api.Repository;

public class SqlMusicRecordRepository
{
    private readonly MusicRecordsDbContext _context;

    public SqlMusicRecordRepository(MusicRecordsDbContext context)
    {
        _context = context;
    }

    public async Task<List<MusicRecord>> GetAllAsync(
        string? title = null,
        string? artist = null,
        int? id = null,
        int? duration = null,
        int? publicationYear = null,
        string? sortBy = null)
    {
        IQueryable<MusicRecord> records = _context.MusicRecords.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(title))
        {
            records = records.Where(record => EF.Functions.Like(record.Title, $"%{title}%"));
        }

        if (!string.IsNullOrWhiteSpace(artist))
        {
            records = records.Where(record => EF.Functions.Like(record.Artist, $"%{artist}%"));
        }

        if (id.HasValue)
        {
            records = records.Where(record => record.Id == id.Value);
        }

        if (duration.HasValue)
        {
            records = records.Where(record => record.Duration == duration.Value);
        }

        if (publicationYear.HasValue)
        {
            records = records.Where(record => record.PublicationYear == publicationYear.Value);
        }

        records = sortBy?.ToLowerInvariant() switch
        {
            "id" => records.OrderBy(record => record.Id),
            "title" => records.OrderBy(record => record.Title),
            "artist" => records.OrderBy(record => record.Artist),
            "duration" => records.OrderBy(record => record.Duration),
            "publicationyear" => records.OrderBy(record => record.PublicationYear),
            _ => records
        };

        return await records.ToListAsync();
    }

    public async Task<MusicRecord?> GetByIdAsync(int id)
    {
        return await _context.MusicRecords
            .AsNoTracking()
            .FirstOrDefaultAsync(record => record.Id == id);
    }

    public async Task<MusicRecord> AddAsync(MusicRecord record)
    {
        record.Id = 0;
        _context.MusicRecords.Add(record);
        await _context.SaveChangesAsync();
        return record;
    }

    public async Task<MusicRecord?> DeleteAsync(int id)
    {
        var record = await _context.MusicRecords.FindAsync(id);
        if (record is null)
        {
            return null;
        }

        _context.MusicRecords.Remove(record);
        await _context.SaveChangesAsync();
        return record;
    }

    public async Task<MusicRecord?> UpdateAsync(int id, MusicRecord updatedRecord)
    {
        var record = await _context.MusicRecords.FindAsync(id);
        if (record is null)
        {
            return null;
        }

        record.Title = updatedRecord.Title;
        record.Artist = updatedRecord.Artist;
        record.Duration = updatedRecord.Duration;
        record.PublicationYear = updatedRecord.PublicationYear;

        await _context.SaveChangesAsync();
        return record;
    }
}
