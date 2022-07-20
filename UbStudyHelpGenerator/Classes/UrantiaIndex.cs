using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using static UbStudyHelpGenerator.Classes.EventsControl;

namespace UbStudyHelpGenerator.Classes
{
    public class UrantiaIndex
    {
        public event ShowMessageDelegate ShowMessage = null;

        private class indextPage
        {
            public string Url;
            public string Ident;
            public string FileName;
            public indextPage(string url, string ident, string fileName)
            {
                Url = url;
                Ident = ident;
                FileName = fileName;
            }
        }


        public void SplitIndexEntry()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<p><strong>Zacharias</strong> death of, <a href=\"https://www.urantia.org/urantia-book-standardized/paper-135-john-baptist#U135_2_0\">135:2.0</a> (1497.3&#8211;6)<br /> ");
            sb.AppendLine("  &nbsp;&nbsp;&nbsp;&nbsp; doubt of, as to Gabriel&rsquo;s visit to Elizabeth, <a href=\"https://www.urantia.org/urantia-book-standardized/paper-122-birth-and-infancy-jesus#U122_2_5\">122:2.5</a> (1345.7), <a href=\"https://www.urantia.org/urantia-book-standardized/paper-135-john-baptist#U135_0_1\">135:0.1</a> (1496.1)<br /> ");
            sb.AppendLine("  &nbsp;&nbsp;&nbsp;&nbsp; dream of, effect, <a href=\"https://www.urantia.org/urantia-book-standardized/paper-135-john-baptist#U135_0_1\">135:0.1</a> (1496.1)<br /> ");
            sb.AppendLine("  &nbsp;&nbsp;&nbsp;&nbsp; education of, <a href=\"https://www.urantia.org/urantia-book-standardized/paper-135-john-baptist#U135_0_4\">135:0.4</a> (1496.4)<br /> ");
            sb.AppendLine("  &nbsp;&nbsp;&nbsp;&nbsp; and Elizabeth, belief of, in missions of John and Jesus, <a href=\"https://www.urantia.org/urantia-book-standardized/paper-122-birth-and-infancy-jesus#U122_8_4\">122:8.4</a> (1351.8)<br /> ");
            sb.AppendLine("  &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; bringing of John to Nazarites by, <a href=\"https://www.urantia.org/urantia-book-standardized/paper-135-john-baptist#U135_1_1\">135:1.1</a> (1496.6)<br /> ");
            sb.AppendLine("  &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; precautions of, during Herod&rsquo;s pursuit of the Bethlehem babe, <a href=\"https://www.urantia.org/urantia-book-standardized/paper-122-birth-and-infancy-jesus#U122_10_1\">122:10.1</a> (1353.28)<br /> ");
            sb.AppendLine("  &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; reactions of, to John&rsquo;s birth, <a href=\"https://www.urantia.org/urantia-book-standardized/paper-122-birth-and-infancy-jesus#U122_2_7\">122:2.7</a> (1346.2)<br /> ");
            sb.AppendLine("  &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; sheep farm of, <a href=\"https://www.urantia.org/urantia-book-standardized/paper-135-john-baptist#U135_0_5\">135:0.5</a> (1496.5)<br /> ");
            sb.AppendLine("  &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; visit to Nazareth family by, <a href=\"https://www.urantia.org/urantia-book-standardized/paper-123-early-childhood-jesus#U123_3_4\">123:3.4</a> (1359.5)<br /> ");
            sb.AppendLine("  &nbsp;&nbsp;&nbsp;&nbsp; friendship of, with Simeon and Anna, <a href=\"https://www.urantia.org/urantia-book-standardized/paper-122-birth-and-infancy-jesus#U122_9_2\">122:9.2</a> (1353.1)<br /> ");
            sb.AppendLine("  &nbsp;&nbsp;&nbsp;&nbsp; identity of, <a href=\"https://www.urantia.org/urantia-book-standardized/paper-122-birth-and-infancy-jesus#U122_2_1\">122:2.1</a> (1345.3), <a href=\"https://www.urantia.org/urantia-book-standardized/paper-135-john-baptist#U135_0_1\">135:0.1</a> (1496.1)<br /> ");
            sb.AppendLine("  &nbsp;&nbsp;&nbsp;&nbsp; Joseph&rsquo;s conference with, concerning Jesus, <a href=\"https://www.urantia.org/urantia-book-standardized/paper-122-birth-and-infancy-jesus#U122_8_4\">122:8.4</a> (1351.8)<br /> ");
            sb.AppendLine("  &nbsp;&nbsp;&nbsp;&nbsp; reaction of, to Elizabeth&rsquo;s story of Gabriel&rsquo;s visit, <a href=\"https://www.urantia.org/urantia-book-standardized/paper-122-birth-and-infancy-jesus#U122_2_5\">122:2.5</a> (1345.7)<br /> ");
            sb.AppendLine("  &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; to impressive dream before John&rsquo;s birth, <a href=\"https://www.urantia.org/urantia-book-standardized/paper-122-birth-and-infancy-jesus#U122_2_5\">122:2.5</a> (1345.7), <a href=\"https://www.urantia.org/urantia-book-standardized/paper-135-john-baptist#U135_0_1\">135:0.1</a> (1496.1)<br /> ");
            sb.AppendLine("  &nbsp;&nbsp;&nbsp;&nbsp; role of, in Joseph&rsquo;s and Mary&rsquo;s flight to Egypt, <a href=\"https://www.urantia.org/urantia-book-standardized/paper-122-birth-and-infancy-jesus#U122_10_4\">122:10.4</a> (1354.3)<br /> ");
            sb.AppendLine("  &nbsp;&nbsp;&nbsp;&nbsp; schooling of John by, <a href=\"https://www.urantia.org/urantia-book-standardized/paper-135-john-baptist#U135_0_4\">135:0.4</a> (1496.4), <a href=\"https://www.urantia.org/urantia-book-standardized/paper-135-john-baptist#U135_3_2\">135:3.2</a> (1498.1)<br /> ");
            sb.AppendLine("  &nbsp;&nbsp;&nbsp;&nbsp; sending of Ur priests to Bethlehem by, <a href=\"https://www.urantia.org/urantia-book-standardized/paper-122-birth-and-infancy-jesus#U122_8_5\">122:8.5</a> (1352.1)</p> ");
            sb.AppendLine(" ");

