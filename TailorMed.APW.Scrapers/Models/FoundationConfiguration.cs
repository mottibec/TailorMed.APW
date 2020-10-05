using System;
using System.Collections.Generic;
using System.Text;

namespace TailorMed.APW.Scrapers
{
    public class FoundationConfiguration
    {
        public FoundationConfiguration(
            string name,
            string url,
            string programsExtractor,
            string programLinkExtractor,
            string nameExtractor,
            string amountExtractor,
            string statusExtractor)
        {
            FoundationName = name;
            Url = url;
            ProgramListExtractor = programsExtractor;
            ProgramDetailsLinkExtractor = programLinkExtractor;
            NameExtractor = nameExtractor;
            AmountExtractor = amountExtractor;
            StatusExtractor = statusExtractor;
        }

        public string FoundationName { get; set; }

        public string Url { get; set; }

        public string ProgramListExtractor { get; set; }

        public string ProgramDetailsLinkExtractor { get; set; }

        public string NameExtractor { get; set; }

        public string AmountExtractor { get; set; }

        public string StatusExtractor { get; set; }

        public bool IsActive { get; set; }
    }
}
