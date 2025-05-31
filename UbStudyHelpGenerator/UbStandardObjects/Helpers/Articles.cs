using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using UbStudyHelpGenerator.Classes;
using System.Text.Json;



namespace UbStudyHelpGenerator.UbStandardObjects.Helpers
{

    /// <summary>
    /// Helper class for articles
    /// </summary>
    public class Articles
    {
        private static string[] _markdownLines; // Store the lines for easier access
        private static int _lineIndex; // Keep track of the current line
        private string CssClass_UL_Element = "nested";
        // private string ExpandableLi = "caret expandable"; kept here for documentation
        private string NonExpandableLi = "caret";
        private string ExpandableLi = "caret active";
        private string CssClassHtml_A = "liIndex";

        private enum LineType
        {
            Heading,
            Blockquote,
            BoldItalic,
            Normal,
            OrderedList,
            UnorderedList,
            Table,
            Blank,
            Line,
            Image,
            Comment,
            Link,
            Mermaid,
            ArticleLinkOpen
        }

        /// <summary>
        /// Convert a string to a slug (URL-friendly format)
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private static string Slugify(string text)
        {
            text = text.ToLowerInvariant();
            text = Regex.Replace(text, @"[^\w\s-]", "");  // remove punctuation
            text = Regex.Replace(text, @"[\s_]+", "-");   // replace spaces/underscores with dashes
            return text.Trim('-');
        }

        private string ReadNextLine()
        {
            if (_lineIndex < _markdownLines.Length)
            {
                return _markdownLines[_lineIndex++];
            }
            return null;
        }

        private void PushLine(string line)
        {
            _lineIndex--; // Decrement to "push" the line back
        }

        private LineType GetLineType(string line)
        {
            // Blank line indicates paragraph break
            if (string.IsNullOrWhiteSpace(line)) return LineType.Blank;
            if (line.StartsWith(";")) return LineType.Comment;
            if (line.StartsWith("#")) return LineType.Heading;
            if (line.StartsWith(">")) return LineType.Blockquote;
            if (line.StartsWith("*")) return LineType.UnorderedList;
            if (line.StartsWith("|")) return LineType.Table;
            if (line.Contains("<articles>")) return LineType.ArticleLinkOpen;
            if (line.Contains("```mermaid") || line.StartsWith("<pre class=\"mermaid\">")) return LineType.Mermaid;

            if (line.StartsWith("---")) return LineType.Line;
            if (line.StartsWith("<li>") || line.StartsWith("<ul>") || line.StartsWith("</ul>")) return LineType.Link;
            if (line.Contains("```img")) return LineType.Image;
            if (Regex.IsMatch(line, @"^\d+\.(?!\s*\\)")) return LineType.OrderedList;
            if (line.StartsWith("<")) return LineType.Blockquote;

            return LineType.Normal;
        }

        private string Paragraph(string line)
        {
            return $"<p>{line}</p>";
        }


        #region Image

        private class ImageInfo
        {
            public string src { get; set; }
            public string title { get; set; }
            public string ident { get; set; }
        }

        private class ImageRoot
        {
            public ImageInfo Imagem { get; set; }
        }


        /// <summary>
        /// Process an image block in the markdown file.
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="lineREad"></param>
        private void ProcessImage(StringBuilder sb)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            string nextLine = ReadNextLine();
            while (!nextLine.StartsWith("```"))
            {
                if (!string.IsNullOrWhiteSpace(nextLine))
                    jsonBuilder.AppendLine(nextLine);
                nextLine = ReadNextLine();
            }
            // Work only with next 2 lines more
            nextLine = ReadNextLine();
            if (string.IsNullOrWhiteSpace(nextLine))
            {
                jsonBuilder.AppendLine(nextLine);
                nextLine = ReadNextLine();
                if (!nextLine.TrimStart().StartsWith("!["))
                {
                    PushLine(nextLine); // Put the line back for later processing
                }
            }


