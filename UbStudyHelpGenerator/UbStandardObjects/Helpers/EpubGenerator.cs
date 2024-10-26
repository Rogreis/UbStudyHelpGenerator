using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UbStudyHelpGenerator.UbStandardObjects.Objects;
using Paragraph = UbStudyHelpGenerator.UbStandardObjects.Objects.Paragraph;

namespace UbStudyHelpGenerator.UbStandardObjects.Helpers
{
    /// <summary>
    /// Generate and epub bilingual book with English and Portuguese Alternative edition
    /// <see href="https://www.w3.org/AudioVideo/ebook/"/>
    /// <see href="https://github.com/IDPF/epub3-samples/tree/main/30/georgia-pls-ssml"/> Sample book
    /// <see href="https://www.ebookit.com/tools/bp/Bo/eBookIt/epub-validator">Epub Validation</see>
    /// </summary>
    public class EpubGenerator
    {
        private const string PackageFilePath = @"EPUB\package.opf";
        private const string PackageFileHtmlPath = "EPUB/package.opf";
        protected const string DividerString = "* * * * *";
        protected const string HtmlSpace = "&nbsp;";
        public event dlShowMessage ShowMessage = null;
        private const string CssLocation = @"EPUB\content\Epub.css";

        private string GenerateLink(string text, string href, bool useButtonFormat= true)
        {
            if (useButtonFormat)
                return $" <a class=\"link-button\" href=\"{href}\">{text}</a>";
            else
                return $"<a class=\"link-normal\" href=\"{href}\">{text}</a>";
        }

        //private class Landmark
        //{
        //    public string Title { get; set; } = "";
        //    public string Href { get; set; } = "";
        //    public string Id { get; set; } = "";
        //    public short StartDoc { get; set; } = 0;
        //    public short EndtDoc { get; set; } = 196;
        //    public bool ShouldGenerate { get; set; } = true;
        //}

        //List<Landmark> LandmarkList = new List<Landmark>()
        //{
        //    new Landmark() {Title = "Introdução", Href= "content/bl_en_pt000.xhtml", Id= "paper000", StartDoc= 0, EndtDoc= 0, ShouldGenerate= false},
        //    new Landmark() {Title = "Parte I",    Href= "partI.xhtml",               Id= "part_I", StartDoc= 1, EndtDoc= 31},
        //    new Landmark() {Title = "Parte II",   Href= "partII.xhtml",              Id= "part_II", StartDoc= 32, EndtDoc= 56},
        //    new Landmark() {Title = "Parte III",  Href= "partIII.xhtml", Id = "part_III", StartDoc = 57, EndtDoc = 119},
        //    new Landmark() {Title = "Parte IV",   Href= "partIV.xhtml", Id = "part_IV", StartDoc = 120, EndtDoc = 196},
        //};



        private string EpubHref(TUB_TOC_Entry entry)
        {
            if (entry.ParagraphNo <= 0 && entry.SectionNo <= 0)
                return $"content/bl_en_pt{entry.PaperNo:000}.xhtml";
            else
                return $"content/bl_en_pt{entry.PaperNo:000}.xhtml#U{entry.PaperNo}_{entry.SectionNo}_{entry.ParagraphNo}";
        }

        private string EpubHref(TOC_Entry entry)
        {
            if (entry.ParagraphNo <= 0 && entry.Section <= 0)
                return $"content/bl_en_pt{entry.Paper:000}.xhtml";
            else
                return $"content/bl_en_pt{entry.Paper:000}.xhtml#U{entry.Paper}_{entry.Section}_{entry.ParagraphNo}";
        }

