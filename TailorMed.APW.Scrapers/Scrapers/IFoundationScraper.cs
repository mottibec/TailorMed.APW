using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TailorMed.APW.Scrapers
{
    public interface IFoundationScraper
    {
        Task<AssistanceProgram[]> GetPrograms(FoundationConfiguration foundation);

        Task<AssistanceProgram> GetAssistanceProgram(FoundationConfiguration foundation, string url);
    }
}