            ImageRoot imageRoot = null;
            if (jsonBuilder.Length > 0)
            {
                try
                {
                    // Deserialize the JSON string into the RootObject
                    imageRoot = JsonSerializer.Deserialize<ImageRoot>(jsonBuilder.ToString());
                    sb.AppendLine($"<img src=\"{imageRoot.Imagem.src}\" class=\"img-thumbnail\" alt=\"{imageRoot.Imagem.title}\">");
                    sb.AppendLine($"<p><sub>{imageRoot.Imagem.ident}</sub></p> ");
                }
                catch
                {
                    sb.AppendLine("ERRO: bloco de imagem inválido");
                    return;
                }
            }
        }


        #endregion


        /// <summary>
        /// Works only with the first blockquote level (articles don't have nested blockquotes)
        /// All lines must start with > to be considered part of the blockquote
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private void ProcessBlockquote(StringBuilder blockquoteContent, string line)
        {
            blockquoteContent.AppendLine("<blockquote>");
            line = ItalicBoldLinksImages(line);

            blockquoteContent.AppendLine("   " + Paragraph(line.Substring(1).Trim()));

            string nextLine = ReadNextLine();
            // Blockquotes (including nested and multi-paragraph)
            while (nextLine != null && nextLine.StartsWith(">"))
            {
                line = ItalicBoldLinksImages(nextLine);
                blockquoteContent.AppendLine("   " + Paragraph(line.Substring(1).Trim()));
                nextLine = ReadNextLine();
            }
            blockquoteContent.AppendLine("</blockquote>");
            blockquoteContent.AppendLine("");
            if (nextLine != null) PushLine(nextLine); // Put the line back for later processing
        }

        private void ProcessArticle(StringBuilder sb)
        {
            string title = "", sumario = "", link = "";
            string nextLine = ReadNextLine();
            sb.AppendLine("<table class=\"table table-dark table-hover\"> ");
            sb.AppendLine("    <thead> ");
            sb.AppendLine("      <tr> ");
            sb.AppendLine("        <th>Link</th> ");
            sb.AppendLine("        <th>Description</th> ");
            sb.AppendLine("      </tr> ");
            sb.AppendLine("    </thead> ");


            while (!nextLine.Contains("</articles>"))
            {
                if (nextLine.Contains("<article>"))
                {
                }
                else if (nextLine.Contains("Título:"))
                {
                    title = nextLine.Replace("Título:", "").Trim();
                }
                else if (nextLine.Contains("Sumário:"))
                {
                    sumario = nextLine.Replace("Sumário:", "").Trim();
                }
                else if (nextLine.Contains("Link:"))
                {
                    link = nextLine.Replace("Link:", "").Trim();
                }
                else if (nextLine.Contains("</article>"))
                {
                    sb.AppendLine("      <tr>");
                    sb.AppendLine($"        <td><a href=\"javascript:loadArticle('{link}')\">{title}</a></td>");
                    sb.AppendLine($"        <td>{sumario}</td>");
                    sb.AppendLine("      </tr>");
                }
                nextLine = ReadNextLine();
            }
            sb.AppendLine("</table>");
            sb.AppendLine("");
        }


        private string ItalicBoldLinksImages(string line)
        {
            // Bold and Italic (handle combined cases - both * and _)
            line = Regex.Replace(line, @"(\*\*\*|___)(.+?)\1", "<strong><em>$2</em></strong>"); // Bold and Italic
            line = Regex.Replace(line, @"(\*\*\*|___)(.+?)\1", "<em><strong>$2</em></strong>"); // Italic and Bold

            // Bold (both * and _)
            line = Regex.Replace(line, @"(\*\*|__)(.+?)\1", "<strong>$2</strong>");

            // Italic (both * and _)
            line = Regex.Replace(line, @"(\*|_)(.+?)\1", "<em>$2</em>");

            // Links: [link text](url) or [link text](url "title")
            line = Regex.Replace(line, @"\[([^\]]+)\]\(([^)]+)(?:\s""([^""]+) "")?\)", "<a href=\"$2\" title=\"$3\">$1</a>");

            // Images: ![alt text](url) or ![alt text](url "title")
            line = Regex.Replace(line, @"!\[([^\]]+)\]\(([^)]+)(?:\s""([^""]+) "")?\)", "<img src=\"$2\" alt=\"$1\" title=\"$3\" />");

            return line;
        }

