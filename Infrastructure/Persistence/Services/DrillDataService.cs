using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Domain.Model;
using Infrastructure.Persistence.Interface;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Persistence.Services
{
    public class DrillDataService : IDrillData
    {
        private readonly ILogger<DrillDataService> _logger;
        private readonly IMusicCollectionRepository _musicCollection;
        private readonly IPopulate _populate;
        public DrillDataService(ILogger<DrillDataService> logger, IMusicCollectionRepository musicCollection, IPopulate populate)
        {
            _populate = populate;
            _musicCollection = musicCollection;
            _logger = logger;

        }

        public async Task DrillData(string fileType)
        {
            try
            {
                var fileName = Path.Combine(Directory.GetCurrentDirectory(), "Sources", fileType, fileType);
                using var streamer = File.OpenText(fileName);
                int row = 0;
                string record;

                while ((record = await streamer.ReadLineAsync()) != null)
                {
                    row++;
                    if (row <= 3)
                        continue;

                    switch (fileType)
                    {
                        case FileTypes.ArtistCollection:
                            //Populate data to be inserted to table.
                            var artistCollection = _populate.PopulateArtistCollection(record);
                            await _musicCollection.ArtistCollection(artistCollection);
                            continue;
                        case FileTypes.Artist:
                            var artist = _populate.PopulateArtist(record);
                            await _musicCollection.Artist(artist);
                            continue;
                        case FileTypes.CollectionMatch:
                            var collectionMatch = _populate.PopulateCollectionMatch(record);
                            await _musicCollection.CollectionMatch(collectionMatch);
                            continue;
                        case FileTypes.Collection:
                            var collection = _populate.PopulateCollection(record);
                            await _musicCollection.Collection(collection);
                            continue;
                        default:
                            Console.WriteLine("Default case");
                            break;
                    }
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
            }

        }
    }
}