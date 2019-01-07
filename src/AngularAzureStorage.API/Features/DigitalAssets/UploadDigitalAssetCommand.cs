using AngularAzureStorage.Core.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace AngularAzureStorage.API.Features.DigitalAssets
{
    public class UploadDigitalAssetCommand
    {
        public class Request : IRequest<Response> { }

        public class Response
        {
            public List<string> DigitalAssetUrls { get;set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IConfiguration _configuration;
            private readonly IHttpContextAccessor _httpContextAccessor;
            
            public Handler(IConfiguration configuration, IHttpContextAccessor httpContextAccessor) {
                _configuration = configuration;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {

                var httpContext = _httpContextAccessor.HttpContext;
                var defaultFormOptions = new FormOptions();
                var digitalAssetUrls = new List<string>();
                var storageAccount = CloudStorageAccount.Parse(_configuration["Storage:DefaultConnection:StorageConnectionString"]);
                var blobClient = storageAccount.CreateCloudBlobClient();
                var container = blobClient.GetContainerReference($"$web");

                BlobContainerPermissions permissions = await container.GetPermissionsAsync();

                permissions.PublicAccess = BlobContainerPublicAccessType.Blob;

                await container.SetPermissionsAsync(permissions);

                if (!MultipartRequestHelper.IsMultipartContentType(httpContext.Request.ContentType))
                    throw new Exception($"Expected a multipart request, but got {httpContext.Request.ContentType}");

                var mediaTypeHeaderValue = MediaTypeHeaderValue.Parse(httpContext.Request.ContentType);

                var boundary = MultipartRequestHelper.GetBoundary(
                    mediaTypeHeaderValue,
                    defaultFormOptions.MultipartBoundaryLengthLimit);

                var reader = new MultipartReader(boundary, httpContext.Request.Body);

                var section = await reader.ReadNextSectionAsync();

                while (section != null)
                {
                    var hasContentDispositionHeader = ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out ContentDispositionHeaderValue contentDisposition);

                    if (hasContentDispositionHeader)
                    {
                        if (MultipartRequestHelper.HasFileContentDisposition(contentDisposition))
                        {
                            using (var targetStream = new MemoryStream())
                            {
                                var targetStreamLength = targetStream.Length;

                                await section.Body.CopyToAsync(targetStream);

                                var filename = $"{contentDisposition.FileName}".Trim(new char[] { '"' }).Replace("&", "and");

                                var blockBlob = container.GetBlockBlobReference(filename);

                                blockBlob.Properties.ContentType = section.ContentType;

                                await blockBlob.UploadFromStreamAsync(targetStream);

                                digitalAssetUrls.Add($"{blockBlob.StorageUri.PrimaryUri}");
                            }
                        }
                    }
   
                    section = await reader.ReadNextSectionAsync();
                }
                
                return new Response()
                {
                    DigitalAssetUrls = digitalAssetUrls
                };
            }
        }
    }
}