        private string Headings(string line)
        {
            int level = 0;
            if (line.StartsWith("######")) level = 6;
            else if (line.StartsWith("#####")) level = 5;
            else if (line.StartsWith("####")) level = 4;
            else if (line.StartsWith("###")) level = 3;
            else if (line.StartsWith("##")) level = 2;
            else level = 1;
            string title = line.Substring(level).Trim();
            string jumpLines = level == 2 ? "<br /><br /><br />" : (level == 3 ? "<br />" : "");
            return $"{jumpLines}<h{level} id=\"{Slugify(title)}\">{title}</h{level}>"; ;
        }

        private string OrderedListLine(string line)
        {
            line = ItalicBoldLinksImages(line);
            return $"<li>{line.Substring(line.IndexOf('.') + 1).Trim()}</li>";
        }

        private string UnorderedListLine(string line)
        {
            line = ItalicBoldLinksImages(line);
            return $"<li>{line.Substring(1).Trim()}</li>";
        }

        private void OrderedList(StringBuilder sb, string line)
        {
            sb.AppendLine("<ol>");
            sb.AppendLine($"   {OrderedListLine(line)}");
            string nextLine = ReadNextLine();
            while (nextLine != null && GetLineType(nextLine) == LineType.OrderedList)
            {
                nextLine = ItalicBoldLinksImages(nextLine);
                sb.AppendLine($"   {OrderedListLine(nextLine)}");
                nextLine = ReadNextLine();
            }
            sb.AppendLine("</ol>");
            sb.AppendLine("");
            if (nextLine != null) PushLine(nextLine); // Put the line back for later processing
        }

        private void UnorderedList(StringBuilder sb, string line)
        {
            sb.AppendLine("<ul>");
            sb.AppendLine($"   {UnorderedListLine(line)}");
            string nextLine = ReadNextLine();
            while (nextLine != null && GetLineType(nextLine) == LineType.OrderedList)
            {
                nextLine = ItalicBoldLinksImages(nextLine);
                sb.AppendLine($"   {UnorderedListLine(nextLine)}");
                nextLine = ReadNextLine();
            }
            sb.AppendLine("</ul>");
            sb.AppendLine("");
            if (nextLine != null) PushLine(nextLine); // Put the line back for later processing
        }

        private List<string> GetTableAlignements(string line)
        {
            string[] colValues = line.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            List<string> alignments = new List<string>();
            foreach (string colValue in colValues)
            {
                string cellValue = colValue.Trim();
                if (!cellValue.Contains("-") || (cellValue.StartsWith(":") && cellValue.EndsWith(":"))) alignments.Add(" style=\"text-align:center; vertical-align: top;\"");
                else if (colValue.Trim().StartsWith(":")) alignments.Add(" style=\"text-align:left; vertical-align: top;\"");
                else if (colValue.Trim().EndsWith(":")) alignments.Add(" style=\"text-align:right; vertical-align: top;\"");
                else alignments.Add(" style=\"text-align:center; vertical-align: top;\"");
            }
            return alignments;
        }

        private void ProcessTableLine(StringBuilder htmlTable, List<string> alignments, string line, bool isHEader)
        {
            string[] colValues = line.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

            List<string> celValues = new List<string>(colValues);
            while (celValues.Count < alignments.Count) celValues.Add(""); // Add empty cells to match alignments

            string tagColumn = isHEader ? "th" : "td";
            htmlTable.AppendLine("   <tr>");
            int _currentIndex = 0;
            foreach (string value in celValues)
            {
                string stylename = _currentIndex < alignments.Count ? alignments[_currentIndex++] : " style=\"text-align:right; vertical-align: top;\"";
                string cellValue = string.IsNullOrEmpty(value.Trim()) ? "&nbsp;" : ItalicBoldLinksImages(value.Trim());
                htmlTable.AppendLine($"      <{tagColumn} {stylename}>{cellValue}</{tagColumn}>");
            }
            htmlTable.AppendLine("   </tr>");
        }

