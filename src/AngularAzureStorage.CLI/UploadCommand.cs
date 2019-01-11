using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace AngularAzureStorage.CLI
{
    public class UploadCommand
    {
        public class Request : IRequest
        {
            public string Directory { get; set; }
            public string CloudStorageConnectionString { get; set; }
             
        }
        
        public class Handler : IRequestHandler<Request>
        {
            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {
                var storageAccount = CloudStorageAccount.Parse(request.CloudStorageConnectionString);
                var blobClient = storageAccount.CreateCloudBlobClient();
                var container = blobClient.GetContainerReference($"$web");

                BlobContainerPermissions permissions = await container.GetPermissionsAsync();

                permissions.PublicAccess = BlobContainerPublicAccessType.Blob;

                await container.SetPermissionsAsync(permissions);

                foreach(var file in Directory.GetFiles(request.Directory))
                    await UploadAsync(file, container);
                
                return new Unit();
            }

            public async Task UploadAsync(string path, CloudBlobContainer container)
            {                
                using (var targetStream = new FileStream(path, FileMode.Open))
                {
                    var filename = Path.GetFileName(path);

                    var blockBlob = container.GetBlockBlobReference(filename);
                    
                    new FileExtensionContentTypeProvider().TryGetContentType(path, out string contentType);

                    blockBlob.Properties.ContentType = contentType;

                    targetStream.Position = 0;

                    await blockBlob.UploadFromStreamAsync(targetStream);
                }
            }
        }


    }
}
