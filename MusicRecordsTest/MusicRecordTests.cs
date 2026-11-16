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
            Assert.HasCount(6, repo.GetAll());
        }

        [TestMethod]
        public void GetAll_FiltersByTitle()
        {
            var repo = new MusicRecordRepository();

            Assert.HasCount(1, repo.GetAll(title: "Blinding"));
        }

        [TestMethod]
        public void GetAll_FiltersByArtistIgnoringCase()
        {
            var repo = new MusicRecordRepository();

            Assert.HasCount(1, repo.GetAll(artist: "weeknd"));
        }

        [TestMethod]
        public void GetAll_FiltersByIdDurationAndPublicationYear()
        {
            var repo = new MusicRecordRepository();

            Assert.HasCount(1, repo.GetAll(id: 1, duration: 3, publicationYear: 2019));
        }

        [TestMethod]
        public void GetAll_CombinesTitleAndArtistFilters()
        {
            var repo = new MusicRecordRepository();

            Assert.HasCount(0, repo.GetAll(title: "Blinding", artist: "Dua Lipa"));
        }
    }
}
