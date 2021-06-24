using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace ConsoleTvMaze
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("ConsoleTvMaze");

            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var builder = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env}.json", true, true)
                .AddEnvironmentVariables();

            var config = builder.Build();

            AppSettings settings = new AppSettings();
            config.GetSection("AppSettings").Bind(settings);

            var services = new TvmazeService();
            await services.Go(settings.TvIncludes);

            Console.Write("Press <any> key to continue");
            Console.ReadKey();
        }
    }
}