        /// <summary>
        /// Extract a list of Papers from a translation
        /// </summary>
        /// <param name="translation"></param>
        /// <returns></returns>
        private List<TOC_Entry> PaperList(Translation translation)
        {
            List<TOC_Entry> list = new List<TOC_Entry>();
            foreach (Paper paper in translation.Papers)
            {
                list.Add(paper.Entry);
            }
            return list;
        }


 
        /// <summary>
        /// Write the mime type file and copy the css
        /// </summary>
        /// <param name="outputPath"></param>
        private void StaticFiles(string outputPath)
        {
            string mimeTypeFile = Path.Combine(outputPath, "mimetype");
            // Create the content
            string content = "application/epub+zip";
            // Write the content to the file with UTF-8 encoding
            File.WriteAllText(mimeTypeFile, content, Encoding.ASCII);

            string cssSource = Path.Combine(System.Windows.Forms.Application.StartupPath, @"Epub\Epub.css");
            string cssDestination = Path.Combine(outputPath, CssLocation);
            File.Copy(cssSource, cssDestination, true);
        }

        /// <summary>
        /// Print the container file
        /// All the ebook content files will be in the Package File  (PackageFilePath)
        /// <see href="https://github.com/IDPF/epub3-samples/blob/main/30/georgia-pls-ssml/META-INF/container.xml"/>
        /// </summary>
        /// <param name="epubFolder"></param>
        private void ContainerFile(string outputPath)
        {
            Directory.CreateDirectory(Path.Combine(outputPath, "META-INF"));
            string containerFile = Path.Combine(outputPath, "META-INF", "container.xml");
            // Create and write the container.xml file
            string containerXml = $@"<?xml version=""1.0""?>
            <container version=""1.0"" xmlns=""urn:oasis:names:tc:opendocument:xmlns:container"">
                <rootfiles>
                    <rootfile full-path=""{PackageFileHtmlPath}"" media-type=""application/oebps-package+xml"" />
                </rootfiles>
            </container>";
            File.WriteAllText(containerFile, containerXml, Encoding.UTF8);
        }



        private void CoverPage(string title, string[] description, string outputPath, string epubFolder)
        {
            //const string imagesFolderName = "images";
            //string pathImages = Path.Combine(epubFolder, imagesFolderName);
            //Directory.CreateDirectory(pathImages);
 
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>  ");
            sb.AppendLine("<html xmlns=\"http://www.w3.org/1999/xhtml\" xml:lang=\"en-US\" lang=\"en-US\">  ");
            sb.AppendLine("	<head>  ");
            sb.AppendLine($"		<title>{title}</title>  ");
            sb.AppendLine("		<link rel=\"stylesheet\" type=\"text/css\" href=\"content/Epub.css\"/>  ");
            sb.AppendLine("		<meta charset=\"utf-8\"/>  ");
            sb.AppendLine("		<style> ");
            sb.AppendLine("			body { ");
            sb.AppendLine("				text-align: center; ");
            sb.AppendLine("				font-family: Arial, sans-serif; ");
            sb.AppendLine("			} ");
            sb.AppendLine(" ");
            sb.AppendLine("			.cover-page { ");
            sb.AppendLine("				width: 100%; ");
            sb.AppendLine("				max-width: 600px; ");
            sb.AppendLine("				margin: auto; ");
            sb.AppendLine("			} ");
            sb.AppendLine(" ");
            sb.AppendLine("			.top-title { ");
            sb.AppendLine("				font-size: 24px; ");
            sb.AppendLine("				font-weight: bold; ");
            sb.AppendLine("				margin-top: 10px; ");
            sb.AppendLine("			} ");
            sb.AppendLine(" ");
            sb.AppendLine("			.cover-image { ");
            sb.AppendLine("				width: 100%; ");
            sb.AppendLine("				height: auto; ");
            sb.AppendLine("			} ");
            sb.AppendLine(" ");
            sb.AppendLine("			.book-info h1 { ");
            sb.AppendLine("				font-size: 28px; ");
            sb.AppendLine("				margin-top: 20px; ");
            sb.AppendLine("			} ");
            sb.AppendLine(" ");
            sb.AppendLine("			.book-info p { ");
            sb.AppendLine("				font-size: 18px; ");
            sb.AppendLine("			} ");
            sb.AppendLine(" ");
            sb.AppendLine("		</style> ");
            sb.AppendLine("	</head>  ");
            sb.AppendLine("	<body>  ");
            sb.AppendLine("		<div class=\"cover-page\"> ");
            //sb.AppendLine($"			<div class=\"top-title\">{title}</div> ");
            //sb.AppendLine("			<img src=\"images/logo.png\" alt=\"The Urantia Book EN-PT\" class=\"cover-image\" /> ");
            sb.AppendLine("			<div class=\"book-info\"> ");
            sb.AppendLine($"				<h1>{title}</h1> ");
            foreach (string descr in description)
            {
                sb.AppendLine($"				<p>{descr}</p> ");
            }
            sb.AppendLine("			</div> ");
            sb.AppendLine("		</div> ");
            sb.AppendLine("		</body>  ");
            sb.AppendLine("</html>  ");

            string coverFilePath = Path.Combine(epubFolder, "cover.xhtml");
            File.WriteAllText(coverFilePath, sb.ToString(), Encoding.UTF8);
        }

