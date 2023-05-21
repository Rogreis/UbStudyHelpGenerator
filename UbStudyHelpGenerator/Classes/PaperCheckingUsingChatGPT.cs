using DocumentFormat.OpenXml;
//using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Packaging;
//using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using UBT_WebSite.Classes;
using Run = DocumentFormat.OpenXml.Wordprocessing.Run;
using Table = DocumentFormat.OpenXml.Wordprocessing.Table;
using Text = DocumentFormat.OpenXml.Wordprocessing.Text;
using TableRow = DocumentFormat.OpenXml.Wordprocessing.TableRow;
using Path = System.IO.Path;
using File = System.IO.File;
using UbStudyHelpGenerator.UbStandardObjects.Objects;
using UbStudyHelpGenerator.UbStandardObjects;
using Paragraph = DocumentFormat.OpenXml.Wordprocessing.Paragraph;
using HtmlAgilityPack;
using DocumentFormat.OpenXml.Drawing.Diagrams;


namespace UbStudyHelpGenerator.Classes
{


    public class TranslationTextLine
    {
        public string Text1 { get; set; }
        public string Text2 { get; set; }
        public string Text3 { get; set; }
        public string Text4 { get; set; }
        public TranslationTextLine()
        {
        }
        public TranslationTextLine(string t1, string t2, string t3, string t4)
        {
            Text1 = t1;
            Text2 = t2;
            Text3 = t3;
            Text4 = t4;
        }
    }



    internal class PaperCheckingUsingChatGPT
    {
        /*
            <p>
                This is a 
                <span style="color:red;">red</span> 
                and this is a 
                <span style="color:green;">green</span> 
                text.
            </p>
         * */

        public List<TranslationTextLine> TranslationListData = null;

        public event dlShowMessage ShowMessageEvent = null;
        public event dlShowExceptionMessage ShowExceptionMessage = null;

        // dlShowExceptionMessage(string message, Exception ex, bool isFatal = false);
        private void ShowMessage(string message, bool isError = false, bool isFatal = false)
        {
            ShowMessageEvent?.Invoke(message, isError, isFatal);
        }

        private void ShowMessage(string message, Exception ex, bool isFatal = false)
        {
            ShowExceptionMessage?.Invoke(message, ex, isFatal);
        }



        //private void CreateWordprocessingDocumentWithHtmlColoredText(string filepath, string html)
        //{
        //    using (WordprocessingDocument wordDocument = WordprocessingDocument.Create(filepath, WordprocessingDocumentType.Document))
        //    {
        //        // Add a main document part.
        //        MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();

        //        // Create the document structure and add some text.
        //        mainPart.Document = new Document();
        //        Body body = mainPart.Document.AppendChild(new Body());

        //        // Create a new paragraph
        //        Paragraph para = body.AppendChild(new Paragraph());

        //        // Parse the HTML
        //        var htmlDoc = new HtmlDocument();
        //        htmlDoc.LoadHtml(html);

        //        // Extract the spans
        //        var spans = htmlDoc.DocumentNode.SelectNodes("//span");

        //        foreach (var span in spans)
        //        {
        //            // Get the color
        //            var style = span.GetAttributeValue("style", "");
        //            var color = style.Split(':')[1].Trim();

        //            // Map the color to a Word color
        //            string wordColor;
        //            switch (color)
        //            {
        //                case "red":
        //                    wordColor = "FF0000";
        //                    break;
        //                case "green":
        //                    wordColor = "00FF00";
        //                    break;
        //                default:
        //                    wordColor = "000000"; // Default to black
        //                    break;
        //            }

        //            // Add a run with colored text
        //            Run run = para.AppendChild(new Run());
        //            RunProperties runProps = run.AppendChild(new RunProperties());
        //            runProps.AppendChild(new Color { Val = wordColor });
        //            run.AppendChild(new Text(span.InnerText));
        //        }
        //    }
        //}


        //private string RemovedTest(string text)
        //{
        //    return $"<span style=\"color:red;\">{text}</span>";
        //}

        //private string InsertedTest(string text)
        //{
        //    return $"<span style=\"color:green;\">{text}</span>";
        //}



        private string ShowDifferences(string oldHtmlText, string newHtmlText)
        {
            Merger merger = new Merger(oldHtmlText, newHtmlText);
            return merger.merge();
        }

