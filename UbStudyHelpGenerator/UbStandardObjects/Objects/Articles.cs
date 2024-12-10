using Markdig;
using System.IO;
using System.Linq;

namespace UbStudyHelpGenerator.UbStandardObjects.Objects
{
    internal class Articles
    {

        private class Articles_Data
        {
            public string Titulo { get; set; } = "";
            public string SubTitulo { get; set; } = "";
            public string Sumario { get; set; } = "";
            public string Link { get; set; } = "";

            public void Clear()
            {
                Titulo = "";
                SubTitulo = "";
                Sumario = "";
                Link = "";
            }
        }

        private string GenerateBootstrapCard(Articles_Data article)
        {
            return $@"
                    <div class=""card"" style=""width: 440px;"">
                      <div class=""card-body"">
                        <h5 class=""card-title"">{article.Titulo}</h5>
                        <h6 class=""card-subtitle mb-2 text-muted"">{article.SubTitulo}</h6>
                        <p class=""card-text"">{article.Sumario}</p>
                        <a href=""javascript:open_article('{article.Link}')#"" class=""card-link"">Abrir artigo</a>
                      </div>
                    </div>
                    ";
        }

        public string MakeCardList(string filePath)
        {
            string html = "";
            var lines = File.ReadAllLines(filePath);

            Articles_Data currentArticle = new Articles_Data();

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    // If an empty line is found, finalize the current article and prepare for the next.
                    if (!string.IsNullOrEmpty(currentArticle.Titulo))
                    {
                        html += GenerateBootstrapCard(currentArticle);
                        currentArticle.Clear();
                    }
                    continue;
                }

                var parts = line.Split(new[] { ':' }, 2);
                if (parts.Length < 2) continue; // Ignore malformed lines

                var key = parts[0].Trim();
                var value = parts[1].Trim();

                if (currentArticle == null)
                    currentArticle = new Articles_Data();

                // Set the property dynamically based on the key.
                switch (key)
                {
                    case "Titulo":
                        currentArticle.Titulo = value;
                        break;
                    case "SubTitulo":
                        currentArticle.SubTitulo = value;
                        break;
                    case "Sumário":
                        currentArticle.Sumario = value;
                        break;
                    case "Link":
                        currentArticle.Link = value;
                        break;
                }
            }

            // Add the last article if it exists.
            if (!string.IsNullOrEmpty(currentArticle.Titulo))
            {
                html += GenerateBootstrapCard(currentArticle);
                currentArticle.Clear();
            }
            return html;
        }


        public string MarkdownToHtml(string markdown)
        {
            var pipeline = new MarkdownPipelineBuilder().Build();
            var document = Markdown.Parse(markdown, pipeline);
            return document.ToHtml();
        }

        public string MarkdownToHtmlOld(string markdown)
        {
            var lines = markdown.Split('\n');
            var html = "";

            foreach (var line in lines)
            {
                if (line.StartsWith("# "))
                {
                    var level = line.Count(c => c == '#');
                    html += $"<h{level}>{line.Substring(level + 1).Trim()}</h{level}>";
                }
                else if (line.StartsWith("* "))
                {
                    html += "<ul><li>" + line.Substring(2).Trim() + "</li></ul>";
                }
                else
                {
                    html += "<p>" + line + "</p>";
                }
            }

            return html;
        }

    }

}
