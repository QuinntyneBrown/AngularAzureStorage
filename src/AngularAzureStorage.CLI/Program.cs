using CommandLine;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AngularAzureStorage.CLI
{
    class Program
    {
        private readonly IMediator _mediator;

        public Program(IMediator mediator)
        {
            _mediator = mediator;
        }
        static void Main(string[] args)
        {
            var serviceProvider = BuildServiceProvider();

            var logger = serviceProvider.GetService<ILoggerFactory>()
                .CreateLogger<Program>();

            var mediator = serviceProvider.GetService<IMediator>();

            new Program(mediator).ProcessArgs(args);
        }

        static ServiceProvider BuildServiceProvider()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddLogging();

            services.AddMediatR(typeof(Program));

            return services.BuildServiceProvider();
        }

        public int ProcessArgs(string[] args)
        {
            int lastArg = 0;

            var command = args[lastArg];

            var appArgs = (lastArg + 1) >= args.Length ? Enumerable.Empty<string>() : args.Skip(lastArg + 1).ToArray();

            var request = new UploadCommand.Request();

            Parser.Default.ParseArguments<Options>(args)
                .MapResult(x =>
                {
                    request.Directory = string.IsNullOrEmpty(x.Directory) ? System.Environment.CurrentDirectory : x.Directory;
                    request.CloudStorageConnectionString = x.CloudStorageConnectionString;
                    return 1;
                }, x => 0);

            _mediator.Send(request).Wait();
            
            return 1;
        }

        private static bool IsArg(string candidate, string longName) => IsArg(candidate, shortName: null, longName: longName);

        private static bool IsArg(string candidate, string shortName, string longName)
        {
            return (shortName != null && candidate.Equals("-" + shortName)) || (longName != null && candidate.Equals("--" + longName));
        }        
    }
}
