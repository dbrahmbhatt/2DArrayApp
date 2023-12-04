using InvestCloud._2DMatrix.ConsoleApp.Interfaces;
using InvestCloud._2DMatrix.ConsoleApp.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using System.IO;

namespace InvestCloud._2DMatrix.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Build())
                .WriteTo.Console()
                .CreateLogger();

            CreateApplicationHostBuilder(args)
                .UseSerilog()
                .Build()
                .Run();
        }

        public static IHostBuilder CreateApplicationHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<IMatrixProvider, MatrixProvider>();
                    services.AddSingleton<IMatrixProcessor, MatrixProcessor>();
                    services.AddSingleton<INumbersApiClient, NumbersApiClient>();
                    services.AddHostedService<ApplicationHostService>();
                })
                .ConfigureLogging((hostContext, logging) => {
                    foreach (ServiceDescriptor serviceDescriptor in logging.Services)
                    {
                        if (serviceDescriptor.ImplementationType == typeof(Microsoft.Extensions.Logging.Console.ConsoleLoggerProvider))
                        {
                            logging.Services.Remove(serviceDescriptor);
                            break;
                        }
                    }
                });
        }
    }
}
