using System;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Application.Features.GoogleDrive.Commands.DownloadFile
{
    public class DownloadFileCommandHandler : IRequestHandler<DownloadFileCommand, int>
    {
        private readonly ILogger<DownloadFileCommandHandler> _logger;
        public DownloadFileCommandHandler(ILogger<DownloadFileCommandHandler> logger)
        {
            _logger = logger;

        }
        public async Task<int> Handle(DownloadFileCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var httpClient = new HttpClient();
                var nextPageToken = "";
                do
                {
                    var folderContentsUri = $"https://www.googleapis.com/drive/v3/files?q='{request.PublicFolderId}'+in+parents&key={request.GoogleDriveApiKey}";
                    if (!String.IsNullOrEmpty(nextPageToken))
                    {
                        folderContentsUri += $"&pageToken={nextPageToken}";
                    }
                    var contentsJson = httpClient.GetStringAsync(folderContentsUri, cancellationToken).GetAwaiter().GetResult();
                    var contents = (JObject)JsonConvert.DeserializeObject(contentsJson);
                    nextPageToken = (string)contents["nextPageToken"];
                    foreach (var file in (JArray)contents["files"])
                    {
                        var id = (string)file["id"];
                        var name = (string)file["name"];
                        (bool, string) newValue = CheckZipFile(name);

                        if(newValue.Item1)
                        {

                            var fileName = Path.Combine(Directory.GetCurrentDirectory(), "Sources", name);
                            var extractFileName = Path.Combine(Directory.GetCurrentDirectory(), "Sources", newValue.Item2);
                    
                            if(!CheckFileExist(fileName))
                            {
                                var downloadUri = $"https://drive.google.com/uc?export=download&id={id}";
                                //var downloadUri = "https://drive.google.com/uc?export=download&id=1_7DEkjboKGermJoHtsN-EQsjZa9jeOVz";

                                var wc = new System.Net.WebClient();
                                wc.DownloadFile(downloadUri, fileName);

                                ZipFile.ExtractToDirectory(fileName, extractFileName);

                                Console.WriteLine($"{id}:{name}");
                            }
                        }
                    }
                } while (!String.IsNullOrEmpty(nextPageToken)); 

                
                return 1; 
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"{ex.Message}");
                throw new System.ApplicationException();
            }
            
        }

        public static bool CheckFileExist(string filePath)
        {
            return File.Exists(filePath);
        }

        public static (bool, string) CheckZipFile(string fileName)
        {
            if(!string.IsNullOrEmpty(fileName))
            {
                bool check = fileName.EndsWith("zip");
                if (check)
                {
                    return (true, fileName[0..^4]);
                }
            }
            return (false, null);
        }

    }
}