using MediatR;

namespace Application.Features.GoogleDrive.Commands.DownloadFile
{
    public class DownloadFileCommand : IRequest<int>
    {
        public string GoogleDriveApiKey { get; set; }
        public string PublicFolderId { get; set; }
        
        
    }
}