        public void CreateWordprocessingDocument(string filepath)
        {
            using (WordprocessingDocument wordDocument =
                WordprocessingDocument.Create(filepath, WordprocessingDocumentType.Document))
            {
                // Add a main document part. 
                MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();

                // Create the document structure and add some text.
                mainPart.Document = new Document();
                Body body = mainPart.Document.AppendChild(new Body());

                // Create a Table
                Table table = new Table();

                // Add 3 rows to the table
                for (int i = 0; i < 3; i++)
                {
                    TableRow row = new TableRow();

                    // Add 4 cells to each row
                    for (int j = 0; j < 4; j++)
                    {
                        TableCell cell = new TableCell();
                        cell.Append(new Paragraph(new Run(new Text($"Cell {i + 1}-{j + 1}"))));
                        row.Append(cell);
                    }

                    table.Append(row);
                }
                body.Append(table);
            }
        }

        private void AddText(TableRow row, string text)
        {
            // Create a new table cell
            TableCell cell = new TableCell();

            // Create a new cell properties object
            TableCellProperties cellProperties = new TableCellProperties();

            // Create a new cell margin object and specify the left, right, top, and bottom padding
            TableCellMargin cellMargin = new TableCellMargin();

            cellMargin.TopMargin = new TopMargin() { Width = "100" }; // Units in twentieths of a point
            cellMargin.BottomMargin = new BottomMargin() { Width = "100" };
            cellMargin.LeftMargin = new LeftMargin() { Width = "100" };
            cellMargin.RightMargin = new RightMargin() { Width = "100" };

            // Add the cell margin to the cell properties
            cellProperties.Append(cellMargin);

            // Add the cell properties to the cell
            cell.Append(cellProperties);


            // Set cell width so that each cell is 25% of the table width.
            cell.Append(new TableCellProperties(new TableCellWidth { Type = TableWidthUnitValues.Pct, Width = "25" }));
            cell.Append(new Paragraph(new Run(new Text(text))));
            row.Append(cell);
        }

        ///// <summary>
        ///// Create a 4 columns word file to be used for translations
        ///// </summary>
        ///// <param name="filePath"></param>
        ///// <returns></returns>
        //private Table CreateWordFile(string filePath)
        //{
        //    WordprocessingDocument wordDocument = WordprocessingDocument.Create(filePath, WordprocessingDocumentType.Document);
        //    MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();
        //    // Create the document structure and add some text.
        //    mainPart.Document = new Document();
        //    Body body = mainPart.Document.AppendChild(new Body());
        //    SectionProperties sectionProps = new SectionProperties();
        //    PageSize pageSize = new PageSize() { Height = (UInt32Value)12240U, Width = (UInt32Value)15840U, Orient = PageOrientationValues.Landscape };
        //    PageMargin pageMargin = new PageMargin() { Top = 360, Right = (UInt32Value)360U, Bottom = 360, Left = (UInt32Value)360U, Header = (UInt32Value)720U, Footer = (UInt32Value)720U, Gutter = (UInt32Value)0U };
        //    sectionProps.Append(pageSize, pageMargin);
        //    body.Append(sectionProps);


        //    // Create a Table
        //    Table table = new Table();

        //    // Define the borders
        //    TableBorders tblBorders = new TableBorders(
        //        new TopBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 12 },
        //        new BottomBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 12 },
        //        new LeftBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 12 },
        //        new RightBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 12 },
        //        new InsideHorizontalBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 12 },
        //        new InsideVerticalBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 12 }
        //    );

        //    TableCellMargin defaultTableCellMargin = new TableCellMargin() { TopMargin = new TopMargin() { Width = "300", Type = TableWidthUnitValues.Dxa }, LeftMargin = new LeftMargin() { Width = "300", Type = TableWidthUnitValues.Dxa } };

        //    // Apply the borders, padding and width to the table
        //    //table.AppendChild<TableProperties>(new TableProperties(tblBorders, new TableCellProperties(new TableCellWidth { Type = TableWidthUnitValues.Pct, Width = "25" }, defaultTableCellMargin)));


        //    // Apply the borders to the table
        //    //table.AppendChild<TableProperties>(new TableProperties(tblBorders, defaultTableCellMargin));
        //    table.AppendChild<TableProperties>(new TableProperties(defaultTableCellMargin));


        //    body.Append(table);
        //    return table;

        //}

        private string MakeFullOuputFileName(string pathInputFile, string appendToName, int nrCopy)
        {
            string directory = Path.GetDirectoryName(pathInputFile);
            string filename = Path.GetFileNameWithoutExtension(pathInputFile);
            string extension = Path.GetExtension(pathInputFile);

            // Create a new filename with the copy number appended, zero-padded to 3 digits
            string newFilename = $"{filename}_{appendToName}{nrCopy:000}{extension}";

            // Combine the directory and new filename to get the full path
            return Path.Combine(directory, newFilename);
        }

        private string ChangeFileExtension(string pathInputFile, string newExtension)
        {
            string directory = Path.GetDirectoryName(pathInputFile);
            string filename = Path.GetFileNameWithoutExtension(pathInputFile);

            // Create a new filename with the copy number appended, zero-padded to 3 digits
            string newFilename = $"{filename}{newExtension}";

            // Combine the directory and new filename to get the full path
            return Path.Combine(directory, newFilename);
        }

