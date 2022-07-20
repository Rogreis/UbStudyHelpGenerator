using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UbStudyHelpGenerator.Database;
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

        private Dictionary<string, string> DictionaryInvalidXmlCharacters()
        {
            return new Dictionary<string, string>()
                 {
                     {"&",  "&amp;"},  // must be the first
	                 {"<",  "&lt;"},
                     {">",  "&gt;"},
                     {"\"",  "&quot;"},
                     {"'", "&apos;"}
                 };
        }

        protected string XmlEncodeString(string xmlIn)
        {
            foreach (KeyValuePair<string, string> pair in DictionaryInvalidXmlCharacters())
                xmlIn = xmlIn.Replace(pair.Key, pair.Value);
            return xmlIn;
        }


        private void SplitHtml(string id, string html, ref short paper, ref short section, ref short paragraphNo, ref short page, ref short line, ref string text)
        {
            // <small>0:12.11 (16.8)</small>   
            // <small>0:8.10 (12.1)</small> 
            // <small>1:0.4 (22.1)</small> Este magnífico mandato universal de esforzarse por alcanzar la perfección de la divinidad es el primer deber, y debería ser la más alta aspiración, de toda la creación de criaturas luchadoras del Dios de perfección. Esta posibilidad de lograr la perfección divina es el destino final y cierto de todo progreso espiritual eterno del hombre.


            html = html.Replace("«", "\"").Replace("»", "\"");
            text = html.Replace(id, "");
            text = XmlEncodeString(text);

            if (id.EndsWith("F"))
            { 
                id = id.Replace("F", "");
                page = line = 0;
                return;
            }
            string[] sepId = { "U", "_" };
            string[] parts = id.Split(sepId, StringSplitOptions.RemoveEmptyEntries);
            paper = Convert.ToInt16(parts[0]);
            section = Convert.ToInt16(parts[1]);
            paragraphNo = Convert.ToInt16(parts[2]);

            if (paragraphNo == 0)
            {
                page = line = 0;
                return;
            }

            int indSmallClose= html.IndexOf("</small>");
            if (indSmallClose == -1)
            {
                page = line = 0;
                return;
            }
            text = XmlEncodeString(html.Remove(0, indSmallClose + 8));


            string[] sep2 = { "<small>", "</small>", ":", ".", "(", ")" };
            string[] parts2 = html.Split(sep2, StringSplitOptions.RemoveEmptyEntries);
            if (parts2.Length < 6)
            {
                page = line = 0;
                return;
            }
            page = Convert.ToInt16(parts2[3]);
            line = Convert.ToInt16(parts2[4]);
        }

        private void GetData(UbtDatabaseSqlServer server, string filePath)
        {

            ShowMessage?.Invoke(filePath);
            var uri = new Uri(filePath);
            var converted = uri.AbsoluteUri;

            Paper paper = new Paper();
            HtmlWeb web = new HtmlWeb();

            HtmlDocument htmlDoc = web.Load(uri);

            List<HtmlNode> divContaineres = htmlDoc.DocumentNode.Descendants("div").Where(n => n.HasClass("container")).ToList();
            if (divContaineres == null || divContaineres.Count == 0 || divContaineres.Count > 1)
            {
                ShowMessage?.Invoke("ERROR: Invalid number of divs");
                return;
            }
            HtmlNode divContainer = divContaineres.FirstOrDefault();

            int count = 0;
            int found = 0, errors = 0;
            foreach (HtmlNode node in divContainer.ChildNodes)
            {
                if (!string.IsNullOrEmpty(node.Id))
                {
                    short paperNo = 0, section = 0, paragraphNo = 0, page = 0, line = 0;
                    string text = "";
                    SplitHtml(node.Id, node.InnerHtml, ref paperNo, ref section, ref paragraphNo, ref page, ref line, ref text);
                    //string textToPrint = text; // text.Length > 40 ? text.Substring(0, 40) : text;
                    //ShowMessage?.Invoke($"{paperNo} {section} {paragraphNo} {page} {line} {textToPrint}");
                    string pk_seq = $"[dbo].[PK_Seq]({paperNo}, {section}, {paragraphNo})";
                    string command = $"INSERT INTO [dbo].[TextImport]([Paper],[Section],[Paragraph],[Page],[Line],[Text],[PK_Seq]) VALUES({paperNo}, {section}, {paragraphNo}, {page}, {line}, '{text}', {pk_seq})";
                    count = server.RunCommand(command);
                    if (count < 1)
                    {
                        errors++;
                        ShowMessage($"{command}");
                    }
                    found++;
                }
            }
            return;


        }


        internal void ProcessFiles(UbtDatabaseSqlServer server, string filesPath)
        {
            string filePath = @"C:\Urantia\Textos\Espanhol2021\04-Documento000.html";
            GetData(server, filePath);

            //int cont = 0;
            //foreach (string filePath in Directory.GetFiles(filesPath, "*.html"))
            //{
            //    if (filePath.Contains("Documento"))
            //    {
            //        GetData(server, filePath);
            //    }
            //    cont++;
            //}


        }


    }
}
