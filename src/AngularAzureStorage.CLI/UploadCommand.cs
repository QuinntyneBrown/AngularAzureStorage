using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace AngularAzureStorage.CLI
{
    public class UploadCommand
    {
        public class Request : IRequest
        {
            public string Directory { get; set; }
        }
        
        public class Handler : IRequestHandler<Request>
        {
            public Handler()
            {

            }

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {
                return await Task.FromResult(new Unit());
            }

        }
    }
}
