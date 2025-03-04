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

        private string LiId(TUB_TOC_Entry entry, string idSuffix= "")
        {
            if (entry.PaperNo < 0)
            {
                string id = "";
                switch (entry.Text)
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
                return $"{id}{idSuffix}";
            }
            return $@"toc_{entry.PaperNo:000}_{entry.SectionNo:000}{idSuffix}";
        }

        private string CreateLiElement(TUB_TOC_Entry entry, string ident)
        {
            if (entry.PaperNo < 0)
            {
                return $"{ident}<li id=\"{LiId(entry)}\"><span class=\"caret expandable\">{entry.Text}</span>";
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
                sb.AppendLine($"{ident}<div id=\"{LiId(entry, "_div")}\">");
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
                sb.AppendLine($"{ident}</li>");
                sb.AppendLine($"{ident}</div>");
            }
            sb.AppendLine($"{ident}</ul> ");
        }


        private void TreeviewStyle(StringBuilder sb)
        {
            sb.AppendLine("    <style> ");
            sb.AppendLine("        .treeview ul { ");
            sb.AppendLine("            list-style: none; ");
            sb.AppendLine("            padding-left: 20px; /* Indentation for nested levels */ ");
            sb.AppendLine("        } ");
            sb.AppendLine(" ");
            sb.AppendLine("        .treeview li { ");
            sb.AppendLine("            cursor: pointer; /* Make list items clickable */ ");
            sb.AppendLine("        } ");
            sb.AppendLine("        .treeview .caret::before{ ");
            sb.AppendLine("            content: \"\f279\"; /* Unicode for caret-right */ ");
            sb.AppendLine("            display: inline-block; ");
            sb.AppendLine("            margin-right: 5px; ");
            sb.AppendLine("            font-family: bootstrap-icons !important; ");
            sb.AppendLine("        } ");
            sb.AppendLine("        .treeview .caret.active::before{ ");
            sb.AppendLine("            content: \"\f27a\"; /* Unicode for caret-down */ ");
            sb.AppendLine("        } ");
            sb.AppendLine(" ");
            sb.AppendLine("        .nested { ");
            sb.AppendLine("            display: none; ");
            sb.AppendLine("        } ");
            sb.AppendLine(" ");
            sb.AppendLine("        .active { ");
            sb.AppendLine("            display: block; ");
            sb.AppendLine("        } ");

            sb.AppendLine(" ");
            sb.AppendLine(".treeview liIndex { ");
            sb.AppendLine("    font-family: var(--font); ");
            sb.AppendLine("    font-size: var(--fontSize); ");
            sb.AppendLine("} ");
            sb.AppendLine(" ");
            sb.AppendLine(".treeview a.liIndex:link { ");
            sb.AppendLine("    text-decoration: none; ");
            sb.AppendLine("    color: var(--textColorDark); ");
            sb.AppendLine("} ");
            sb.AppendLine(" ");
            sb.AppendLine(".treeview a.liIndex:visited { ");
            sb.AppendLine("    text-decoration: none; ");
            sb.AppendLine("    color: var(--textColorDark); ");
            sb.AppendLine("} ");
            sb.AppendLine(" ");
            sb.AppendLine(".treeview a.liIndex:hover { ");
            sb.AppendLine("    text-decoration: underline; ");
            sb.AppendLine("    color: var(--textColorDark); ");
            sb.AppendLine("} ");
            sb.AppendLine(" ");
            sb.AppendLine(".treeview a.liIndex:active { ");
            sb.AppendLine("    text-decoration: none; ");
            sb.AppendLine("    color: var(--textColorDark); ");
            sb.AppendLine("} ");
            sb.AppendLine(" ");


            sb.AppendLine("    </style> ");
            sb.AppendLine(" ");
        }

        private string TreeviewLink(TUB_TOC_Entry entry)
        {
            if (entry.PaperNo < 0)
                return entry.Text;
            return $"<a class=\"liIndex\" href=\"{Href(entry)}\">{entry.Text}</a>";
        }

        private void TreeviewNodes(StringBuilder sb, TUB_TOC_Entry entry, string ident)
        {
            if (entry.Nodes != null && entry.Nodes.Count > 0)
            {
                // Has nodes
                sb.AppendLine($"{ident}<li id=\"{LiId(entry, "_div")}\"><span class=\"caret\">{TreeviewLink(entry)}</span>");
                sb.AppendLine($"{ident}   <ul class=\"nested\">");
                foreach (TUB_TOC_Entry childEntry in entry.Nodes)
                {
                    TreeviewNodes(sb, childEntry, ident + "   ");
                }
                sb.AppendLine($"{ident}   </ul>");
                sb.AppendLine($"{ident}</li>");
            }
            else
            {
                sb.AppendLine($"{ident}   <li id=\"{LiId(entry, "_div")}\">{TreeviewLink(entry)}</span>");
            }

        }

        public void Html(string pathTocTable)
        {
            StringBuilder sb = new StringBuilder();
            TreeviewStyle(sb);

            sb.AppendLine("<div class=\"treeview\">");
            string ident = "   ";
            sb.AppendLine($"{ident}<ul>");
            foreach (TUB_TOC_Entry entry in TocEntries)
                TreeviewNodes(sb, entry, ident + "   ");

            sb.AppendLine($"{ident}</ul>");
            sb.AppendLine("</div>");
            sb.AppendLine(" ");
            string html = sb.ToString();
            File.WriteAllText(pathTocTable, html, Encoding.UTF8);
        }
    }


}
