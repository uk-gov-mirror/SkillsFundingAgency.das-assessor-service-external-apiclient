using System.Windows;
using Microsoft.Extensions.Configuration;
using System;
using SFA.DAS.AssessorService.ExternalApi.Client.Configuration;

namespace SFA.DAS.AssessorService.ExternalApi.Client
{
    public partial class App : Application
    {
        public static IConfiguration Configuration { get; private set; }
        public static ApiSettings ApiSettings { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();

            ApiSettings = Configuration.GetSection("ApiSettings").Get<ApiSettings>();

            base.OnStartup(e);
        }
    }
}