        private string MakeOutputFile(string pathInputFile, string appendToName)
        {
            int nrCopy = 1;

            string filePathOut = MakeFullOuputFileName(pathInputFile, appendToName, nrCopy);
            bool fileExists = File.Exists(filePathOut);
            while (fileExists)
            {
                nrCopy++;
                if (nrCopy > 999)
                {
                    throw new Exception($"To many files: {filePathOut}");
                }
                filePathOut = MakeFullOuputFileName(pathInputFile, appendToName, nrCopy);
                fileExists = File.Exists(filePathOut);
            }
            return filePathOut;
        }

        private void ImportWordFile(string filePathIn)
        {
            using (WordprocessingDocument wordDocument = WordprocessingDocument.Open(filePathIn, false))
            {
                Body body = wordDocument.MainDocumentPart.Document.Body;
                Table table = body.Elements<Table>().First();

                foreach (TableRow row in table.Elements<TableRow>())
                {
                    List<TableCell> cells = row.Elements<TableCell>().ToList();
                    TranslationTextLine obj = new TranslationTextLine
                    {
                        Text1 = cells[0].InnerText,
                        Text2 = cells[1].InnerText,
                        Text3 = cells[2].InnerText,
                        Text4 = cells[2].InnerText
                    };
                    TranslationListData.Add(obj);
                }
            }
        }

