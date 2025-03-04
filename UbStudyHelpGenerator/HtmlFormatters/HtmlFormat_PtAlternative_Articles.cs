//using Markdig;
//using Markdig.Syntax;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using UbStudyHelpGenerator.UbStandardObjects;

namespace UbStudyHelpGenerator.HtmlFormatters
{
    internal class HtmlFormat_PtAlternative_Articles
    {
        private class Artigo
        {
            public string Titulo { get; set; }
            public string Sumario { get; set; }
            public string Link { get; set; }

            public override string ToString()
            {
                return $"Título: {Titulo}\nSumário: {Sumario}\nLink: {Link}\n";
            }
        }

        private List<Artigo> ExtrairArtigos(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return new List<Artigo>();
            }

            List<Artigo> artigos = new List<Artigo>();

            // Usando Regex para encontrar blocos de texto formatados como artigos.
            // Isso é mais robusto para lidar com variações na formatação, como espaços extras.
            string pattern = @"Título:\s*(.+)\s*Sumário:\s*(.+)\s*Link:\s*(.+)";
            MatchCollection matches = Regex.Matches(input, pattern, RegexOptions.Multiline);

            foreach (Match match in matches)
            {
                Artigo artigo = new Artigo
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
            string filePath = Path.Combine(StaticObjects.Parameters.EditBookRepositoryFolder, @"articles/articles.txt");
            string articleMarkdown = File.ReadAllText(filePath);
            List<Artigo> artigos = ExtrairArtigos(articleMarkdown);
            foreach (Artigo artigo in artigos)
            {
                sb.AppendLine($"<a href=\"javascript:loadArticle('{artigo.Link}')\"><h4>{artigo.Titulo}</h4></a>  ");
                sb.AppendLine($"<div>{artigo.Sumario}");
                sb.AppendLine("</div><br /><br />");
            }
        }
    }
}
