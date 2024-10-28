using System;
using System.Collections.Generic;
using HtmlAgilityPack;

namespace UbStudyHelpGenerator.Classes
{
    public class ReferenceData
    {
        public string Quote { get; set; }
        public string UBReference { get; set; }
        public List<(string BibleReference, string Url)> BibleReferences { get; set; }
    }

    public class HtmlParser
    {
    // ERRO: tem que trabalhar com cada entrada
        public ReferenceData ParseHtmlExtract(string htmlContent)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(htmlContent);

            var referenceData = new ReferenceData();
            referenceData.BibleReferences = new List<(string, string)>();

            // Extract the quote
            var textNodes = htmlDoc.DocumentNode.SelectNodes("//text()");
            if (textNodes != null && textNodes.Count > 0)
            {
                referenceData.Quote = textNodes[0].InnerText.Trim().Split(':')[0].Trim('"');
            }

            // Extract the UB reference
            var ubLink = htmlDoc.DocumentNode.SelectSingleNode("//a[contains(@href, 'UrantiaBook')]");
            if (ubLink != null)
            {
                referenceData.UBReference = ubLink.InnerText.Trim();
            }

            // Extract each Bible reference
            var bibleLinks = htmlDoc.DocumentNode.SelectNodes("//a[contains(@href, 'NewTestament')]");
            if (bibleLinks != null)
            {
                foreach (var link in bibleLinks)
                {
                    string reference = link.InnerText.Trim();
                    string url = link.GetAttributeValue("href", string.Empty).Trim();
                    referenceData.BibleReferences.Add((reference, url));
                }
            }

            return referenceData;
        }
    }
}



//// Example usage:
//public class Program
//{
//    public static void Main()
//    {
//    }
//}
