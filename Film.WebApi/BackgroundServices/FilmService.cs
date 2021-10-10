using Film.Business;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Film.WebApi.BackgroundServices
{
    public class FilmService : BackgroundService
    {
        private readonly IHostApplicationLifetime hostLifetime;
        protected readonly IServiceProvider serviceProvider;

        public FilmService(IServiceProvider serviceProvider,
            IHostApplicationLifetime hostLifetime)
        {
            this.serviceProvider = serviceProvider;
            this.hostLifetime = hostLifetime;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await ReadFilms();
                break;
            }
        }

        private async Task ReadFilms()
        {
            var file = File.OpenText(@"movielist.csv");
            try
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    var leitoBusiness = scope.ServiceProvider.GetRequiredService<FilmsBusiness>();
                    leitoBusiness.ReadFilms(file);
                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
