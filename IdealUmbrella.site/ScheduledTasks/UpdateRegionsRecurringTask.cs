using IdealUmbrella.site.Services.ContentServices.Impl;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Scoping;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Sync;
using Umbraco.Cms.Infrastructure.HostedServices;

namespace IdealUmbrella.site.ScheduledTasks
{
    public class UpdateRegionsRecurringTask : RecurringHostedServiceBase
    {
        private readonly IRuntimeState _runtimeState;
        private readonly ICoreScopeProvider _scopeProvider;
        private readonly IRegionContentService _regionContentService;
        private readonly ILogger<UpdateRegionsRecurringTask> _logger;

        private static TimeSpan HowOftenToRepeatScheduledTask => TimeSpan.FromMinutes(10);
        private static TimeSpan DelayBeforeStart => TimeSpan.FromMinutes(
#if DEBUG
            1 // run the task on startup in debug/dev
#else
            20 // leave the task 20 minutes after startup if in production
#endif
            );


        public UpdateRegionsRecurringTask(
            IRuntimeState runtimeState,
            IServerRoleAccessor serverRoleAccessor,
            IProfilingLogger profilingLogger,
            IRegionContentService regionContentService,
            ILogger<UpdateRegionsRecurringTask> logger,
            ICoreScopeProvider scopeProvider) 
            : base(logger, HowOftenToRepeatScheduledTask, DelayBeforeStart)
        {
            _runtimeState = runtimeState;
            _regionContentService = regionContentService;
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
            _logger.LogInformation($"The task {nameof(UpdateRegionsRecurringTask)} has started");


            _regionContentService.UpdateRegionsFromCsvFile();


            _logger.LogInformation($"The task {nameof(UpdateRegionsRecurringTask)} has completed");
            return Task.CompletedTask;
        }
    }
}
