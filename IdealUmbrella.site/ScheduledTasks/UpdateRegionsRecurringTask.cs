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
        private readonly IContentService _contentService;
        private readonly ICoreScopeProvider _scopeProvider;
        private readonly ILogger<UpdateRegionsRecurringTask> _logger;

        private static TimeSpan HowOftenToRepeatScheduledTask => TimeSpan.FromMinutes(5);
        private static TimeSpan DelayBeforeStart => TimeSpan.FromMinutes(1);


        public UpdateRegionsRecurringTask(IRuntimeState runtimeState,
        IContentService contentService,
        IServerRoleAccessor serverRoleAccessor,
        IProfilingLogger profilingLogger,
        ILogger<UpdateRegionsRecurringTask> logger,
        ICoreScopeProvider scopeProvider) : base(logger, HowOftenToRepeatScheduledTask, DelayBeforeStart)
        {
            _runtimeState = runtimeState;
            _contentService = contentService;
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





            _logger.LogInformation($"The task {nameof(UpdateRegionsRecurringTask)} has completed");
            return Task.CompletedTask;
        }
    }
}
