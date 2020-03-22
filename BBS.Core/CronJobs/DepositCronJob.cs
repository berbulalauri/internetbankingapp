using BBS.Interfaces;
using BBS.Interfaces.CronJobs.Configurations;
using BBS.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BBS.Core.CronJobs
{
    public class DepositCronJob : CronJobService, IDisposable
    {
        private readonly ILogger _logger;
        private readonly IServiceScope _scope;

        public DepositCronJob(IScheduleConfig<DepositCronJob> config, ILogger logger, IServiceProvider service)
            : base(config.CronExpression, config.TimeZoneInfo)
        {
            _logger = logger;
            _scope = service.CreateScope();
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.Info("Deposit CronJob 1 starts.");
            return base.StartAsync(cancellationToken);
        }

        public override async Task DoWork(CancellationToken cancellationToken)
        {
            _logger.Info($"{DateTime.Now:hh:mm:ss} Deposit CronJob 1 is working.");

            using (var depositService = _scope.ServiceProvider.GetRequiredService<IDepositService>())
            {
                await depositService.AddDepositPercents();
            }

            await Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.Info("Deposit CronJob 1 is stopping.");
            return base.StopAsync(cancellationToken);
        }

        public override void Dispose()
        {
            _scope?.Dispose();
            base.Dispose();
        }
    }
}
