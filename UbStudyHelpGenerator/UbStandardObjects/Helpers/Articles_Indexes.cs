using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UbStudyHelpGenerator.Classes;

namespace UbStudyHelpGenerator.UbStandardObjects.Helpers
{
    /// <summary>
    /// Represents a node in a tree structure.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TreeNode<T>
    {
        public T Data { get; set; }
        public TreeNode<T> Parent { get; set; }
        public List<TreeNode<T>> Children { get; set; }

        public TreeNode(T data)
        {
            Data = data;
            Children = new List<TreeNode<T>>();
        }

        public void AddChild(TreeNode<T> child)
        {
            child.Parent = this;
            Children.Add(child);
        }
    }

    /// <summary>
    /// Represents a tree structure.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Tree<T>
    {
        public TreeNode<T> Root { get; set; }

        public Tree(T rootData)
        {
            Root = new TreeNode<T>(rootData);
        }

        // Add methods for traversal, search, etc. if needed
    }

    public class Articles_Indexes
    {

        private string CssClass_UL_Element = "nested";
        // private string ExpandableLi = "caret expandable"; kept here for documentation
        private string ExpandableLi = "caret";
        private string CssClassHtml_A = "liIndex";

        /// <summary>
        /// Convert a string to a slug (URL-friendly format)
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private static string Slugify<T>(T text)
        {
            string textNew = text.ToString().ToLowerInvariant();
            textNew = Regex.Replace(textNew, @"[^\w\s-]", "");  // remove punctuation
            textNew = Regex.Replace(textNew, @"[\s_]+", "-");   // replace spaces/underscores with dashes
            return textNew.Trim('-');
        }


        /// <summary>
        /// Create an html li element for the index
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="pageName"></param>
        /// <param name="title"></param>
        /// <param name="level"></param>
        private void AddLiElement<T>(StringBuilder sb, TreeNode<T> node, int level, bool hasChieldNodes)
        {
            string htmlId = Slugify<T>(node.Data);
            string title = node.Data.ToString();
            // href="javascript:jumpToAnchor(anchorId)"
            string span = hasChieldNodes ? $"<span class=\"{ExpandableLi}\">" : "";
            string link = $"<a class=\"{CssClassHtml_A}\" href=\"javascript:jumpToAnchor('{htmlId}')\">";
            string closeNodes = hasChieldNodes ? "</a></span>" : "</a></li>";
            sb.AppendLine($"{identLevelForTocArticles(level)}<li>{span}{link}{System.Net.WebUtility.HtmlEncode(title)}{closeNodes}");
        }

        /// <summary>
        /// Generate the indentation for the index of articles
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sb"></param>
        /// <param name="node"></param>
        /// <param name="level"></param>
        private void PrintNode<T>(StringBuilder sb, TreeNode<T> node, ref int level)
        {
            bool hasChieldNodes = node.Children.Count > 0;
            AddLiElement<T>(sb, node, level, hasChieldNodes);
            if (hasChieldNodes)
            {
                sb.AppendLine($"{identLevelForTocArticles(level)}<ul class=\"{CssClass_UL_Element}\">");
                level++;
                foreach (var child in node.Children)
                {
                    PrintNode(sb, child, ref level);
                }
                level--;
                sb.AppendLine($"{identLevelForTocArticles(level)}</ul>");
                sb.AppendLine($"{identLevelForTocArticles(level)}</li>");
            }
        }

        /// <summary>
        /// Generate the home button for the index of articles
        /// </summary>
        /// <param name="sb"></param>
        private void TocHomeButton(StringBuilder sb)
        {
            sb.AppendLine("<p><a href=\"javascript:loadArticle('README.html')\" data-bs-toggle=\"tooltip\" data-bs-placement=\"top\" title=\"Página Inicial\">🏠 Página Inicial</a></p>");
        }




        /// <summary>
        /// Generate indentation for the index
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        private string identLevelForTocArticles(int level)
        {
            string ident = "";
            for (int i = 0; i < level; i++)
            {
                ident += "    ";
            }
            return ident;
        }

        public void Run(string pathMarkdownFile, string pathHtmlFile)
        {
            var lines = File.ReadAllLines(pathMarkdownFile);
            string tocFilePath = pathHtmlFile.Replace(".html", ".toc");

            Tree<string> headerTree = null;
            TreeNode<string> parentNode = null;
            int level = 0;

            foreach (var line in lines)
            {
                var match = Regex.Match(line, @"^(#{1,6})\s+(.*)");
                if (!match.Success) continue;

                int newLevel = match.Groups[1].Value.Length;
                string title = match.Groups[2].Value.Trim();

                if (level == 0)
                {
                    headerTree = new Tree<string>(title);
                    parentNode = headerTree.Root;
                    // Waiting for level 2 nodes
                    level = 2;
                    continue;
                }

                TreeNode<string> newNode = new TreeNode<string>(title);
                newNode.Parent = parentNode;
                if (newLevel == level)
                {
                    parentNode.Children.Add(newNode);
                }
                else if (newLevel > level)
                {
                    parentNode = parentNode.Children.LastOrDefault();
                    parentNode.Children.Add(newNode);
                    level = newLevel;
                }
                else if (newLevel < level)
                {
                    level = newLevel;
                    parentNode = parentNode.Parent;
                    parentNode.Children.Add(newNode);
                }
            }
            level = 0;
            StringBuilder html = new StringBuilder();
            html.AppendLine("<div class=\"treeview\">");
            TocHomeButton(html);
            level++;
            html.AppendLine($"{identLevelForTocArticles(level)}<ul>");
            level++;
            PrintNode<string>(html, headerTree.Root, ref level);
            level--;
            for (int newLevel = level; newLevel >= 0; newLevel--)
            {
                html.AppendLine($"{identLevelForTocArticles(newLevel)}</ul>");
            }
            html.AppendLine("</div>");

            File.WriteAllText(tocFilePath, html.ToString(), Encoding.UTF8);
            EventsControl.FireShowMessage($"Generated '{tocFilePath}' successfully.");
        }

    }
}
