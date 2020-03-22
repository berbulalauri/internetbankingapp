using BBS.Interfaces;
using BBS.Interfaces.CronJobs.Configurations;
using BBS.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BBS.Core.Services.CronJobs
{
    public class LoanCronJob : CronJobService, IDisposable
    {
        private readonly ILogger _logger;
        private IServiceScope _scope;
        private readonly IServiceProvider provider;

        public LoanCronJob(IScheduleConfig<LoanCronJob> config, ILogger logger, IServiceProvider service)
            : base(config.CronExpression, config.TimeZoneInfo)
        {
            _logger = logger;
            _scope = service.CreateScope();
            provider = service;
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.Info("Loan CronJob starts.");
            await base.StartAsync(cancellationToken);
        }

        public override async Task DoWork(CancellationToken cancellationToken)
        {
            _logger.Info($"{DateTime.Now:hh:mm:ss} Loan CronJob is working.");
            _scope = provider.CreateScope();

            using (var loanService = _scope.ServiceProvider.GetRequiredService<ILoanService>())
            {
                try
                {
                    await loanService.PayLoans();
                }
                catch (Exception e)
                {
                    _logger.Warn("Exception thrown while paying loan");
                    _logger.Error(e);
                }
            }

            await Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.Info("Loan CronJob is stopping.");
            return base.StopAsync(cancellationToken);
        }

        public override void Dispose()
        {
            _scope?.Dispose();
            base.Dispose();
        }
    }
}