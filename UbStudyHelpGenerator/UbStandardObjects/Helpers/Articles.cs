using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using UbStudyHelpGenerator.Classes;


namespace UbStudyHelpGenerator.UbStandardObjects.Helpers
{

    /// <summary>
    /// Helper class for articles
    /// </summary>
    public class Articles
    {
        private static string[] _markdownLines; // Store the lines for easier access
        private static int _lineIndex; // Keep track of the current line

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
            Image
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
            if (line.StartsWith("#")) return LineType.Heading;
            if (line.StartsWith(">")) return LineType.Blockquote;
            if (line.StartsWith("*")) return LineType.UnorderedList;
            if (line.StartsWith("|")) return LineType.Table;
            if (line.StartsWith("---")) return LineType.Line;
            if (line.Contains("<img")) return LineType.Image;
            if (Regex.IsMatch(line, @"^\d+\.(?!\s*\\)")) return LineType.OrderedList;
            if (line.StartsWith(">")) return LineType.Blockquote;

            return LineType.Normal;
        }


        private string Paragraph(string line)
        {
            return $"<p>{line}</p>";
        }

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
            return $"<h{level}>{line.Substring(level).Trim()}</h{level}>";
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
            while (nextLine != null && GetLineType(nextLine) ==  LineType.OrderedList)
            {
                nextLine= ItalicBoldLinksImages(nextLine);
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

            List<string> celValues= new List<string>(colValues);
            while (celValues.Count < alignments.Count) celValues.Add(""); // Add empty cells to match alignments

            string tagColumn = isHEader ? "th" : "td";
            htmlTable.AppendLine("   <tr>");
            int _currentIndex = 0;
            foreach (string value in celValues)
            {
                string stylename = _currentIndex < alignments.Count ? alignments[_currentIndex++] : " style=\"text-align:right; vertical-align: top;\"";
                string cellValue= string.IsNullOrEmpty(value.Trim()) ? "&nbsp;" : ItalicBoldLinksImages(value.Trim());
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



        private void ConvertFile(string markdownFilePath, string htmlFilePath)
        {
            string pattern = @"(<img src="")[^""]*\\([^""]*"")";
            string replacement = @"$1articles\\images\\$2";

            try
            {
                _markdownLines = File.ReadAllLines(markdownFilePath); // Store for global access
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
                            line = Regex.Replace(line, pattern, replacement);
                            sb.AppendLine(line);
                            break;
                        default:
                            sb.AppendLine(Paragraph($"Linha não tratada: {line}"));
                            break;
                    }
                    line = ReadNextLine();
                }
                File.WriteAllText(htmlFilePath, sb.ToString(), Encoding.UTF8);
                EventsControl.FireShowMessage($"Markdown file '{markdownFilePath}' converted to HTML '{htmlFilePath}' successfully.");
            }
            catch (FileNotFoundException)
            {
                EventsControl.FireShowMessage($"File '{markdownFilePath}' not found.");
            }
            catch (Exception ex)
            {
                EventsControl.FireShowMessage($"An error occurred: {ex.Message}");
            }

        }

        public void Process(string basePath)
        {
            //string markdownFilePath = @"C:\Trabalho\Github\Rogerio\b\articles\teste.md";
            //string htmlFilePath = @"C:\Trabalho\Github\Rogerio\b\articles\teste.html";
            //ConvertFile(markdownFilePath, htmlFilePath);

            string pathHtmlFiles = Path.Combine(basePath, @"articles");
            foreach (string markdownFilePath in Directory.GetFiles(pathHtmlFiles, "*.md"))
            {
                string htmlFilePath = markdownFilePath.Replace(".md", ".html");
                ConvertFile(markdownFilePath, htmlFilePath);
            }
        }

    }
}
