using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using REST_music_api.Data;
using REST_music_api.Model;
using REST_music_api.Repository;

namespace MusicRecordTest;

[TestClass]
public sealed class SqlMusicRecordRepositoryTests
{
    private SqliteConnection _connection = null!;
    private MusicRecordsDbContext _context = null!;
    private SqlMusicRecordRepository _repository = null!;

    [TestInitialize]
    public async Task Initialize()
    {
        _connection = new SqliteConnection("Data Source=:memory:");
        await _connection.OpenAsync();

        var options = new DbContextOptionsBuilder<MusicRecordsDbContext>()
            .UseSqlite(_connection)
            .Options;

        _context = new MusicRecordsDbContext(options);
        await _context.Database.EnsureCreatedAsync();
        _repository = new SqlMusicRecordRepository(_context);
    }

    [TestCleanup]
    public async Task Cleanup()
    {
        await _context.DisposeAsync();
        await _connection.DisposeAsync();
    }

    [TestMethod]
    public async Task GetAll_FiltersByDuration()
    {
        var records = await _repository.GetAllAsync(duration: 420);

        Assert.AreEqual(1, records.Count);
        Assert.AreEqual("Blinding Lights", records[0].Title);
    }

    [TestMethod]
    public async Task GetAll_FiltersByPublicationYear()
    {
        var records = await _repository.GetAllAsync(publicationYear: 2022);

        Assert.AreEqual(1, records.Count);
        Assert.AreEqual("As It Was", records[0].Title);
    }

    [TestMethod]
    public async Task Add_AssignsAnIdAndStoresTheRecord()
    {
        var added = await _repository.AddAsync(
            new MusicRecord("Test Song", "Test Artist", 180, 2024));

        Assert.IsTrue(added.Id > 0);
        Assert.AreEqual(added.Id, (await _repository.GetByIdAsync(added.Id))?.Id);
    }

    [TestMethod]
    public async Task Update_ChangesAnExistingRecord()
    {
        var updated = await _repository.UpdateAsync(
            1,
            new MusicRecord("New Title", "New Artist", 200, 2025));

        Assert.IsNotNull(updated);
        Assert.AreEqual("New Title", updated.Title);
        Assert.AreEqual("New Artist", updated.Artist);
        Assert.AreEqual(200, updated.Duration);
        Assert.AreEqual(2025, updated.PublicationYear);
    }

    [TestMethod]
    public async Task Delete_RemovesAnExistingRecord()
    {
        var deleted = await _repository.DeleteAsync(1);

        Assert.IsNotNull(deleted);
        Assert.IsNull(await _repository.GetByIdAsync(1));
    }

    [TestMethod]
    public async Task UpdateAndDelete_ReturnNullForMissingRecords()
    {
        var updated = await _repository.UpdateAsync(
            999,
            new MusicRecord("Missing", "Missing Artist", 180, 2024));
        var deleted = await _repository.DeleteAsync(999);

        Assert.IsNull(updated);
        Assert.IsNull(deleted);
    }
}
