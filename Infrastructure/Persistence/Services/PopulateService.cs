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
                    ExportDate = secondValue[0],
                    CollectionId = long.Parse(secondValue[1]),
                    Name = secondValue[2],
                    TitleVersion = secondValue[3],
                    SearchTerms = secondValue[4],
                    ParentalAdvisoryId = secondValue[5],
                    ArtistDisplayName = secondValue[6],
                    ViewUrl = secondValue[7],
                    ArtworkUrl = secondValue[8],
                    OriginalReleaseDate = secondValue[9],
                    ItunesReleaseDate = secondValue[10],
                    LabelStudio = secondValue[11],
                    ContentProviderName = secondValue[12],
                    Copyright = secondValue[13],
                    PLine = secondValue[14],
                    MediaTypeId = secondValue[15],
                    IsCompilation = secondValue[16],
                    CollectionTypeId = secondValue[17],
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
                    CollectionId = int.Parse(secondValue[1]),
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