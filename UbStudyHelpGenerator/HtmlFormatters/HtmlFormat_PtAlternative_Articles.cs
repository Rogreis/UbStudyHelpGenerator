using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using UbStudyHelpGenerator.UbStandardObjects;
using UbStudyHelpGenerator.UbStandardObjects.ImportExport.Models;

namespace UbStudyHelpGenerator.HtmlFormatters
{
    internal class HtmlFormat_PtAlternative_Articles
    {

        private List<ArticleIndexEntry> ExtractArticleTitleFromTextFile(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return new List<ArticleIndexEntry>();
            }

            List<ArticleIndexEntry> artigos = new List<ArticleIndexEntry>();

            // Usando Regex para encontrar blocos de texto formatados como artigos.
            // Isso é mais robusto para lidar com variações na formatação, como espaços extras.
            string pattern = @"Título:\s*(.+)\s*Sumário:\s*(.+)\s*Link:\s*(.+)";
            MatchCollection matches = Regex.Matches(input, pattern, RegexOptions.Multiline);

            foreach (Match match in matches)
            {
                ArticleIndexEntry artigo = new ArticleIndexEntry
                {
                    Titulo = match.Groups[1].Value.Trim(),
                    Sumario = match.Groups[2].Value.Trim(),
                    Link = match.Groups[3].Value.Trim()
                };
                artigos.Add(artigo);
            }

            return artigos;
        }

        public void ArticlesIndex(StringBuilder sb)
        {
            //string filePath = Path.Combine(StaticObjects.Parameters.EditBookRepositoryFolder, @"articles/articles.txt");
            //string articleMarkdown = File.ReadAllText(filePath);
            //List<ArticleIndexEntry> artigos = ExtractArticleTitleFromTextFile(articleMarkdown);
            //foreach (ArticleIndexEntry artigo in artigos)
            //{
            //    sb.AppendLine($"<a href=\"javascript:loadArticle('{artigo.Link}')\"><h4>{artigo.Titulo}</h4></a>  ");
            //    sb.AppendLine($"<div>{artigo.Sumario}");
            //    sb.AppendLine("</div><br /><br />");
            //}
        }
    }
}
