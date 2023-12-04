using InvestCloud._2DMatrix.ConsoleApp.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace InvestCloud._2DMatrix.ConsoleApp
{
    class ApplicationHostService : IHostedService
    {
        IMatrixProvider matrixProvider;
        IMatrixProcessor matrixProcessor;
        ILogger<ApplicationHostService> logger;
        IHostApplicationLifetime hostApplicationLifetime;
        public ApplicationHostService(IMatrixProvider matrixProvider, 
            IMatrixProcessor matrixProcessor, 
            ILogger<ApplicationHostService> logger,
            IHostApplicationLifetime hostApplicationLifetime)
        {
            this.matrixProvider = matrixProvider;
            this.matrixProcessor = matrixProcessor;
            this.logger = logger;
            this.hostApplicationLifetime = hostApplicationLifetime;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            hostApplicationLifetime.ApplicationStarted.Register(OnAppStarted);
            hostApplicationLifetime.ApplicationStopping.Register(OnAppStopping);
            return Task.CompletedTask;
        }

        private void OnAppStarted()
        {
            Task.Run(() => {
                try
                {
                    logger.Log(LogLevel.Information, "Starting InvestCloud 2DMatrix Console App");


                    int[,] matrixData = matrixProvider.GetMatrixDataset(Constants.DATASET_A).Result;

                    int[,] multipliedMatrix = matrixProcessor.MultiplyMatrix(matrixData, matrixData);

                    logger.Log(LogLevel.Information, "\n" +
                        "Matrix Data Received.\n");
                }
                catch (Exception ex)
                {
                    logger.Log(LogLevel.Information, "\nApplication Error: {0}.", ex.Message);
                }
            });
        }

        private void OnAppStopping()
        {
            Environment.Exit(0);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
