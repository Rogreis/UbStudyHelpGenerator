using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UbStudyHelpGenerator.Classes.EventsControl;

namespace UbStudyHelpGenerator.Classes
{
    public class UrantiaSpanish
    {
        public event ShowMessageDelegate ShowMessage = null;

        private enum enHtmlType
        {
            BookTitle = 0,
            PaperTitle = 1,
            SectionTitle = 2,
            NormalParagraph = 3,
            IdentedParagraph = 4
        }


        private class Paragraph
        {
            public short Paper { get; set; }
            public short Section { get; set; }
            public short ParagraphNo { get; set; }
            public short Page { get; set; }
            public short Line { get; set; }
            public string Text = "";
            public enHtmlType Format { get; set; }

            public Paragraph(string anchor, string text)
            {
                char[] sep = { '_' };
                anchor = anchor.Remove(0, 1);
                string[] parts = anchor.Split(sep, StringSplitOptions.RemoveEmptyEntries);
                Paper = Convert.ToInt16(parts[0]);
                Section = Convert.ToInt16(parts[1]);
                ParagraphNo = Convert.ToInt16(parts[2]);
                Text = text;
            }

            public override string ToString()
            {
                return $"{Paper}:{Section}-{ParagraphNo}  {Text}";
            }
        }

        private class Paper
        {
            public List<Paragraph> Paragraphs = new List<Paragraph>();
            public string Title { get; set; }
        }



        private void GetData(string filePath)
        {
            ShowMessage?.Invoke(filePath);
            var uri = new Uri(filePath);
            var converted = uri.AbsoluteUri;

            Paper paper = new Paper();

            HtmlWeb web = new HtmlWeb();
            HtmlDocument htmlDoc = web.Load(uri);

            //IEnumerable<HtmlNode> listDivContainer = htmlDoc.DocumentNode.ChildNodes.Descendants("h1").Where(n => n.HasClass("container"));
            List<HtmlNode> nodes = htmlDoc.DocumentNode.ChildNodes.Descendants("h1").ToList();
            HtmlNode nodeTitle = nodes[0];
            HtmlAttribute attributeTitle = nodeTitle.Attributes.ToList().Find(a => a.Name == "id");
            paper.Title = nodeTitle.InnerText;
            paper.Paragraphs.Add(new Paragraph(attributeTitle.Value, nodeTitle.InnerText));

            List<HtmlNode> paragraphNodes = htmlDoc.DocumentNode.ChildNodes.Descendants("p").Where(n => n.HasClass("btn")).ToList();
            foreach (HtmlNode node in paragraphNodes)
            {
                HtmlAttribute attribute = node.Attributes.ToList().Find(a => a.Name == "id");

                // <small>0:12.11 (16.8)</small>   
                // <small>0:8.10 (12.1)</small> 


                Paragraph par = new Paragraph(attribute.Value, node.InnerHtml);
                paper.Paragraphs.Add(par);
                ShowMessage?.Invoke(par.ToString());
            }


        }


        public void ProcessFiles(string filesPath)
        {
            int cont = 0;
            foreach (string filePath in Directory.GetFiles(filesPath, "*.html"))
            {
                if (filePath.Contains("Documento"))
                {
                    GetData(filePath);
                }
                cont++;
                if (cont == 5)
                    return;
            }


        }


    }
}
