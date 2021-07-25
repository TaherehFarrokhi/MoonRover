using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MoonRover.CLI
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.CancelKeyPress += CancelKeyHandler;

            var configuration = BuildConfiguration(args);
            var serviceProvider = ConfigureServices(configuration);

            try
            {
                var controller = serviceProvider.GetRequiredService<IPlutoController>();

                var command = ReadCommand();
                while (!command.Equals("quit", StringComparison.CurrentCultureIgnoreCase))
                {
                    var result = controller.ExecuteCommand(command);

                    WriteOutput(result);
                    command = ReadCommand();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Environment.Exit(-1);
            }
        }

        private static void CancelKeyHandler(object sender, ConsoleCancelEventArgs e)
        {
            Environment.Exit(0);
        }

        private static string ReadCommand()
        {
            Console.Write("Enter command ('quit' or Ctrl+C to exit): ");
            return Console.ReadLine() ?? string.Empty;
        }        
        
        private static void WriteOutput(CommandResult result)
        {   
            Console.WriteLine(result.Failed
                ? $"Pluto stopped at {result.Position}. {result.FailedReason}"
                : $"Pluto moved to {result.Position}");
        }

        private static IConfiguration BuildConfiguration(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false)
                .AddCommandLine(args);

            return builder.Build();
        }

        private static IServiceProvider ConfigureServices(IConfiguration configuration)
        {
            var services = new ServiceCollection();

            services.AddSingleton(new PlutoEnvironment(100, 100));
            services.AddTransient<IPlutoController, PlutoController>();
            services.AddTransient<ILocationCalculator, LocationCalculator>();
            services.AddTransient<IDirectionCalculator, DirectionCalculator>();

            return services.BuildServiceProvider();
        }
    }
}