        private void PackageFile(string title, string opfFile, short paperNo)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?> ");
            sb.AppendLine("<package xmlns=\"http://www.idpf.org/2007/opf\" ");
            sb.AppendLine("         version=\"3.0\" ");
            sb.AppendLine("         unique-identifier=\"uid\" ");
            sb.AppendLine("         xml:lang=\"en-US\"> ");
            sb.AppendLine("   <metadata xmlns:dc=\"http://purl.org/dc/elements/1.1/\"> ");
            sb.AppendLine("      <dc:identifier id=\"uid\">code.google.com.epub-samples.georgia-pls-ssml</dc:identifier> ");
            sb.AppendLine($"      <dc:title id=\"t1\">{title}</dc:title> ");
            sb.AppendLine("      <meta refines=\"#t1\" property=\"title-type\">main</meta> ");
            sb.AppendLine("      <meta refines=\"#t1\" property=\"display-seq\">3</meta> ");
            sb.AppendLine($"      <dc:title id=\"full-title\">{title}</dc:title> ");
            sb.AppendLine("      <meta refines=\"#full-title\" property=\"title-type\">expanded</meta> ");
            sb.AppendLine("      <dc:title id=\"t2\">Alternative Portuguese PT-BR translation -  a work in permanent progress</dc:title> ");
            sb.AppendLine("      <meta refines=\"#t2\" property=\"title-type\">collection</meta> ");
            sb.AppendLine("      <meta refines=\"#t2\" property=\"display-seq\">1</meta> ");
            sb.AppendLine($"      <dc:title id=\"t3\">{title}</dc:title> ");
            sb.AppendLine("      <meta refines=\"#t3\" property=\"title-type\">edition</meta> ");
            sb.AppendLine("      <meta refines=\"#t3\" property=\"display-seq\">2</meta> ");
            sb.AppendLine("      <dc:language id=\"dclang\">en-US</dc:language> ");

            DateTime utcTime = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now);
            string formattedTime = utcTime.ToString("yyyy-MM-ddTHH:mm:ssZ");
            sb.AppendLine($"      <meta property=\"dcterms:modified\" id=\"mod\">{utcTime.ToString("yyyy-MM-ddTHH:mm:ssZ")}</meta> ");

            sb.AppendLine("      <dc:creator id=\"aut1\">Various</dc:creator> ");
            sb.AppendLine("      <meta refines=\"#aut1\" property=\"role\" scheme=\"marc:relators\">aut</meta> ");
            sb.AppendLine("      <meta property=\"dcterms:source\">https://rogreis.github.io</meta> ");
            sb.AppendLine("   </metadata> ");
            sb.AppendLine("   <manifest> ");
            sb.AppendLine("      <item href=\"cover.xhtml\" id=\"cover\" media-type=\"application/xhtml+xml\"/>  ");
            //sb.AppendLine("	     <item href=\"images/logo.png\" id=\"cover-image\" properties=\"cover-image\" media-type=\"image/png\"/> ");
            sb.AppendLine("      <item href=\"content/Epub.css\" id=\"css1\" media-type=\"text/css\"/>  ");
            //sb.AppendLine("      <item href=\"css/synth.css\" id=\"css2\" media-type=\"text/css\"/>  ");

