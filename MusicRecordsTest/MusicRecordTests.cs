using REST_music_api.Model;
using REST_music_api.Repository;
namespace MusicRecordTest
{
    [TestClass]
    public class MusicRecordRepositoryTests
    {
        [TestMethod]
        public void GetAll_ReturnsAllRecords_WhenTitleIsNull()
        {
            var repo = new MusicRecordRepository();
            Assert.AreEqual(6, repo.GetAll(null).Count());
        }

        [TestMethod]
        public void GetAll_FiltersByTitle()
        {
            var repo = new MusicRecordRepository();

            Assert.AreEqual(1, repo.GetAll(title: "Blinding").Count);
        }

        [TestMethod]
        public void GetAll_FiltersByArtistIgnoringCase()
        {
            var repo = new MusicRecordRepository();

            Assert.AreEqual(1, repo.GetAll(artist: "weeknd").Count);
        }

        [TestMethod]
        public void GetAll_AppliesMultipleFilters()
        {
            var repo = new MusicRecordRepository();

            Assert.AreEqual(1, repo.GetAll(
                title: "Blinding",
                artist: "The Weeknd",
                duration: 3,
                publicationYear: 2019).Count);
        }

        [TestMethod]
        public void GetAll_SortsByTitle()
        {
            var repo = new MusicRecordRepository();

            Assert.AreEqual("As It Was", repo.GetAll(sortBy: "title")[0].Title);
        }

        [TestMethod]
        public void Add_AssignsAnIdAndStoresTheRecord()
        {
            var repo = new MusicRecordRepository();
            var record = new MusicRecord("Test Song", "Test Artist", 180, 2024);

            var added = repo.Add(record);

            try
            {
                Assert.IsTrue(added.Id > 0);
                Assert.AreEqual(added, repo.GetById(added.Id));
            }
            finally
            {
                repo.Delete(added.Id);
            }
        }

        [TestMethod]
        public void Delete_RemovesAnExistingRecord()
        {
            var repo = new MusicRecordRepository();
            var added = repo.Add(new MusicRecord("Test Song", "Test Artist", 180, 2024));

            var deleted = repo.Delete(added.Id);

            Assert.AreEqual(added.Id, deleted?.Id);
            Assert.IsNull(repo.GetById(added.Id));
        }

        [TestMethod]
        public void Update_ChangesAnExistingRecord()
        {
            var repo = new MusicRecordRepository();
            var added = repo.Add(new MusicRecord("Old Title", "Old Artist", 180, 2024));

            try
            {
                var updated = repo.Update(
                    added.Id,
                    new MusicRecord("New Title", "New Artist", 200, 2025));

                Assert.IsNotNull(updated);
                Assert.AreEqual("New Title", updated.Title);
                Assert.AreEqual("New Artist", updated.Artist);
                Assert.AreEqual(200, updated.Duration);
                Assert.AreEqual(2025, updated.PublicationYear);
            }
            finally
            {
                repo.Delete(added.Id);
            }
        }
    }
}
