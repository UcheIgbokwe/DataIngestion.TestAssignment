using Domain.Model;
using Infrastructure.Persistence.Interface;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Persistence.Services
{
    public class PopulateService : IPopulate
    {
        private readonly ILogger<PopulateService> _logger;
        public PopulateService(ILogger<PopulateService> logger)
        {
            _logger = logger;

        }
        public ArtistCollection PopulateArtistCollection(string record)
        {
            try
            {
                var firstValue = record.Replace("\u0002", "");
                var secondValue = firstValue.Split('\x01');

                var artistCollection = new ArtistCollection
                {
                    ExportDate = secondValue[0],
                    ArtistId = long.Parse(secondValue[1]),
                    CollectionId = long.Parse(secondValue[2]),
                    IsPrimaryArtist = secondValue[3],
                    RoleId = int.Parse(secondValue[4]),
                };
                return artistCollection;
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public Artist PopulateArtist(string record)
        {
            try
            {
                var firstValue = record.Replace("\u0002", "");
                var secondValue = firstValue.Split('\x01');

                var artist = new Artist
                {
                    ExportDate = secondValue[0],
                    ArtistId = long.Parse(secondValue[1]),
                    Name = secondValue[2],
                    IsActualArtist = secondValue[3],
                    ViewUrl = secondValue[4],
                    ArtistTypeId = secondValue[5]
                };

                return artist;
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public Collection PopulateCollection(string record)
        {
            try
            {
                var firstValue = record.Replace("\u0002", "");
                var secondValue = firstValue.Split('\x01');

                var collection = new Collection
                {
                    ExportDate = secondValue[0] ?? string.Empty,
                    CollectionId = long.Parse(secondValue[1]),
                    Name = secondValue[2] ?? string.Empty,
                    TitleVersion = secondValue[3] ?? string.Empty,
                    SearchTerms = secondValue[4] ?? string.Empty,
                    ParentalAdvisoryId = secondValue[5] ?? string.Empty,
                    ArtistDisplayName = secondValue[6] ?? string.Empty,
                    ViewUrl = secondValue[7] ?? string.Empty,
                    ArtworkUrl = secondValue[8] ?? string.Empty,
                    OriginalReleaseDate = secondValue[9] ?? string.Empty,
                    ItunesReleaseDate = secondValue[10] ?? string.Empty,
                    LabelStudio = secondValue[11] ?? string.Empty,
                    ContentProviderName = secondValue[12] ?? string.Empty,
                    Copyright = secondValue[13] ?? string.Empty,
                    PLine = secondValue[14] ?? string.Empty,
                    MediaTypeId = secondValue[15] ?? string.Empty,
                    IsCompilation = secondValue[16] ?? string.Empty,
                    CollectionTypeId = secondValue[17] ?? string.Empty,
                };

                return collection;
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public CollectionMatch PopulateCollectionMatch(string record)
        {
            try
            {
                var firstValue = record.Replace("\u0002", "");
                var secondValue = firstValue.Split('\x01');

                var collectionMatch = new CollectionMatch
                {
                    ExportDate = secondValue[0],
                    CollectionId = long.Parse(secondValue[1]),
                    Upc = secondValue[2],
                    Grid = secondValue[3],
                    AmgAlbumId = secondValue[4]
                };

                return collectionMatch;

            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }
    }
}