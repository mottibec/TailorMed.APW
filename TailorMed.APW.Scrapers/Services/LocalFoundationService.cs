using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TailorMed.APW.Scrapers;

namespace TailorMed.APW.Services
{
    public class LocalFoundationService : IFoundationService
    {
        //todo save to db
        public Task SaveAssistanceProgram(string foundationName, AssistanceProgram[] programs)
        {
            var result = new
            {
                FoundationName = foundationName,
                Programs = programs,
            };

            return File.WriteAllTextAsync(
                @"C:\Users\mojo\source\repos\TailorMed.APW\data\foundations.json",
                JsonConvert.SerializeObject(new object[] { result }));
        }
    }
}
