using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApp
{
    [Export]
    public sealed class Program
    {
        private readonly IEnumerable<ExportFactory<Command, ICommandDescriptor>> commands;
        private readonly ILogger logger;


        [ImportingConstructor]
        public Program([ImportMany] IEnumerable<ExportFactory<Command, ICommandDescriptor>> commands, [Import] ILogger<Program> logger)
        {
            this.commands = commands;
            this.logger = logger;
        }

        static async Task Main(string[] args)
        {
            var catalog = new ApplicationCatalog();

            using var container = new CompositionContainer(catalog);
            await container
                .GetExportedValue<Program>()
                .Run(args);
        }

        public async Task Run(string[] args)
        {
            var command = commands
                .Single(c => c.Metadata.Name == "hello")
                .CreateExport()
                .Value;

            await command.Execute();
        }
    }

    [Export]
    public class Startup
    {
        [Export]
        public ILogger<Program> Logger { get; }

        public Startup()
        {
            var services = new ServiceCollection();
            services.AddLogging();

            var locator = services.BuildServiceProvider();
            Logger = locator.GetRequiredService<ILogger<Program>>();
        }
    }

    [Command("hello")]
    public sealed class HelloCommand : Command
    {
        public override async Task Execute()
        {
            await Console.Out.WriteLineAsync("HELLO, WORLD!");
        }
    }
}
