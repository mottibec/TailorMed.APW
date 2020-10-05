using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.XPath;
using OpenScraping;
using OpenScraping.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TailorMed.APW.Scrapers.Extensions;

namespace TailorMed.APW.Scrapers
{
    public class FoundationScraper : IFoundationScraper
    {
        /// <summary>
        /// Scrapes the foundation page and finds all links to the program details, then iterates over them and scrapes the details
        /// </summary>
        /// <param name="foundation"></param>
        /// <returns></returns>
        public async Task<AssistanceProgram[]> GetPrograms(FoundationConfiguration foundation)
        {
            foundation = foundation ?? throw new ArgumentNullException(nameof(foundation));

            var config = Configuration.Default.WithDefaultLoader().WithXPath();
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(foundation.Url);
            var programDetailUrls = GetProgramsDetailUrl(foundation, document);
            var programs = await Task.WhenAll(programDetailUrls.Select(url => GetAssistanceProgram(foundation, url)));
            return programs.ToArray();
        }

        private IEnumerable<string> GetProgramsDetailUrl(
            FoundationConfiguration foundation,
            IDocument document)
        {
            //using the XPath from the configuration to extract the links to the assistance program details
            return document.Body.SelectSingleNode(foundation.ProgramListExtractor)
                .ChildNodes
                .Select(node => node as IElement)
                .Select(element => element.SelectSingleNode(foundation.ProgramDetailsLinkExtractor) as IHtmlAnchorElement)
                .Select(anchorElement => anchorElement.Href)
                .ToArray();
        }

        public async Task<AssistanceProgram> GetAssistanceProgram(FoundationConfiguration foundation, string url)
        {
            foundation = foundation ?? throw new ArgumentNullException(nameof(foundation));
            url = url ?? throw new ArgumentNullException(nameof(url));

            var config = Configuration.Default.WithDefaultLoader().WithXPath();
            var programDetailContext = BrowsingContext.New(config);
            var programDetailDocument = await programDetailContext.OpenAsync(url);
            var programNameElemment = programDetailDocument.Body.SelectSingleNode(foundation.NameExtractor);
            var fundingAmountElemment = programDetailDocument.Body.SelectSingleNode(foundation.AmountExtractor);
            var statusElemment = programDetailDocument.Body.SelectSingleNode(foundation.StatusExtractor);
            var name = programNameElemment.TextContent.CleanHtmlContent();
            var fundingAmount = fundingAmountElemment.TextContent.CleanHtmlContent();
            var status = statusElemment.TextContent.CleanHtmlContent();

            //var eligibleTreatments = programDetailsElement
            //    .QuerySelector(".treatments")
            //    .QuerySelectorAll("ul li")
            //    .Select(li => li.TextContent);

            static ProgramStatus ConvertToStatus(string statusStr)
            {
                return statusStr.Contains("open", StringComparison.InvariantCultureIgnoreCase)
                    ? ProgramStatus.Open
                    : ProgramStatus.Closed;
            }

            return new AssistanceProgram
            {
                GrantAmount = fundingAmount,
                AssistanceProgramName = name,
                ProgramStatus = ConvertToStatus(status)
            };
        }
    }
}
