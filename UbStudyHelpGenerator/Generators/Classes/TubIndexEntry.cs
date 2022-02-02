using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UbStudyHelpGenerator.Generators.Classes
{

    /// <summary>
    /// Detail for index entry as used in UbStudyHelp
    /// </summary>
    public class Detail
    {
        public int DetailType { get; set; }
        public string Text { get; set; } = "";
        public List<string> Links { get; set; } = new List<string>();
    }


    /// <summary>
    /// Index entry for TUB index as used in UbStudyHelp
    /// </summary>
    public class TubIndexEntry
    {
        public string Title { get; set; }
        public List<Detail> Details { get; set; } = new List<Detail>();
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(Title);
            foreach (Detail detail in Details)
            {
                sb.AppendLine($"   {detail.DetailType}  {detail.Text}");
                foreach (string link in detail.Links)
                {
                    sb.AppendLine("      " + link);
                }
            }
            sb.AppendLine("");
            return sb.ToString();
        }

    }
}
