using System.Threading;
using Application.Features.GoogleDrive.Commands.DownloadFile;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DataIngestion.TestAssignment
{
    public class Worker
    {
        private readonly IMediator _mediator;
        private readonly ILogger<Worker> _logger;
        public Worker(IMediator mediator, ILogger<Worker> logger)
        {
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

                var response = await _mediator.Send(request, cancellationToken);

            }
            catch (System.Exception ex)
            {
                _logger.LogError($"{ex.Message}");
                throw new System.ApplicationException();
            }  
        }
    }
}