﻿
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;

namespace WebApi.BackgroundServices
{
    public class RedirectService : BackgroundService
    {
        private const int WaitTimeInMinutes = 2;
        private readonly IServiceScopeFactory serviceScopeFactory;
        public RedirectService(IServiceScopeFactory serviceScopeFactory)
        {
            this.serviceScopeFactory = serviceScopeFactory;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = serviceScopeFactory.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetService<AppDbContext>();
                    var queues = dbContext.Queues.ToList();
                    foreach (var queue in queues)
                    {
                        if ((DateTime.Now - queue.StartDate).TotalMinutes == WaitTimeInMinutes)
                        {
                            queue.Status = Enums.QueueStatus.Skipped;
                            int freeMinutes = (queue.EndDate - DateTime.Now).Minutes;
                            var queuesToRedirect = dbContext.Queues
                                .Where(x => x.OperationId == queue.OperationId 
                                && x.Status == (int)(Enums.QueueStatus.Planned)
                                && (x.EndDate-x.StartDate).Minutes<=freeMinutes);
                            dbContext.SaveChanges();
                        }
                    }
                }

                await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
            }
        }
    }
}
