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
    }
}
