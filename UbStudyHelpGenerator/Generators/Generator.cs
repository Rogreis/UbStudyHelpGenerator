using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UbStudyHelpGenerator.Classes.EventsControl;

namespace UbStudyHelpGenerator.Generators
{
    public abstract class Generator
    {
        public abstract void Generate(string filesPath, string outputFiles);
    }
}
