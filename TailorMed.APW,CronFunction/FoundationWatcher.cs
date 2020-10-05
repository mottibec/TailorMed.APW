using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TailorMed.APW.Scrapers;
using TailorMed.APW.Services;

namespace TailorMed.APW.CronFunction
{
    public static class FoundationWatcher
    {
        /// <summary>
        /// An azure function with a cron trigger to run when the current minute is divided by 1 (every minute)
        /// </summary>
        /// <param name="timer"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [FunctionName("FoundationWatcher")]
        public static async Task Run([TimerTrigger("0 */1 * * * *")] TimerInfo timer, ILogger log)
        {
            log.LogInformation($"FoundationWatcher Function executed at: {DateTime.Now}");
            var configurations = await GetActiveFoundationConfiguration();

            log.LogInformation($"Loaded {configurations.Length} Foundation configurations");
            var scrapper = new FoundationScraper();
            var service = GetFoundationService();

            //todo move to queue and process elsewhere
            foreach (var foundationConfiguration in configurations)
            {
                log.LogInformation($"Getting Assistance Programs from {foundationConfiguration.FoundationName}");
                var programs = await scrapper.GetPrograms(foundationConfiguration);
                log.LogInformation($"Received {programs.Length} programs from {foundationConfiguration.FoundationName}");

                //todo move to queue and process elsewhere
                await service.SaveAssistanceProgram(foundationConfiguration.FoundationName, programs);
                log.LogInformation($"Saved {programs.Length} programs from {foundationConfiguration.FoundationName}");
            }
            log.LogInformation($"FoundationWatcher Function complated at: {DateTime.Now}");
        }

        //TODO load from a database
        private static async Task<FoundationConfiguration[]> GetActiveFoundationConfiguration()
        {
            var json = await File.ReadAllTextAsync(@"C:\Users\mojo\source\repos\TailorMed.APW\data\configuration.json");
            return JsonConvert.DeserializeObject<FoundationConfiguration[]>(json)
                .Where(fc => fc.IsActive)
                .ToArray();
        }

        //TODO handle in ioc container
        private static IFoundationService GetFoundationService()
        {
            return new LocalFoundationService();
        }
    }
}
