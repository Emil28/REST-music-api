using REST_music_api.Model;
using System.Linq;

namespace REST_music_api.Repository
{
    public class MusicRecordRepository
    {
        private int _nextInt = 1;

        private readonly List<MusicRecord> _records;

        public MusicRecordRepository()
        {
            _records = new List<MusicRecord>
            {
                new MusicRecord
                {
                    Id = _nextInt++,
                    Title = "Blinding Lights",
                    Artist = "The Weeknd",
                    Duration = 3,
                    PublicationYear = 2019
                },
                new MusicRecord
                {
                    Id = _nextInt++,
                    Title = "Levitating",
                    Artist = "Dua Lipa",
                    Duration = 3,
                    PublicationYear = 2020
                },
                new MusicRecord
                {
                    Id = _nextInt++,
                    Title = "Levitating",
                    Artist = "Dua Lipa",
                    Duration = 3,
                    PublicationYear = 2020
                },
                new MusicRecord
                {
                    Id = _nextInt++,
                    Title = "Shape of You",
                    Artist = "Ed Sheeran",
                    Duration = 3,
                    PublicationYear = 2017
                },
                new MusicRecord
                {
                    Id = _nextInt++,
                    Title = "Bad Guy",
                    Artist = "Billie Eilish",
                    Duration = 3,
                    PublicationYear = 2019
                },
                new MusicRecord
                {
                    Id = _nextInt++,
                    Title = "As It Was",
                    Artist = "Harry Styles",
                    Duration = 2,
                    PublicationYear = 2022
                }
            };
        }

        public List<MusicRecord> GetAll(
            string? title = null,
            string? artist = null,
            int? id = null,
            int? duration = null,
            int? publicationYear = null,
            string? sortBy = null)
        {
            IEnumerable<MusicRecord> records = _records;

            if (!string.IsNullOrWhiteSpace(title))
            {
                records = records.Where(r => r.Title.Contains(title, StringComparison.OrdinalIgnoreCase));
            }
            if (!string.IsNullOrWhiteSpace(artist))
            {
                records = records.Where(r => r.Artist.Contains(artist, StringComparison.OrdinalIgnoreCase));
            }
            if (id.HasValue)
            {
                records = records.Where(r => r.Id == id.Value);
            }
            if (duration.HasValue)
            {
                records = records.Where(r => r.Duration == duration.Value);
            }
            if (publicationYear.HasValue)
            {
                records = records.Where(r => r.PublicationYear == publicationYear.Value);
            }

            return sortBy?.ToLowerInvariant() switch
            {
                "id" => records.OrderBy(r => r.Id).ToList(),
                "title" => records.OrderBy(r => r.Title).ToList(),
                "artist" => records.OrderBy(r => r.Artist).ToList(),
                "duration" => records.OrderBy(r => r.Duration).ToList(),
                "publicationyear" => records.OrderBy(r => r.PublicationYear).ToList(),
                _ => records.ToList()
            };
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
