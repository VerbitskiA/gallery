using Gallery.Abstractions.Repositories;
using Gallery.Abstractions.Services;
using Gallery.Core.Data;
using Gallery.Services.Ftp;
using Gallery.Services.Photo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace Gallery.Telegram.Bot
{
    public class Program
    {
        public static IConfigurationRoot Configuration { get; private set; }            
                
        static async Task Main(string[] args)
        {
            using IHost host = CreateHostBuilder(args).Build();
            await host.RunAsync();
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
                        Host.CreateDefaultBuilder(args)
                        .ConfigureAppConfiguration((hostingContext, configuration) =>
                        {
                            configuration.Sources.Clear();
                            configuration
                            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

                            IConfigurationRoot configurationRoot = configuration.Build();
                            Configuration = configurationRoot;
                        }).ConfigureServices((services) =>
                        {
                            string connection = Configuration.GetConnectionString("NpgTestSqlConnection");
                            services.AddDbContext<PostgresContext>(options => options.UseNpgsql(connection));

                            services.AddHostedService<ConsoleService>();
                            services.AddTransient<IPhotoRepository, PhotoRepository>();
                            services.AddTransient<IPhotoService, PhotoService>();
                            services.AddTransient<IFtpService, FtpLocalService>();
                        });
               
    }
}