            // Link for the content files
            //foreach (int i in paperSet)
            //    sb.AppendLine($"      <item href=\"content/bl_en_pt{i:000}.xhtml\" id=\"paper{i:000}\" media-type=\"application/xhtml+xml\"/>  ");

            sb.AppendLine($"      <item href=\"content/bl_en_pt{paperNo:000}.xhtml\" id=\"paper{paperNo:000}\" media-type=\"application/xhtml+xml\"/>  ");

            // Toc must be after the content pages
            sb.AppendLine("     <item href=\"toc.xhtml\" id=\"toc\" media-type=\"application/xhtml+xml\" properties=\"nav\"/>  ");

            //// Landmark pages
            //// Landmark pages in the spine
            //foreach (Landmark landmark in LandmarkList)
            //{
            //    if (landmark.ShouldGenerate)
            //        sb.AppendLine($"		<item id=\"{landmark.Id}\" href=\"{landmark.Href}\" media-type=\"application/xhtml+xml\"/> ");
            //}


            // Copia as fontes de texto
            string sourceDirectory = @"C:\Urantia\Epub\fonts";
            string destinationDirectory = Path.Combine(Path.GetDirectoryName(opfFile), "fonts");
            // Create the destination directory if it doesn't exist
            Directory.CreateDirectory(destinationDirectory);
            // Copy all files from the source directory to the destination directory
            foreach (string file in Directory.GetFiles(sourceDirectory))
            {
                string destinationFile = Path.Combine(destinationDirectory, Path.GetFileName(file));
                File.Copy(file, destinationFile, true);
            }



            // Fonts
            sb.AppendLine("		<item id=\"epub.embedded.font.1\" href=\"fonts/UbuntuMono-B.ttf\" media-type=\"application/vnd.ms-opentype\"/> ");
            sb.AppendLine("		<item id=\"epub.embedded.font.2\" href=\"fonts/UbuntuMono-BI.ttf\" media-type=\"application/vnd.ms-opentype\"/> ");
            sb.AppendLine("		<item id=\"epub.embedded.font.3\" href=\"fonts/UbuntuMono-R.ttf\" media-type=\"application/vnd.ms-opentype\"/> ");
            sb.AppendLine("		<item id=\"epub.embedded.font.4\" href=\"fonts/UbuntuMono-RI.ttf\" media-type=\"application/vnd.ms-opentype\"/> ");
            sb.AppendLine("		<item id=\"epub.embedded.font.5\" href=\"fonts/FreeSerif.otf\" media-type=\"application/vnd.ms-opentype\"/> ");
            sb.AppendLine("		<item id=\"epub.embedded.font.6\" href=\"fonts/FreeSansBold.otf\" media-type=\"application/vnd.ms-opentype\"/> ");

            sb.AppendLine("   </manifest> ");

            // Create the spine element that indicates the order for internal files to be read
            sb.AppendLine("   <spine> ");
            sb.AppendLine("      <itemref idref=\"cover\" linear=\"no\"/> ");

            //// Landmark pages in the spine
            //foreach (Landmark landmark in LandmarkList)
            //{
            //    if (landmark.ShouldGenerate)
            //        sb.AppendLine($"      <itemref idref=\"{landmark.Id}\"/> ");
            //}
            //sb.AppendLine("      <itemref idref=\"toc\" linear=\"no\"/> ");

