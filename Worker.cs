using System.Threading;
using Application.Features.GoogleDrive.Commands.DownloadFile;
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
        public Worker(IMediator mediator, ILogger<Worker> logger, IDrillData drillData)
        {
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
                //await _mediator.Send(request, cancellationToken);

                //Populate respective tables.
                await _drillData.DrillData("artist_collection");

            }
            catch (System.Exception ex)
            {
                _logger.LogError($"{ex.Message}");
                throw new System.ApplicationException();
            }
        }
    }
}