            var htmlDoc = new HtmlAgilityPack.HtmlDocument();
            htmlDoc.LoadHtml(sb.ToString());


            var x = htmlDoc.DocumentNode.ChildNodes.Count;


            //    RemoteWebElement p = new RemoteWebElement(null, "p");
            //    p.Text = ;

            //    IWebElement strong = p.FindElement(By.TagName("strong"));
            //    ReadOnlyCollection<IWebElement> links = p.FindElements(By.TagName("a"));
            //    if (links != null && links.Count > 0)
            //    {
            //        string strongText = "";
            //        List<string> references = new List<string>();
            //        string parText = p.Text;
            //        foreach (IWebElement elem in p.FindElements(By.XPath(".//*")))
            //        {
            //            if (elem.TagName == "strong")
            //            {
            //                strongText = elem.Text;
            //            }
            //            if (elem.TagName == "a")
            //            {
            //                references.Add(elem.Text);
            //            }
            //        }
            //    }

        }

        private void Download(indextPage index)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(index.Url);
            string pathFile = @"C:\Urantia\Index\Originais\" + index.FileName + ".htm";
            System.IO.File.WriteAllText(pathFile, doc.Text);
        }


        #region Download Index
        //private ChromeDriver driver = null;

        private List<indextPage> GetList()
        {
            List<indextPage> list = new List<indextPage>();

            list.Add(new indextPage("https://www.urantia.org/urantia-foundations-first-index-urantia-book/aa-az", "Aa - Az", "aa-az"));
            list.Add(new indextPage("https://www.urantia.org/urantia-foundations-first-index-urantia-book/ba-bz", "Ba - Bz", "ba-bz"));
            list.Add(new indextPage("https://www.urantia.org/urantia-foundations-first-index-urantia-book/ca-cz", "Ca - Cz", "ca-cz"));
            list.Add(new indextPage("https://www.urantia.org/urantia-foundations-first-index-urantia-book/da-dz", "Da - Dz", "da-dz"));
            list.Add(new indextPage("https://www.urantia.org/urantia-foundations-first-index-urantia-book/ea-ez", "Ea - Ez", "ea-ez"));
            list.Add(new indextPage("https://www.urantia.org/urantia-foundations-first-index-urantia-book/fa-fz", "Fa - Fz", "fa-fz"));
            list.Add(new indextPage("https://www.urantia.org/urantia-foundations-first-index-urantia-book/ga-gz", "Ga - Gz", "ga-gz"));
            list.Add(new indextPage("https://www.urantia.org/urantia-foundations-first-index-urantia-book/ha-hz", "Ha - Hz", "ha-hz"));
            list.Add(new indextPage("https://www.urantia.org/urantia-foundations-first-index-urantia-book/ia-iz", "Ia - Iz", "ia-iz"));
            list.Add(new indextPage("https://www.urantia.org/urantia-foundations-first-index-urantia-book/ja-jz", "Ja - Jz", "ja-jz"));
            list.Add(new indextPage("https://www.urantia.org/urantia-foundations-first-index-urantia-book/ka-kz", "Ka - Kz", "ka-kz"));
            list.Add(new indextPage("https://www.urantia.org/urantia-foundations-first-index-urantia-book/la-lz", "La - Lz", "la-lz"));
            list.Add(new indextPage("https://www.urantia.org/urantia-foundations-first-index-urantia-book/ma-mz", "Ma - Mz", "ma-mz"));
            list.Add(new indextPage("https://www.urantia.org/urantia-foundations-first-index-urantia-book/na-nz", "Na - Nz", "na-nz"));
            list.Add(new indextPage("https://www.urantia.org/urantia-foundations-first-index-urantia-book/oa-oz", "Oa - Oz", "oa-oz"));
            list.Add(new indextPage("https://www.urantia.org/urantia-foundations-first-index-urantia-book/pa-pz", "Pa - Pz", "pa-pz"));
            list.Add(new indextPage("https://www.urantia.org/urantia-foundations-first-index-urantia-book/qa-qz", "Qa - Qz", "qa-qz"));
            list.Add(new indextPage("https://www.urantia.org/urantia-foundations-first-index-urantia-book/ra-rz", "Ra - Rz", "ra-rz"));
            list.Add(new indextPage("https://www.urantia.org/urantia-foundations-first-index-urantia-book/sa-sz", "Sa - Sz", "sa-sz"));
            list.Add(new indextPage("https://www.urantia.org/urantia-foundations-first-index-urantia-book/ta-tz", "Ta - Tz", "ta-tz"));
            list.Add(new indextPage("https://www.urantia.org/urantia-foundations-first-index-urantia-book/ua-uz", "Ua - Uz", "ua-uz"));
            list.Add(new indextPage("https://www.urantia.org/urantia-foundations-first-index-urantia-book/va-vz", "Va - Vz", "va-vz"));
            list.Add(new indextPage("https://www.urantia.org/urantia-foundations-first-index-urantia-book/wa-wz", "Wa - Wz", "wa-wz"));
            list.Add(new indextPage("https://www.urantia.org/urantia-foundations-first-index-urantia-book/xa-xz", "Xa - Xz", "xa-xz"));
            list.Add(new indextPage("https://www.urantia.org/urantia-foundations-first-index-urantia-book/ya-yz", "Ya - Yz", "ya-yz"));
            list.Add(new indextPage("https://www.urantia.org/urantia-foundations-first-index-urantia-book/za-zz", "Za - Zz", "za-zz"));
            return list;
        }


        //private class IndexDetails
        //{
        //    public string Text { get; set; }

        //    public string Details { get; set; }

        //    public List<string> References { get; set; } = new List<string>();
        //}

        //private class IndexTexts
        //{
        //    public List<IndexDetails> Details { get; set; } = new List<IndexDetails>();
        //}

        //public string GetUbIndex(string url)
        //{
        //    try
        //    {
        //        IndexTexts indexTexts = new IndexTexts();

        //        //FireShowMessage(null);
        //        //FireShowMessage("Getting UB Index");

        //        driver.Url = url;
        //        Thread.Sleep(5000);

        //        IWebElement hmtl = driver.FindElement(By.TagName("html"));

        //        string x = hmtl.Text;


        //        IWebElement body = driver.FindElement(By.ClassName("node-inner"));
        //        IWebElement content = body.FindElement(By.ClassName("content"));


        //        foreach (IWebElement p in content.FindElements(By.TagName("p")))
        //        {
        //            // <strong>Sabbath</strong>
        //            IWebElement strong = p.FindElement(By.TagName("strong"));
        //            ReadOnlyCollection<IWebElement> links = p.FindElements(By.TagName("a"));
        //            if (links != null && links.Count > 0)
        //            {
        //                string strongText = "";
        //                List<string> references = new List<string>();
        //                string parText = p.Text;
        //                foreach (IWebElement elem in p.FindElements(By.XPath(".//*")))
        //                {
        //                    /*
        //                        <strong>Sabbath</strong>
        //                        afternoon walks, of Joseph and Jesus, 
        //                        (1363.5)
        //                        <a href="https://www.urantia.org/urantia-book-standardized/paper-123-early-childhood-jesus#U123_5_12">123:5.12</a>
        //                        <br>

        //                        Sabbath afternoon walks, of Joseph and Jesus, 123:5.12 (1363.5)
        //                             at Bethany, 172:1.0 (1878.4–1880.1)
        //                             breaking, Josiah and Jesus charged with, 164:4.8 (1814.4)
        //                                  Pharisees’ accusations of Jesus’, 169:0.6 (1850.6)
        //                                       attempts to entrap Jesus into, 147:6.3 (1654.2), 148:7.2 (1665.1)
        //                                       hypocrisy about, 164:4.1 (1813.4)
        //                             day, Jesus’ query about healing on the, 167:1.4 (1834.2)
        //                                  Jewish rulers’ activities vs. those of Jesus on the, 162:2.2 (1790.5)
        //                             -day tradition, origin of, 74:4.6 (832.6)
        //                             day’s journey, legal length of, 147:6.3 (1654.2)
        //                             Jewish, a day of rest and worship, 185:2.7 (1990.3)
        //                             Jewish attitude toward play on the, 123:4.3 (1361.3)
        //                             Nazareth’s liberal-mindedness as to the, 123:5.12 (1363.5)
        //                             at the pool of Bethesda, 147:3.1 (1649.1)
        //                             preaching of Jesus in the Jewish synagogues on the, 146:4.1 (1643.2)
        //                             query of the man with the withered hand as to legality of being healed on the, 148:7.2 (1665.1)
        //                             rites, Jesus’ inquiry into the meaning of, 123:3.5 (1359.6)
        //                             services, Jesus’ reading of the scriptures at the, 123:5.4 (1362.5)
        //                             Son of Man lord of the, 147:6.4 (1654.3)
        //                             at Tiberias, 150:3.0 (1680.3–1681.7)
        //                             was made for man and not man for the Sabbath, 147:6.4 (1654.3)


        //                    */
        //                    if (elem.TagName == "strong")
        //                    {
        //                        strongText = elem.Text;
        //                    }
        //                    if (elem.TagName == "a")
        //                    {
        //                        references.Add(elem.Text);
        //                    }
        //                }

        //                FireShowMessage("");
        //                FireShowMessage(strongText);

        //                IndexDetails indexDetails = new IndexDetails()
        //                {
        //                    Text = strongText,
        //                    References = references,
        //                    Details = parText,
        //                };

        //                indexTexts.Details.Add(indexDetails);

        //            }

        //        }

        //        return JsonConvert.SerializeObject(indexTexts);


        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        #endregion




        public class Detail
        {
            public int DetailType { get; set; }
            public string Text { get; set; } = "";
            public List<string> Links { get; set; } = new List<string>();

        }

        public class TubIndexEntry
        {
            public string Title { get; set; }
            public List<Detail> Details { get; set; } = new List<Detail>();
            public override string ToString()
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Title);
                foreach (Detail detail in Details)
                {
                    sb.AppendLine($"   {detail.DetailType}  {detail.Text}");
                    foreach (string link in detail.Links)
                    {
                        sb.AppendLine("      " + link);
                    }
                }
                sb.AppendLine("");
                return sb.ToString();
            }

        }


        /// <summary>
        /// Download and process each TUB file index
        /// </summary>
        /// <param name="localHtmlIndexFiles"></param>
        /// <param name="outputJsonIndexFiles"></param>
        public void GetIndex(string localHtmlIndexFiles, string outputJsonIndexFiles)
        {
            ShowMessage(null);

            List<TubIndexEntry> tubIndexEntries = new List<TubIndexEntry>();

            foreach (indextPage index in GetList())
            {
                ShowMessage(index.FileName);
                HtmlWeb web = new HtmlWeb();

                string pathIndex = Path.Combine(localHtmlIndexFiles, index.FileName + ".htm");
                var uri = new Uri(pathIndex);
                var converted = uri.AbsoluteUri;
                HtmlDocument htmlDoc = web.Load(uri);

                IEnumerable<HtmlNode> listParagraphs = htmlDoc.DocumentNode.ChildNodes.Descendants("p"); //.Where(n => n.HasClass("container"));

                foreach (HtmlNode paragraph in listParagraphs)
                {
                    TubIndexEntry entry = null;
                    Detail detail = null;

                    foreach (HtmlNode node in paragraph.ChildNodes)
                    {
                        switch (node.Name)
                        {
                            case "strong":
                                entry = new TubIndexEntry();
                                tubIndexEntries.Add(entry);
                                entry.Title = node.InnerText;
                                detail = new Detail();
                                entry.Details.Add(detail);
                                break;

                            case "br":
                                if (entry != null)
                                {
                                    detail = new Detail();
                                    entry.Details.Add(detail);
                                }
                                break;

                            case "#text":
                                if (entry != null && !node.InnerText.Trim().StartsWith("("))
                                {
                                    if (string.IsNullOrEmpty(detail.Text))
                                    {
                                        detail.Text = node.InnerText;
                                        Regex re = new Regex("&nbsp;");
                                        detail.DetailType = 100 + re.Matches(detail.Text).Count;
                                        detail.Text = detail.Text.Replace("&nbsp;", "").Replace("&rsquo;", "'").Trim();
                                    }
                                    else
                                    {
                                        detail.Text += node.InnerText;
                                    }
                                }
                                break;

                            case "a":
                                if (detail != null)
                                {
                                    if (!string.IsNullOrWhiteSpace(node.InnerText))
                                        detail.Links.Add(node.InnerText);
                                }
                                break;

                            case "em":
                                if (entry != null)
                                {
                                    if (node.InnerText.Contains("see also"))
                                    {
                                        detail = new Detail();
                                        entry.Details.Add(detail);
                                        detail.DetailType = 2;
                                        detail.Text = "See also";
                                    }
                                    else
                                    {
                                        detail.Text += node.InnerText;
                                    }
                                }
                                break;
                        }
                    }
                }
            }

            string pathJsonIndex = Path.Combine(outputJsonIndexFiles, "tubIndex_000.json");
            string jsonString = JsonSerializer.Serialize<List<TubIndexEntry>>(tubIndexEntries);
            File.WriteAllText(pathJsonIndex, jsonString);

        }

        private void JobCheckingSelenium_ShowMessage(string mensagem, bool Grave)
        {
            throw new NotImplementedException();
        }
    }
}
