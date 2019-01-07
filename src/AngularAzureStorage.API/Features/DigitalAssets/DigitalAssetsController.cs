using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AngularAzureStorage.API.Features.DigitalAssets
{
    [ApiController]
    [Route("api/digitalAssets")]
    public class DigitalAssetsController
    {
        private readonly IMediator _mediator;

        public DigitalAssetsController(IMediator mediator) => _mediator = mediator;

        [HttpPost("upload"), DisableRequestSizeLimit]
        public async Task<ActionResult<UploadDigitalAssetCommand.Response>> Upload()
        {

            try
            {
                var response = await _mediator.Send(new UploadDigitalAssetCommand.Request());

                return response;
            }catch (Exception e)
            {
                throw e;
            }
        }        
    }
}
