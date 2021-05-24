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
        public DrillDataService(ILogger<DrillDataService> logger)
        {
            _logger = logger;

        }

        public async Task DrillData(string fileType)
        {
            try
            {
                var fileName = Path.Combine(Directory.GetCurrentDirectory(), "Sources", FileTypes.ArtistCollection, FileTypes.ArtistCollection);
                using var streamer = File.OpenText(fileName);
                int row = 0;
                string line;

                while ((line = await streamer.ReadLineAsync()) != null)
                {
                    row++;
                    if (row <= 3)
                        continue;

                    switch (fileType)
                    {
                        case FileTypes.ArtistCollection:
                            Console.WriteLine("Case 1");
                            break;
                        case FileTypes.Artist:
                            Console.WriteLine("Case 2");
                            break;
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

        private ArtistCollection PopulateArtistCollection(string record)
        {
            try
            {
                var firstValue = record.Replace("\u0002", "");
                var secondValue = firstValue.Split('\x01');

                var artistCollection = new ArtistCollection
				{
					ExportDate = secondValue[0],
					ArtistId = int.Parse(secondValue[1]),
					CollectionId = int.Parse(secondValue[2]),
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
    }
}