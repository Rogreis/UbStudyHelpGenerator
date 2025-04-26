using LiteDB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UbStudyHelpGenerator.UbStandardObjects.ImportExport.Models;
using UbStudyHelpGenerator.UbStandardObjects.Objects;

namespace UbStudyHelpGenerator.UbStandardObjects.ImportExport
{
    public class Export_HtmlTocTable : Export_0base
    {
        private string CssClass_UL_Element = "nested";
        // private string ExpandableLi = "caret expandable"; kept here for documentation
        private string NonExpandableLi = "caret";
        private string CssClassHtml_A = "liIndex";

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

        /// <summary>
        /// Class used to administrate the identation of the treeview html code
        /// </summary>
        private class SpaceString
        {
            private string _value;
            private const string IdentDistance = "   ";

            public SpaceString()
            {
                _value = String.Empty;
            }

            public string Value => _value;

            public static SpaceString operator ++(SpaceString s)
            {
                s._value += IdentDistance;
                return s;
            }

            public static SpaceString operator --(SpaceString s)
            {
                if (s._value.Length >= IdentDistance.Length)
                {
                    s._value = s._value.Substring(0, s._value.Length - IdentDistance.Length);
                }
                return s;
            }

            public override string ToString() => _value;
        }


        //private string LiId(TUB_TOC_Entry entry, string idSuffix = "")
        //{
        //    if (entry.PaperNo < 0)
        //    {
        //        string id = "";
        //        switch (entry.Text)
        //        {
        //            case "Parte I":
        //                id = "part1";
        //                break;
        //            case "Parte II":
        //                id = "part2";
        //                break;
        //            case "Parte III":
        //                id = "part3";
        //                break;
        //            case "Parte IV":
        //                id = "part4";
        //                break;
        //        }
        //        return $"{id}{idSuffix}";
        //    }
        //    return $@"toc_{entry.PaperNo:000}_{entry.SectionNo:000}{idSuffix}";
        //}

        private string TreeviewHtmlID(ParagraphExport par)
        {
            return $"toc_{Math.Abs(par.Paper):000}_{Math.Abs(par.Section):000}_000_div";
        }

        private string TreeviewHtmlLink(ParagraphExport par)
        {
            return $"javascript:loadDoc('content/Doc{par.Paper:000}.html','p{par.Paper:000}_{par.Section:000}_{par.Paragraph:000}')";
        }

        private string RomanoPart(int part)
        {
            string romanNumber = "I";
            switch (part)
            {
                case 2: romanNumber = "II"; break;
                case 3: romanNumber = "III"; break;
                case 4: romanNumber = "IV"; break;
            };
            return romanNumber;
        }

        private void OpenTreeviewNodes(StringBuilder sb, ParagraphExport par, ref SpaceString ident)
        {
            if (par.IsStartingPart)
            {
                sb.AppendLine($"{ident++}<li id=\"part{par.Part}_div\"><span class=\"{NonExpandableLi}\">Parte {RomanoPart(par.Part)}</span> ");
                sb.AppendLine($"{ident++}<ul class=\"{CssClass_UL_Element}\"> ");
            }

            sb.AppendLine($"{ident++}<li id=\"{TreeviewHtmlID(par)}\"><span class=\"{NonExpandableLi}\"><a class=\"{CssClassHtml_A}\" href=\"{TreeviewHtmlLink(par)}\">{par.Title}</a></span>");
            sb.AppendLine($"{ident++}<ul class=\"nested\"> ");
        }

        private void CloseTreeviewNodes(StringBuilder sb, ParagraphExport par, ref SpaceString ident)
        {
            sb.AppendLine($"{--ident}</ul>");
            sb.AppendLine($"{--ident}</li>");

            if (par.IsStartingPart)
            {
                if (par.Paper > 1) sb.AppendLine($"{--ident}</ul>");
                sb.AppendLine($"{--ident}</li>");
            }
        }

        private void TreeviewNode(StringBuilder sb, ParagraphExport par, ref SpaceString ident)
        {
            if (par.IsPaperTitle)
            {
                switch (par.Paper)
                {
                    case 0:
                        OpenTreeviewNodes(sb, par, ref ident);
                        break;
                    default:
                        CloseTreeviewNodes(sb, par, ref ident);
                        OpenTreeviewNodes(sb, par, ref ident);
                        break;
                }
            }

            if (par.IsSectionTitle)
            {
                sb.AppendLine($"{ident}<li id=\"{TreeviewHtmlID(par)}\"><a class=\"{CssClassHtml_A}\" href=\"{TreeviewHtmlLink(par)}\">{par.Text}</a></span></li>");
            }

            StaticObjects.FireShowPaperNumber(par.Paper);
        }

        public override void Run(string tableName, string pathDatabase)
        {
            CountErrors = 0;
            SpaceString ident = new SpaceString();
            StringBuilder sb = new StringBuilder();

            // Add the style for the treeview
            TreeviewStyle(sb);

            sb.AppendLine("<div class=\"treeview\">");
            sb.AppendLine($"{ident}<ul>");

            using (var db = new LiteDatabase(pathDatabase))
            {
                ILiteCollection<ParagraphExport> liteCollection = db.GetCollection<ParagraphExport>(tableName);
                List<ParagraphExport> tocData = liteCollection.Query()
                            .Where(x => x.Paragraph == 0)
                            .OrderBy(x => new { x.Paper, x.Section })
                            .ToList();
                ident++;
                foreach (ParagraphExport par in tocData)
                {
                    TreeviewNode(sb, par, ref ident);
                }
                CloseTreeviewNodes(sb, tocData.Last(), ref ident);
                sb.AppendLine($"{ident}</ul>");
                sb.AppendLine("</div>");
                sb.AppendLine(" ");
                string html = sb.ToString();
                Encoding utf8WithoutBOM = new UTF8Encoding(false);
                File.WriteAllText(@"C:\Trabalho\Github\Rogerio\b\content\TocTable2.html", html, utf8WithoutBOM);
                StaticObjects.FireShowMessage("Finished.");
            }

        }

        protected override void FillPaper(LiteDatabase db, string destyinationFolder, short paperNo)
        {
            throw new NotImplementedException();
        }
    }
}
