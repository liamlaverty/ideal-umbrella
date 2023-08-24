using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Scoping;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Infrastructure.HostedServices;

namespace IdealUmbrella.site.ScheduledTasks
{
    public class DownloadClimateTraceDataRecurringTask : RecurringHostedServiceBase
    {

        private readonly IRuntimeState _runtimeState;
        private readonly ICoreScopeProvider _scopeProvider;
        private readonly ILogger<DownloadClimateTraceDataRecurringTask> _logger;


        private static TimeSpan HowOftenToRepeatScheduledTask => TimeSpan.FromHours(6);
        private static TimeSpan DelayBeforeStart => TimeSpan.FromMinutes(
#if DEBUG
            1 // run the task on startup in debug/dev
#else
            60 // leave the task 60 minutes after startup if in production
#endif
            );

        public DownloadClimateTraceDataRecurringTask(
               IRuntimeState runtimeState,
               ILogger<DownloadClimateTraceDataRecurringTask> logger,
               ICoreScopeProvider scopeProvider)
       : base(logger, HowOftenToRepeatScheduledTask, DelayBeforeStart)
        {
            _runtimeState = runtimeState;
            _scopeProvider = scopeProvider;
            _logger = logger;
        }

        public override Task PerformExecuteAsync(object? state)
        {
            if (_runtimeState.Level is not RuntimeLevel.Run)
            {
                return Task.CompletedTask;
            }
            using ICoreScope scope = _scopeProvider.CreateCoreScope();
            _logger.LogInformation($"The task {nameof(DownloadClimateTraceDataRecurringTask)} has started");




            _logger.LogInformation($"The task {nameof(DownloadClimateTraceDataRecurringTask)} has completed");
            return Task.CompletedTask;
        }

    }
}
