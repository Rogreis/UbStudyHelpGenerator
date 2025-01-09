using DocumentFormat.OpenXml.Drawing.Charts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json.Serialization;

namespace UbStudyHelpGenerator.UbStandardObjects.Objects
{

    /// <summary>
    /// Define a table of contents entry for TUB
    /// </summary>
    public class TUB_TOC_Entry
    {
        public short PaperNo { get; set; } = -1;

        public short SectionNo { get; set; } = -1;

        public short ParagraphNo { get; set; } = -1;

        private string _text { get; set; } = "";

        [JsonPropertyName("text")]
        public string Text
        {
            get { return Paragraph.ToHtml(_text); }
            set { _text = value; }
        }


        [JsonPropertyName("expanded")]
        public bool Expanded { get; set; }

        [JsonPropertyName("nodes")]
        public List<TUB_TOC_Entry> Nodes { get; set; } = new List<TUB_TOC_Entry>();

        public override string ToString()
        {
            return Text;
        }
    }

    /// <summary>
    /// Output table of contents using bootstrap 5
    /// <see href="https://www.w3schools.com/howto/howto_js_treeview.asp"/>
    /// </summary>
    public class TUB_TOC_Html
    {
        private string classesForUlElement = "nested";
        private string ExpandableLi = "caret expandable";
        private string NonExpandableLi = "caret";

        private List<TUB_TOC_Entry> TocEntries = null;

        private Parameters Param = null;

        public TUB_TOC_Html(Parameters param, List<TUB_TOC_Entry> toc_entries)
        {
            TocEntries= toc_entries;
            Param = param;
        }

        private string Href(short paperNo)
        {
            return $@"javascript:loadDoc('content/Doc{paperNo:000}.html', null)";
        }

        private string Href(TUB_TOC_Entry entry)
        {
            return $@"javascript:loadDoc('content/Doc{entry.PaperNo:000}.html','p{entry.PaperNo:000}_{entry.SectionNo:000}_000')";
        }

        private string LiId(TUB_TOC_Entry entry)
        {
            return $@"toc_{entry.PaperNo:000}_{entry.SectionNo:000}";
        }

        private string CreateLiElement(TUB_TOC_Entry entry, string ident)
        {
            if (entry.PaperNo < 0)
            {
                string id = "";
                switch(entry.Text)
                {
                    case "Parte I":
                        id = "part1";
                        break;
                    case "Parte II":
                        id = "part2";
                        break;
                    case "Parte III":
                        id = "part3";
                        break;
                    case "Parte IV":
                        id = "part4";
                        break;
                }
                return $"{ident}<li id=\"{id}\"><span class=\"caret expandable\">{entry.Text}</span>";
            }
            if (entry.SectionNo == 0)
            {
                return $"{ident}<li id=\"{LiId(entry)}\"><span class=\"caret\"><a class=\"liIndex\" href=\"{Href(entry)}\">{entry.Text}</a></span>";
            }
            return $"{ident}<li id=\"{LiId(entry)}\"><a class=\"liIndex\" href=\"{Href(entry)}\">{entry.Text}</a>"; //  
        }

        private void HtmlNodes(StringBuilder sb, List<TUB_TOC_Entry> tocEntries, string ident)
        {
            sb.AppendLine($"{ident}<ul class=\"{classesForUlElement}\"> ");
            foreach (TUB_TOC_Entry entry in tocEntries)
            {
                string classes = entry.SectionNo == 0 ? NonExpandableLi : ExpandableLi; 
                bool hasNodes = entry.Nodes != null && entry.Nodes.Count > 0;
                if (hasNodes)
                {
                    sb.AppendLine(CreateLiElement(entry, ident));
                    HtmlNodes(sb, entry.Nodes, ident + "   ");
                }
                else
                {
                    //sb.AppendLine($"{ident}   <li><a class=\"liIndex\" href=\"{Href(entry)}\">{entry.Text}</a> ");
                    sb.AppendLine(CreateLiElement(entry, ident));
                }
                sb.AppendLine($"{ident}   </li> ");
            }
            sb.AppendLine($"{ident}</ul> ");
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sb"></param>
        public void JavaScript(StringBuilder sb)
        {
            sb.AppendLine("<script> ");
            sb.AppendLine("  var toggler = document.getElementsByClassName(\"caret\"); ");
            sb.AppendLine("  var expandables = document.getElementsByClassName(\"expandable\"); ");
            sb.AppendLine("  var i; ");
            sb.AppendLine(" ");
            sb.AppendLine("  for (i = 0; i < expandables.length; i++) { ");
            sb.AppendLine("      expandables[i].parentElement.querySelector(\".nested\").classList.toggle(\"active\"); ");
            sb.AppendLine("      expandables[i].classList.toggle(\"caret-down\"); ");
            sb.AppendLine("} ");
            sb.AppendLine("  for (i = 0; i < toggler.length; i++) { ");
            sb.AppendLine("    toggler[i].addEventListener(\"click\", function() { ");
            sb.AppendLine("      this.parentElement.querySelector(\".nested\").classList.toggle(\"active\"); ");
            sb.AppendLine("      this.classList.toggle(\"caret-down\"); ");
            sb.AppendLine("    }); ");
            sb.AppendLine("  } ");
            sb.AppendLine("</script> ");
        }

        public void Html(string pathTocTable)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<ul id=\"myUL\"> ");
            string ident = "";
            foreach (TUB_TOC_Entry entry in TocEntries)
            {
                sb.AppendLine(CreateLiElement(entry, ident));

                if (entry.Nodes != null && entry.Nodes.Count > 0)
                {
                    HtmlNodes(sb, entry.Nodes, ident + "   ");
                }
                sb.AppendLine($"{ident}   </li> ");
            }
           
            sb.AppendLine("</ul> ");
            sb.AppendLine(" ");
            string html = sb.ToString();
            File.WriteAllText(pathTocTable, html);
        }
    }


}
