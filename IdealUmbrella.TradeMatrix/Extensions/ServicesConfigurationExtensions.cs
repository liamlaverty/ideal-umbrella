using IdealUmbrella.TradeMatrix.Services.Impl;
using IdealUmbrella.TradeMatrix.Services.Interface;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdealUmbrella.TradeMatrix.Extensions
{
    public static class ServicesConfigurationExtensions
    {
        public static void AddTradeMatrixServices(
            this IServiceCollection services)
        {
            services.AddScoped<ITradeMatrixService, TradeMatrixService>();
        }

    }
}