        private void ExportListToWord(List<TranslationTextLine> list, string filePath)
        {
            using (WordprocessingDocument wordDocument =
                   WordprocessingDocument.Create(filePath, WordprocessingDocumentType.Document))
            {
                // Add a main document part. 
                MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();

                // Create the document structure and add some text.
                mainPart.Document = new Document();
                Body body = mainPart.Document.AppendChild(new Body());

                SectionProperties sectionProps = new SectionProperties();
                PageSize pageSize = new PageSize() { Height = (UInt32Value)12240U, Width = (UInt32Value)15840U, Orient = PageOrientationValues.Landscape };
                PageMargin pageMargin = new PageMargin() { Top = 360, Right = (UInt32Value)360U, Bottom = 360, Left = (UInt32Value)360U, Header = (UInt32Value)720U, Footer = (UInt32Value)720U, Gutter = (UInt32Value)0U };
                sectionProps.Append(pageSize, pageMargin);
                body.Append(sectionProps);


                // Create a Table
                Table table = new Table();

                // Define the borders
                TableBorders tblBorders = new TableBorders(
                    new TopBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 12 },
                    new BottomBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 12 },
                    new LeftBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 12 },
                    new RightBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 12 },
                    new InsideHorizontalBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 12 },
                    new InsideVerticalBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 12 }
                );

                TableCellMargin defaultTableCellMargin = new TableCellMargin() { TopMargin = new TopMargin() { Width = "300", Type = TableWidthUnitValues.Dxa }, LeftMargin = new LeftMargin() { Width = "300", Type = TableWidthUnitValues.Dxa } };

                // Apply the borders, padding and width to the table
                //table.AppendChild<TableProperties>(new TableProperties(tblBorders, new TableCellProperties(new TableCellWidth { Type = TableWidthUnitValues.Pct, Width = "25" }, defaultTableCellMargin)));


                // Apply the borders to the table
                //table.AppendChild<TableProperties>(new TableProperties(tblBorders, defaultTableCellMargin));
                table.AppendChild<TableProperties>(new TableProperties(defaultTableCellMargin));

                // Iterate through each item in the list and add the data to the table.
                for (int i = 0; i < list.Count; i++)
                {
                    TableRow row = new TableRow();
                    AddText(row, list[i].Text1);
                    AddText(row, list[i].Text2);
                    AddText(row, list[i].Text3);
                    AddText(row, list[i].Text4);
                    table.Append(row);
                }
                body.Append(table);
            }

        }

        /// <summary>
        /// Read all lines from a GPT translation file
        /// Create the file id not existing
        /// </summary>
        /// <param name="wordFilePathIn">Path of the word inout file</param>
        /// <returns></returns>
        private List<string> GetGptTranslationTextList(string wordFilePathIn)
        {
            string pathTextInputFile = ChangeFileExtension(wordFilePathIn, ".txt");
            if (!File.Exists(pathTextInputFile))
            {
                File.WriteAllText(pathTextInputFile, "");
                return new List<string>();
            }
            string[] lines = File.ReadAllLines(pathTextInputFile);
            return lines.ToList();
        }

        /// <summary>
        /// Get the line number count from the gpt tranalstion list
        /// </summary>
        /// <param name="list"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        private string GetGptTranslationLine(List<string> list, int count)
        {
            try
            {
                return list[count];
            }
            catch 
            {
                return "";
            }
        }

        #region UAI routines
        /// <summary>
        /// Used to make a word file for UAI translations
        /// </summary>
        /// <param name="filePathIn"></param>
        /// <returns></returns>
        public bool ImportListFromWord(string wordFilePathIn)
        {
            try
            {
                string filePathOut = MakeOutputFile(wordFilePathIn, "Work");
                TranslationListData = new List<TranslationTextLine>();
                ImportWordFile(wordFilePathIn);
                ExportListToWord(TranslationListData, filePathOut);
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Import the text file with GPT-4 translations for the UAI document
        /// </summary>
        /// <param name="filePathIn"></param>
        /// <returns></returns>
        public bool ImportTextFile(string filePathIn)
        {
            try
            {
                List<string> gptTranslationList = GetGptTranslationTextList(filePathIn);
                
                string filePathOut = MakeOutputFile(filePathIn, "Work");
                TranslationListData = new List<TranslationTextLine>();
                ImportWordFile(filePathIn);

                int contLines = 0;
                bool firstLine = true;
                foreach (TranslationTextLine line in TranslationListData)
                {
                    if (!firstLine)
                    {
                        line.Text3 = GetGptTranslationLine(gptTranslationList, contLines++);
                        line.Text4 = "";
                        contLines++;
                    }
                    firstLine = false;
                    if (contLines >= gptTranslationList.Count) break;
                }
                ExportListToWord(TranslationListData, filePathIn);
                return true;
            }
            catch (Exception)
            {
                throw;
            }

        }

        #endregion

        #region PtAlternative Routines

        /// <summary>
        /// Generate a word file for the indicated paper
        /// Gets the English translation from the json and the current PT Alternative from local repository
        /// </summary>
        /// <param name="filePathIn"></param>
        /// <returns></returns>
        public bool GeneratePtAlternativeWord(string workFolderPath, short paperNo)
        {
            try
            {
                string filePathOut = MakeOutputFile(Path.Combine(workFolderPath, $"PtAlternative.docx"), "Compare");

                // C:\Trabalho\Github\Rogerio\Ub\rogreis.github.io\PaperTranslations
                List<string> gptTranslationList = GetGptTranslationTextList(filePathOut);

                Translation ptAlternative = new Translation();

                StaticObjects.Book.EditTranslation.Paper(paperNo);
                TranslationListData = new List<TranslationTextLine>();

                int parNo = 0;
                Paper paper = StaticObjects.Book.EditTranslation.Papers.Find(p => p.PaperNo == paperNo);
                if (paper != null)
                {
                    foreach (UbStandardObjects.Objects.Paragraph par in paper.Paragraphs)
                    {
                        UbStandardObjects.Objects.Paragraph p1 = StaticObjects.Book.EnglishTranslation.Papers[paperNo].Paragraphs[parNo];
                        string gptText= GetGptTranslationLine(gptTranslationList, parNo++);
                        TranslationListData.Add(new TranslationTextLine(p1.Text, par.Text, gptText, ""));
                    }

                    //ExportListToWord(TranslationListData, filePathOut);
                    //CreateWordprocessingDocumentWithHtmlColoredText(filePathOut, string html)
                    //ExportListToWord(TranslationListData, filePathOut);
                    return true;
                }
                else
                {
                    ShowMessage($"Paper {paperNo} not found in edit translation");
                    return false;
                }

            }
            catch (Exception ex)
            {
                ShowMessage("Error generating PtAlternative", ex, true);
                throw;
            }
        }



        public bool CompareListFromWord(string filePathIn)
        {
            try
            {
                string filePathOut = MakeOutputFile(filePathIn, "Compare");
                TranslationListData = new List<TranslationTextLine>();

                using (WordprocessingDocument wordDocument = WordprocessingDocument.Open(filePathIn, false))
                {
                    Body body = wordDocument.MainDocumentPart.Document.Body;
                    Table table = body.Elements<Table>().First();

                    foreach (TableRow row in table.Elements<TableRow>())
                    {
                        List<TableCell> cells = row.Elements<TableCell>().ToList();
                        TranslationTextLine obj = new TranslationTextLine
                        {
                            Text1 = cells[0].InnerText,
                            Text2 = cells[1].InnerText,
                            Text3 = cells[2].InnerText
                        };

                        obj.Text4 = ShowDifferences(obj.Text2, obj.Text3);

                        TranslationListData.Add(obj);
                    }
                }
                ExportListToWord(TranslationListData, filePathOut);
                return true;
            }
            catch (Exception ex)
            {
                ShowMessage("Error comparing PtAlternative", ex, true);
                throw;
            }
        }

        #endregion


    }
}
