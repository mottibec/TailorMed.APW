using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TailorMed.APW.Scrapers;

namespace TailorMed.APW.Services
{
    public interface IFoundationService
    {
        Task SaveAssistanceProgram(string foundationName, AssistanceProgram[] programs);
    }
}