        private void ProcessTable(StringBuilder htmlTable, string line)
        {
            htmlTable.AppendLine("<table>");
            List<string> headers = new List<string>();

            // 1. Read header line
            string headerLine = line;
            List<string> alignments = null;
            line = ReadNextLine();
            // The secong line indicates a table header or not
            if (line != null && line.Contains("---"))
            {
                htmlTable.AppendLine("<thead>");
                alignments = GetTableAlignements(line);
                ProcessTableLine(htmlTable, alignments, headerLine, true);
                htmlTable.AppendLine("</thead>");
                htmlTable.AppendLine("<tbody>");
            }
            else
            {
                ProcessTableLine(htmlTable, alignments, headerLine, false);
                ProcessTableLine(htmlTable, alignments, line, false);
            }

            line = ReadNextLine();
            while (line != null && line.StartsWith("|"))
            {
                ProcessTableLine(htmlTable, alignments, line, false);
                line = ReadNextLine();
            }
            htmlTable.AppendLine("</tbody>");
            htmlTable.AppendLine("</table>");
            if (line != null) PushLine(line); // Put the line back for later processing
        }


        private void ProcessMermaid(StringBuilder sb, string line)
        {
            sb.AppendLine("<figure> ");
            sb.AppendLine("   <div class=\"col-md-8\">  ");
            sb.AppendLine("      <div class=\"mermaid\">  ");

            string caption = "";

            line = ReadNextLine();
            while (line != null && !(line.StartsWith("```") || line.StartsWith("</pre>")))
            {
                if (line.StartsWith("%%"))
                {
                    caption = line.Substring(2).Trim();
                }
                else
                    sb.AppendLine($"      {line}");
                line = ReadNextLine();
            }
            sb.AppendLine("      </div>  ");
            sb.AppendLine("   </div>  ");
            sb.AppendLine("<figcaption style=\"font-size: 0.8em;  margin-top: 5px; margin-left: 50px;\"> ");
            sb.AppendLine($"{caption}");
            sb.AppendLine("</figcaption> ");
            sb.AppendLine("</figure> ");
        }


        private void ConvertFile(string[] markdownLines, string htmlFilePath)
        {
            string pattern = @"(<img src="")[^""]*\\([^""]*"")";
            string replacement = @"$1articles\\images\\$2";

            try
            {
                _markdownLines = markdownLines;
                _lineIndex = 0;
                StringBuilder sb = new StringBuilder();
                List<string> htmlLines = new List<string>();

                string line = ReadNextLine();
                while (line != null)
                {
                    switch (GetLineType(line))
                    {
                        case LineType.Heading:
                            sb.AppendLine(ItalicBoldLinksImages(Headings(line)));
                            break;
                        case LineType.Blockquote:
                            ProcessBlockquote(sb, line);
                            break;
                        case LineType.OrderedList:
                            OrderedList(sb, line);
                            break;
                        case LineType.UnorderedList:
                            UnorderedList(sb, line);
                            break;
                        case LineType.Table:
                            ProcessTable(sb, line);
                            break;
                        case LineType.Mermaid:
                            ProcessMermaid(sb, line);
                            break;
                        case LineType.Normal:
                            sb.AppendLine(ItalicBoldLinksImages(Paragraph(line)));
                            break;
                        case LineType.Blank:
                            sb.AppendLine("");
                            break;
                        case LineType.Line:
                            sb.AppendLine("<hr />");
                            break;
                        case LineType.Image:
                            //line = Regex.Replace(line, pattern, replacement);
                            //sb.AppendLine(line);
                            ProcessImage(sb);
                            break;
                        case LineType.Link:
                            sb.AppendLine(line);
                            break;
                        case LineType.ArticleLinkOpen:
                            ProcessArticle(sb);
                            break;
                        case LineType.Comment:
                            // Comment are just ignored lines
                            break;
                        default:
                            sb.AppendLine(Paragraph($"Linha não tratada: {line}"));
                            break;
                    }
                    line = ReadNextLine();
                }
                File.WriteAllText(htmlFilePath, sb.ToString(), Encoding.UTF8);
                EventsControl.FireShowMessage($"Generated '{htmlFilePath}' successfully.");
            }
            catch (Exception ex)
            {
                EventsControl.FireShowMessage($"An error occurred: {ex.Message}");
            }
        }