            //foreach (int i in paperSet)
            //{
            //    sb.AppendLine($"      <itemref idref=\"paper{i:000}\"/> ");
            //}
            sb.AppendLine($"      <itemref idref=\"paper{paperNo:000}\"/> ");
            sb.AppendLine("   </spine> ");

            sb.AppendLine("</package> ");

            File.WriteAllText(opfFile, sb.ToString(), Encoding.UTF8);
        }

        #region Table of contents
        /// <summary>
        /// Prevent a toc text to use <br />
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private string TocText(string text)
        {
            return Regex.Replace(text, "<br />", " ", RegexOptions.IgnoreCase);
        }

        private void TableOfContentsItem(StringBuilder sb, TUB_TOC_Entry entry, string ident)
        {
            if (entry.PaperNo < 0)
            {
                sb.AppendLine($"{ident}<li><span>{TocText(entry.Text)}</span> ");
            }
            else
            {
                sb.AppendLine($"{ident}<li>{GenerateLink(TocText(entry.Text), EpubHref(entry), false)} ");
            }
            TableOfContentsItems(sb, entry.Nodes, ident + "   ");
            sb.AppendLine($"{ident}</li> ");
        }

        private void TableOfContentsItems(StringBuilder sb, List<TUB_TOC_Entry> toc_entries, string ident)
        {
            if (toc_entries.Count == 0) return;
            sb.AppendLine($"{ident}<ol class=\"no_number\"> ");
            foreach (TUB_TOC_Entry entry in toc_entries)
            {
                TableOfContentsItem(sb, entry, ident + "   ");
            }
            sb.AppendLine($"{ident}</ol> ");
        }

        //private void LandmarkPage(string epubFolder, List<TOC_Entry> entries, Landmark landmark)
        //{
        //    if (!landmark.ShouldGenerate) return;

        //    StringBuilder sb = new StringBuilder();
        //    sb.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"no\"?> ");
        //    sb.AppendLine("<!DOCTYPE html> ");
        //    sb.AppendLine("<html xmlns=\"http://www.w3.org/1999/xhtml\" xmlns:epub=\"http://www.idpf.org/2007/ops\" xml:lang=\"en\" ");
        //    sb.AppendLine("	lang=\"en\"> ");
        //    sb.AppendLine("	<head> ");
        //    sb.AppendLine($"<title>{landmark.Title}</title> ");
        //    sb.AppendLine("		<link rel=\"stylesheet\" type=\"text/css\" href=\"Epub.css\" /> ");
        //    sb.AppendLine("	</head> ");
        //    sb.AppendLine("	<body> ");
        //    sb.AppendLine($"<h1>{landmark.Title}</h1> ");

        //    // Landmark content
        //    sb.AppendLine("   <ol class=\"no_number\"> ");
        //    for (short paperNo = landmark.StartDoc; paperNo <= landmark.EndtDoc; paperNo++)
        //    {
        //        TOC_Entry entry = entries.Find(e => e.Paper == paperNo && e.Section == 0 && e.ParagraphNo == 0);
        //        sb.AppendLine($"     <li>{GenerateLink($"{entry.Paper} - {TocText(entry.Text)}", EpubHref(entry), false)}</li> ");
        //    }
        //    sb.AppendLine("   </ol> ");

        //    sb.AppendLine("</body> ");
        //    sb.AppendLine("</html> ");

        //    string landmarkPath = Path.Combine(epubFolder, landmark.Href);
        //    if (File.Exists(landmarkPath)) File.Delete(landmarkPath);
        //    File.WriteAllText(landmarkPath, sb.ToString(), Encoding.UTF8);
        //}

        /// <summary>
        /// Print the top section index for each paper
        /// </summary>
        /// <param name="paper"></param>
        /// <returns></returns>
        private string PageTopIndex(Paper paper)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("  <ol class=\"no_number\"> ");
            foreach (Paragraph p in paper.Paragraphs.FindAll(p => p.ParagraphNo == 0 && p.Section > 0))
            {
                string href = $"bl_en_pt{p.Paper:000}.xhtml#U{p.Paper}_{p.Section}_{p.ParagraphNo}";
                sb.AppendLine($"    <li>{GenerateLink(p.TextNoHtml, href)}</li> ");
            }
            sb.AppendLine("  </ol> ");

            //sb.AppendLine("  <table class=\"table custom-table\"> ");
            //sb.AppendLine("     <tbody> ");
            //sb.AppendLine("      <tr> ");

            //sb.AppendLine("        <td class=\"tdPartes\"><a style=\"font-size: small;\" href=\"../partI.xhtml\">Parte I</a></td> ");
            //sb.AppendLine("        <td class=\"tdPartes\"><a style=\"font-size: small;\" href=\"../partII.xhtml\">Parte II</a></td> ");
            //sb.AppendLine("        <td class=\"tdPartes\"><a style=\"font-size: small;\" href=\"../partIII.xhtml\">Parte III</a></td> ");
            //sb.AppendLine("        <td class=\"tdPartes\"><a style=\"font-size: small;\" href=\"../partIV.xhtml\">Parte IV</a></td> ");

            //sb.AppendLine("      </tr> ");
            //sb.AppendLine("    </tbody> ");
            //sb.AppendLine("  </table> ");
            return sb.ToString();
        }

        //private void LandmarkPages(string epubFolder, List<TOC_Entry> entries)
        //{
        //    foreach (Landmark landmark in LandmarkList)
        //    {
        //        LandmarkPage(epubFolder, entries, landmark);
        //    }
        //}

        private void TableOfContents(List<TUB_TOC_Entry> toc_entries, string title, string opfFile)
        {

            StringBuilder sb = new StringBuilder();
            // Start tags
            sb.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"no\"?> ");
            sb.AppendLine("<!DOCTYPE html> ");
            sb.AppendLine("<html xmlns=\"http://www.w3.org/1999/xhtml\" xmlns:epub=\"http://www.idpf.org/2007/ops\" xml:lang=\"en\" ");
            sb.AppendLine("	lang=\"en\"> ");
            sb.AppendLine("	<head> ");
            sb.AppendLine($"<title>{title}</title> ");
            sb.AppendLine("		<link rel=\"stylesheet\" type=\"text/css\" href=\"content/Epub.css\" /> ");
            sb.AppendLine("	</head> ");
            sb.AppendLine("	<body> ");
            sb.AppendLine($"<h1>{title}</h1> ");

            //// landmarks
            //sb.AppendLine("<nav epub:type=\"landmarks\" id=\"landmarks\" hidden=\"\"> ");
            //sb.AppendLine("   <h2>Conteúdo desta edição</h2> ");
            //sb.AppendLine("   <ol class=\"no_number\"> ");
            //sb.AppendLine("       <li><a epub:type=\"toc\" href=\"#toc\">Documentos</a></li> ");
            ////foreach (Landmark landmark in LandmarkList)
            ////{
            ////    sb.AppendLine($"       <li><a epub:type=\"{landmark.Id}\" href=\"{landmark.Href}\">{landmark.Title}</a></li> ");
            ////}
            //sb.AppendLine("   </ol> ");
            //sb.AppendLine("</nav> ");


            sb.AppendLine("<nav epub:type=\"toc\" id=\"toc\" role=\"doc-toc\"> ");
            sb.AppendLine("<h2>Table of Contents</h2> ");
            sb.AppendLine("<ol class=\"no_number\"> ");

            string ident = "   ";
            // Generated code
            foreach (TUB_TOC_Entry entry in toc_entries)
            {
                TableOfContentsItem(sb, entry, ident);
            }

            // Close tags
            sb.AppendLine("</ol> ");
            sb.AppendLine("</nav> ");

            sb.AppendLine("</body> ");
            sb.AppendLine("</html> ");

            // toc.ncx
            File.WriteAllText(opfFile, sb.ToString(), Encoding.UTF8);
        }

        #endregion

        #region Paper print

        private string BackToTopLink(string text)
        {
            return $"<br />{GenerateLink(text, "#top")}";
        }

        private void PrintLine(StringBuilder sb, string textLeft, string textRigth)
        {
            sb.AppendLine(" <tr>");
            sb.AppendLine($"<td>{textLeft}</td>");
            sb.AppendLine($"<td>{textRigth}</td>");
            sb.AppendLine("</tr>");
        }

        private void PrintLine(StringBuilder sb, Paragraph parLeft, Paragraph parRight)
        {
            // Fix to the data; reason: divider is not in the paper json file as a paragraph type, but as a property
            ParagraphHtmlType type = parLeft.IsDivider ? ParagraphHtmlType.Divider : parLeft.Format;
            string padding = "style=\"padding: 20px;\"";
            string paddingDivider = "style=\"padding: 20px;\" text-align: center";

            sb.AppendLine("<tr>");
            switch (type)
            {
                case ParagraphHtmlType.BookTitle:
                    sb.AppendLine($"<td {padding}>{parLeft.FormatText(false, startTag: "h2")}</td>");
                    sb.AppendLine($"<td {padding}>{parRight.FormatText(true, startTag: "h2")}</td>");
                    break;
                case ParagraphHtmlType.PaperTitle:
                    sb.AppendLine($"<td {padding}>{parLeft.FormatText(false, startTag: "h3")}</td>");
                    sb.AppendLine($"<td {padding}>{parRight.FormatText(true, startTag: "h3")}</td>");
                    break;
                case ParagraphHtmlType.SectionTitle:
                    sb.AppendLine($"<td {padding}>{parLeft.FormatText(false, startTag: "h4")}</td>");
                    sb.AppendLine($"<td {padding}>{parRight.FormatText(true, startTag: "h4")}</td>");
                    break;
                case ParagraphHtmlType.Divider:
                    sb.AppendLine($"<td {paddingDivider}>{DividerString}</td>");
                    sb.AppendLine($"<td {paddingDivider}>{DividerString}</td>");
                    break;
                case ParagraphHtmlType.NormalParagraph:
                    sb.AppendLine($"<td {padding}>{parLeft.FormatText(false, startTag: "p ")}{BackToTopLink("Back to Top")}</td>");
                    sb.AppendLine($"<td {padding}>{parRight.FormatText(true, startTag: "p ")}{BackToTopLink("Voltar ao Início")}</td>");
                    break;
                case ParagraphHtmlType.IdentedParagraph:
                    sb.AppendLine($"<td class=\"blockquote-cell\">{parLeft.FormatText(false, startTag: "p")}{BackToTopLink("Back to Top")}</td>");
                    sb.AppendLine($"<td class=\"blockquote-cell\">{parRight.FormatText(true, startTag: "p")}{BackToTopLink("Voltar ao Início")}</td>");
                    break;
            }
            sb.AppendLine("</tr>");
        }

        private void EpubPaper(Paper leftPaper, Paper rightPaper, string epubFolder)
        {
            ShowMessage?.Invoke($"Paper {leftPaper.PaperNo}");

            string filePath = Path.Combine(epubFolder, $"content/bl_en_pt{leftPaper.PaperNo:000}.xhtml");
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"no\"?> ");
            sb.AppendLine("<!DOCTYPE html> ");
            sb.AppendLine("<html xmlns=\"http://www.w3.org/1999/xhtml\" xmlns:epub=\"http://www.idpf.org/2007/ops\" xml:lang=\"en\" lang=\"en\"> ");
            sb.AppendLine("	<head> ");
            sb.AppendLine("		<title>Chapter 1. Introduction</title> ");
            sb.AppendLine("		<link rel=\"stylesheet\" type=\"text/css\" href=\"Epub.css\" /> ");
            sb.AppendLine("	</head> ");
            sb.AppendLine("	<body> ");
            sb.AppendLine($" <a id=\"top\"></a>");
            sb.AppendLine("	<table> ");

            PrintLine(sb, "<h2>" + leftPaper.Title + "</h2>", "<h2>" + rightPaper.Title + "</h2>");
            PrintLine(sb, PageTopIndex(leftPaper), PageTopIndex(rightPaper));


            // Top of the page indicator
            sb.AppendLine(" ");

            for (int i = 0; i < leftPaper.Paragraphs.Count; i++)
            {
                if (i >= rightPaper.Paragraphs.Count)
                {
                    ShowMessage?.Invoke($"Missing edit paragraph {leftPaper.Entry.ParagraphID}");
                    rightPaper.Paragraphs.Add(new Paragraph(leftPaper.Paragraphs[i]));
                }
                PrintLine(sb, leftPaper.Paragraphs[i], rightPaper.Paragraphs[i]);
            }

            sb.AppendLine("	</table> ");
            sb.AppendLine("	</body> ");
            sb.AppendLine("</html> ");
            File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
        }

        #endregion

        private bool GenerateDocEpub(short paperNo, Paper leftPaper, Paper rightPaper, string ebookFolder, string outputEpubDocFile)
        {
            // Define the EPUB file structure
            string epubFolder = Path.Combine(ebookFolder, "EPUB");
            string contentFolder = Path.Combine(epubFolder, "content");
            string tocFile = Path.Combine(epubFolder, "toc.xhtml");
            string opfFile = Path.Combine(ebookFolder, PackageFilePath);

            // Title for the book and folder name
            string title = $"Doc {paperNo} - {rightPaper.Title}";

            // Create necessary directories
            Directory.CreateDirectory(contentFolder);

            // Generate the static files: mimetype and css
            StaticFiles(ebookFolder);

            // Navidation page
            //NavPage(epubFolder);

            // Print the container file
            ContainerFile(ebookFolder);

            // Print the package file
            PackageFile(title, opfFile, paperNo);

            // Print the cover page
            string[] description = { "Edição Bilingüe do Livro de Urântia EN / PT-BR", $"Versão de {DateTime.Now.ToString("dd/MM/yyyy HH:mm")}"};
            CoverPage(title, description, contentFolder, epubFolder);

            List<TUB_TOC_Entry> tocEntries = rightPaper.GetToc();
            TableOfContents(tocEntries, title, tocFile);
            //LandmarkPages(epubFolder, PaperList(rightTranslation));

            EpubPaper(leftPaper, rightPaper, epubFolder);

            //// Create the EPUB file
            if (File.Exists(outputEpubDocFile)) { File.Delete(outputEpubDocFile); }
            System.IO.Compression.ZipFile.CreateFromDirectory(ebookFolder, outputEpubDocFile);

            return true;
        }

        public void GenerateEpubWithTOC(Translation leftTranslation, Translation rightTranslation, string outputPath, HashSet<short> paperSet)
        {
            // Title for the book and folder name
            string title = $"LU - EN-PT - {DateTime.Now:yyyy-MM-dd}";
            
            // Create the main folder
            string ebookFolder = Path.Combine(outputPath, "EN-PT");
            Directory.CreateDirectory(ebookFolder);

            // Create a folder for each paper
            foreach(short paperNo in paperSet)
            {
                string paperPolder = Path.Combine(ebookFolder, $"Doc{paperNo:000}");
                Directory.CreateDirectory(paperPolder);
                string outputEpubDocFile = Path.Combine(outputPath, $"DOC_{paperNo:000}.epub");
                Paper leftPaper = leftTranslation.Papers[paperNo];
                Paper rightPaper = rightTranslation.Papers.ToList().Find(p => p.PaperNo == paperNo);
                GenerateDocEpub(paperNo, leftPaper, rightPaper, paperPolder, outputEpubDocFile);
            }

        }
    }
}




