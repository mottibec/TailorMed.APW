using NUnit.Framework;
using System.Threading.Tasks;

namespace TailorMed.APW.Scrapers.Tests
{
    public class Tests
    {
        private FoundationConfiguration _healthwellFoundation;

        [SetUp]
        public void Setup()
        {
            var name = "healthwell foundation";
            var url = "https://www.healthwellfoundation.org/disease-funds";
            var programListExtractor = "//body/div[@id='page']/main[@id='main']/section[1]/div[1]/div[1]/ul[2]";
            var programDetailsLinkExtractor = "a[1]";
            var nameExtractor = "//body/div[@id='page']/main[@id='main']/section[@id='fund-intro']/div[1]/div[1]/h1[1]";
            var amountExtractor = "//body/div[@id='page']/main[@id='main']/section[2]/div[1]/div[1]/div[1]/div[2]/div[1]//text()[2]";
            var statusExtractor = "//body/div[@id='page']/main[@id='main']/section[2]/div[1]/div[1]/div[1]/div[1]/div[1]//text()[2]";
            _healthwellFoundation = new FoundationConfiguration(
                name,
                url,
                programListExtractor,
                programDetailsLinkExtractor,
                nameExtractor,
                amountExtractor,
                statusExtractor);
        }

        [Test]
        public async Task Get_All_Programs()
        {
            var scraper = new FoundationScraper();
            var programs = await scraper.GetPrograms(_healthwellFoundation);
            Assert.That(programs.Length, Is.AtLeast(1));
        }

        [Test]
        public async Task Get_Program()
        {
            var scraper = new FoundationScraper();
            var program = await scraper.GetAssistanceProgram(
                _healthwellFoundation,
                "https://www.healthwellfoundation.org/fund/cancer-related-behavioral-health");
            Assert.That(program, Is.Not.Null);
            Assert.That(program.GrantAmount, Is.EqualTo("$2,000"));
            Assert.That(program.ProgramStatus, Is.EqualTo(ProgramStatus.Open));
        }
    }
}