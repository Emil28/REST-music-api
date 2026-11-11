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
    }
}
