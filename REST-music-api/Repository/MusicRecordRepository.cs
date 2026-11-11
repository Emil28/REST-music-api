using REST_music_api.Model;
using System.Linq;

namespace REST_music_api.Repository
{
    public class MusicRecordRepository
    {
        private static int _nextInt = 1;

        private static readonly List<MusicRecord> _records = new List<MusicRecord>();

        public List<MusicRecord> GetAll(string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                return _records;
            }
            return _records.Where(r => r.Title.Contains(title, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public MusicRecord? GetById(int id)
        {
            return _records.FirstOrDefault(r => r.Id == id);
        }

        public MusicRecord Add(MusicRecord record)
        {
            record.Id = _nextInt++;
            _records.Add(record);
            return record;
        }

        public MusicRecord? Delete(int id)
        {
            var record = GetById(id);
            if (record != null)
            {
                _records.Remove(record);
            }
            return record;
        }

        public MusicRecord? Update(int id, MusicRecord updatedRecord)
        {
            var record = GetById(id);
            if (record != null)
            {
                record.Title = updatedRecord.Title;
                record.Artist = updatedRecord.Artist;
                record.Duration = updatedRecord.Duration;
                record.PublicationYear = updatedRecord.PublicationYear;
            }
            return record;
        }
    }
}
