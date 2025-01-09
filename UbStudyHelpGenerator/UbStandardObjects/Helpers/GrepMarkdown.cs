using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UbStudyHelpGenerator.UbStandardObjects.Helpers
{

    internal class GrepCommand
    {
        public string Name { get; set; }
        public string Command { get; set; }

        public GrepCommand() { }

        public GrepCommand(string name, string command)
        {
            Command = command;
            Name = name;
        }
    }

    internal class ParagraphsMdFound
    {

        public Int16 Paper = 0;
        public Int16 Section = 0;
        public Int16 ParagraphNo = 0;

        public ParagraphsMdFound(Int16 paper, Int16 section, Int16 paragraphNo)
        {
            Paper = paper;
            Section = section;
            ParagraphNo = paragraphNo;
        }

        public override string ToString()
        {
        return $"{Paper}:{Section}-{ParagraphNo}";
        }
    }


    internal class GrepMarkdown
    {
        public List<GrepCommand> Commands = new List<GrepCommand>();

        private ParagraphsMdFound ExtractValuesFromParString(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentNullException(nameof(input), "Input string cannot be null or empty.");
            }

            const string prefix = "Par_";
            int startIndex = input.IndexOf(prefix) + prefix.Length;

            if (startIndex < 0)
            {
                throw new ArgumentException("Input string does not contain the expected 'Par_' prefix.");
            }

            string[] parts = input.Substring(startIndex).Split('_', '.');

            if (parts.Length < 3)
            {
                throw new ArgumentException("Invalid format. Expected three parts separated by underscores.");
            }

            if (!short.TryParse(parts[0], out short paper) ||
                !short.TryParse(parts[1], out short section) ||
                !short.TryParse(parts[2], out short paragraph))
            {
                throw new ArgumentException("Failed to parse one or more values as integers.");
            }
            else
            {
                return new ParagraphsMdFound(paper, section, paragraph);
            }
         }

        public List<ParagraphsMdFound> ExecuteGrepCommand(string repositoryPath, string command)
        {
            var result = new List<ParagraphsMdFound>();

            List<string> commands = new List<string>()
            {
                "cd " + RunScripts.GetUnixPath(repositoryPath),
                command
            };

            bool errorOcurred = false;
            List<string> lines= RunScripts.BashExecuteGetLines(commands, ref errorOcurred);
            if (errorOcurred) return null;

            foreach(string line in lines)
                result.Add(ExtractValuesFromParString(line));

             return result;
        }


        public GrepMarkdown()
        {
            Commands.Add(new GrepCommand("Formas verbais segunda pessoa",
                "grep -E -r '\\b(ai|ais|ardes|areis|áreis|aríeis|ásseis|astes|áveis|des|ei|eis|erdes|ereis|êreis|eríeis|êsseis|estes|i|íeis|irdes|ireis|íreis|iríeis|is|ísseis|istes)\\b' --include=\"*.md\" ."));
        }
    }
}