        #region Articles Index
        //private List<ArticleIndexEntry> ExtractArticleTitleFromTextFile(string input)
        //{
        //    if (string.IsNullOrEmpty(input))
        //    {
        //        return new List<ArticleIndexEntry>();
        //    }

        //    List<ArticleIndexEntry> artigos = new List<ArticleIndexEntry>();

        //    // Usando Regex para encontrar blocos de texto formatados como artigos.
        //    // Isso é mais robusto para lidar com variações na formatação, como espaços extras.
        //    string pattern = @"Título:\s*(.+)\s*Sumário:\s*(.+)\s*Link:\s*(.+)";
        //    MatchCollection matches = Regex.Matches(input, pattern, RegexOptions.Multiline);

        //    foreach (Match match in matches)
        //    {
        //        ArticleIndexEntry artigo = new ArticleIndexEntry
        //        {
        //            Titulo = match.Groups[1].Value.Trim(),
        //            Sumario = match.Groups[2].Value.Trim(),
        //            Link = match.Groups[3].Value.Trim()
        //        };
        //        artigos.Add(artigo);
        //    }

        //    return artigos;
        //}


        #endregion


        public void Process(string sourceMarkdownFilesPath, string destinationHtmlFilesPath)
        {
            destinationHtmlFilesPath = Path.Combine(destinationHtmlFilesPath, "articles");

            // (1) Copy all files from source to destination, first deleting all files in destination
            FolderCreator.CopyAll(sourceMarkdownFilesPath, destinationHtmlFilesPath);
            StaticObjects.FireShowMessage("Files copied.");


            // (3) Convert all markdown file to be html
            MarkdownHelper markdownHelper = new MarkdownHelper();
            //string pathHtmlFiles = Path.Combine(destinationHtmlFilesPath, @"articles");
            foreach (string markdownFilePath in Directory.GetFiles(destinationHtmlFilesPath, "*.md", SearchOption.AllDirectories))
            {
                StaticObjects.FireShowMessage($"Processing file: {markdownFilePath}");

                string fileNameWithoutExtension = "", foldersPathRelativeToRoot = "";
                FolderCreator.ExtractFileInfo(markdownFilePath,
                                              destinationHtmlFilesPath,
                                              out fileNameWithoutExtension,
                                              out foldersPathRelativeToRoot);

                string pathHtmlFileDestination = Path.Combine(destinationHtmlFilesPath, Path.Combine(foldersPathRelativeToRoot, fileNameWithoutExtension + ".html"));

                string[] lines = File.ReadAllLines(markdownFilePath);
                ConvertFile(lines, pathHtmlFileDestination);
                Articles_Indexes articles_Indexes = new Articles_Indexes();
                articles_Indexes.Run(markdownFilePath, pathHtmlFileDestination);
            }

            // (4) Deleta all markdown files in the destination folder
            foreach (string markdownFilePath in Directory.GetFiles(destinationHtmlFilesPath, "*.md", SearchOption.AllDirectories))
            {
                StaticObjects.FireShowMessage($"Deleting file: {markdownFilePath}");
                File.Delete(markdownFilePath);
            }


        }
    }
}
