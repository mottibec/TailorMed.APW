using System;
using System.Collections.Generic;
using System.Text;

namespace TailorMed.APW.Scrapers
{
    public class AssistanceProgram
    {
        public string FoundationName { get; set; }

        public string AssistanceProgramName { get; set; }

        public ProgramStatus ProgramStatus { get; set; }

        public string GrantAmount { get; set; }
    }

    public enum ProgramStatus
    {
        Open,
        Closed
    }
}
