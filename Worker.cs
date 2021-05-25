using System;
using System.Threading;
using Application.Features.GoogleDrive.Commands.DownloadFile;
using Application.Features.GoogleDrive.Commands.ElasticSearch;
using Domain;
using Infrastructure.Persistence.Interface;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DataIngestion.TestAssignment
{
    public class Worker
    {
        private readonly IMediator _mediator;
        private readonly ILogger<Worker> _logger;
        private readonly IDrillData _drillData;
        private readonly IMusicCollectionRepository _musicCollection;
        public Worker(IMediator mediator, ILogger<Worker> logger, IDrillData drillData, IMusicCollectionRepository musicCollection)
        {
            _musicCollection = musicCollection;
            _drillData = drillData;
            _logger = logger;
            _mediator = mediator;

        }

        public async void Run(CancellationToken cancellationToken)
        {
            try
            {
                var request = new DownloadFileCommand
                {
                    PublicFolderId = "1RkUWkw9W0bijf7GOgV4ceiFppEpeXWGv",
                    GoogleDriveApiKey = "AIzaSyAuSWvpMscZhppVbvgEXjc5GlOr-aMrP64"
                };

                //Download and extract files.    
                await _mediator.Send(request, cancellationToken);

                //Populate respective tables.
                foreach (FType fileType in (FType[])Enum.GetValues(typeof(FType)))
                {
                    await _drillData.DrillData(fileType.ToString());
                }

                //Insert into Elastic Search.
                var size = 50;
                var skip = 0;
            
                for (var i = 0; i < size; i++)
                {
                    var elasticRequest = new ElasticSearchCommand()
                    {
                        Library = _musicCollection.GetAlbums(size, skip)
                    };
                    await _mediator.Send(elasticRequest, cancellationToken);
                    skip += size;
                }

            }
            catch (System.Exception ex)
            {
                _logger.LogError($"{ex.Message}");
                throw new System.ApplicationException();
            }
        }
    }
}