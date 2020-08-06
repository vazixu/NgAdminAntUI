using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Filters;

namespace NgAdminAntUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var pathLog = Path.Combine("Logs", "log-.txt");
            var pathApiLog = Path.Combine("Logs", "apilog-.txt");
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Information)
            .WriteTo.File(pathLog, rollingInterval: RollingInterval.Day)
            .WriteTo.Logger(lc => lc
                    //.Filter.ByExcluding(Matching.FromSource("Microsoft"
                    .Filter.ByIncludingOnly(Matching.FromSource("NgAdminAntUI.Controllers"))
                    .WriteTo.File(pathApiLog, rollingInterval: RollingInterval.Day))
            .CreateLogger();

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
