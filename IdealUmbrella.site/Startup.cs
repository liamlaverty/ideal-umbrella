using IdealUmbrella.DataConnector.CountryData;
using IdealUmbrella.site.Models.Config;
using IdealUmbrella.site.Services.ContentServices.Impl;
using IU.ClimateTrace.Downloader.Extensions;
using IdealUmbrella.TradeMatrix.Extensions;

namespace IdealUmbrella.site
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _config;

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup" /> class.
        /// </summary>
        /// <param name="webHostEnvironment">The web hosting environment.</param>
        /// <param name="config">The configuration.</param>
        /// <remarks>
        /// Only a few services are possible to be injected here https://github.com/dotnet/aspnetcore/issues/9337.
        /// </remarks>
        public Startup(IWebHostEnvironment webHostEnvironment, IConfiguration config)
        {
            _env = webHostEnvironment ?? throw new ArgumentNullException(nameof(webHostEnvironment));
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <remarks>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940.
        /// </remarks>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddClimateTraceDownloaderServices();
            services.AddTradeMatrixServices();

            services.AddUmbraco(_env, _config)
                .AddBackOffice()
                .AddWebsite()
                .AddDeliveryApi()
                .AddComposers()
                .Build();

            // Add configs
            services.Configure<MapboxConfig>(_config.GetSection(MapboxConfig.ConfigName));

            // add other services
            services.AddTransient<ICountryDataCsvService, CountryDataCsvService>();
            services.AddTransient<IRegionContentService, RegionContentService>();


            // Add recurring hosted services
            // services.AddHostedService<UpdateRegionsRecurringTask>();

            // TODO: DownloadClimateTraceData requires some AddScoped<Services>
            // which can't be injected, because AddHostedServices are singletons
            // see https://learn.microsoft.com/en-us/dotnet/core/extensions/scoped-service?pivots=dotnet-7-0
            // services.AddHostedService<DownloadClimateTraceDataRecurringTask>();
        }

        /// <summary>
        /// Configures the application.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <param name="env">The web hosting environment.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseUmbraco()
                .WithMiddleware(u =>
                {
                    u.UseBackOffice();
                    u.UseWebsite();
                })
                .WithEndpoints(u =>
                {
                    u.UseInstallerEndpoints();
                    u.UseBackOfficeEndpoints();
                    u.UseWebsiteEndpoints();
                });
        }
    }
}
