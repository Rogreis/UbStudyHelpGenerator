using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace UbStudyHelpGenerator.UbStandardObjects.Helpers
{
    public class MarkdownHelper
    {
        private void FireShowMessage(string message)
        {
            StaticObjects.FireShowMessage(message);
        }

        /// <summary>
        /// Generates a table of contents for a markdown file and converts headers to anchors.
        /// </summary>
        /// <param name="markdownFilePath">The path to the markdown file.</param>
        /// <returns>The markdown content with the table of contents inserted and headers converted to anchors.</returns>
        public string[] GenerateTableOfContents(string markdownFilePath)
        {
            if (!File.Exists(markdownFilePath))
            {
                throw new FileNotFoundException($"Markdown file not found: {markdownFilePath}");
            }

            string[] lines = File.ReadAllLines(markdownFilePath);
            List<TocEntry> tocEntries = new List<TocEntry>();
            List<string> newLines = new List<string>();

            // Regular expression to match markdown headers (H1 to H6)
            Regex headerRegex = new Regex(@"^(#+)\s+(.*)$");

            foreach (string line in lines)
            {
                Match match = headerRegex.Match(line);
                if (match.Success)
                {
                    int level = match.Groups[1].Value.Length;
                    string headerText = match.Groups[2].Value.Trim();
                    string anchor = GenerateAnchor(headerText);

                    tocEntries.Add(new TocEntry { Level = level, Text = headerText, Anchor = anchor });

                    // Convert the header line to include an anchor
                    newLines.Add($"{match.Groups[1].Value} <a id=\"{anchor}\" href=\"#{anchor}\">{headerText}</a>");
                }
                else
                {
                    newLines.Add(line);
                }
            }

            // Generate the table of contents markdown
            int position = 0;
            newLines.Insert(position++, "# Tabela de Conteúdo");
            int previousLevel = 0;
            foreach (var entry in tocEntries)
            {
                if (previousLevel < entry.Level)
                {
                    previousLevel = entry.Level;
                    newLines.Insert(position++, "<ul>");
                }
                else if (previousLevel > entry.Level)
                {
                    previousLevel = entry.Level;
                    newLines.Insert(position++, "</ul>");
                }
                newLines.Insert(position++, $"<li><a href=\"#{entry.Anchor}\">{entry.Text}</a></li>");
                
            }
            newLines.Insert(position++, "</ul>");
            return newLines.ToArray();
        }

        /// <summary>
        /// Generates a URL-friendly anchor from the header text.
        /// </summary>
        /// <param name="headerText">The text of the header.</param>
        /// <returns>The generated anchor.</returns>
        private string GenerateAnchor(string headerText)
        {
            // Convert to lowercase
            string anchor = headerText.ToLowerInvariant();

            // Remove special characters and spaces, replace spaces with hyphens
            anchor = Regex.Replace(anchor, @"[^a-z0-9\s-]", "");
            anchor = Regex.Replace(anchor, @"\s+", "-");

            // Remove leading and trailing hyphens
            anchor = anchor.Trim('-');

            return anchor;
        }

        private class TocEntry
        {
            public int Level { get; set; }
            public string Text { get; set; }
            public string Anchor { get; set; }
        }

        public string[] CreateToc(string markdownFilePath)
        {
            try
            {
                return GenerateTableOfContents(markdownFilePath);
            }
            catch (FileNotFoundException ex)
            {
                FireShowMessage($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                FireShowMessage($"An error occurred: {ex.Message}");
            }
            return null;
        }
    }
}

