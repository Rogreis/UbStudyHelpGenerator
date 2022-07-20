using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;

namespace UbStudyHelpGenerator.Classes
{
    internal class ExportToWord
    {

        private object SaveChanges = Word.WdSaveOptions.wdDoNotSaveChanges;
        private object OriginalFormat = Word.WdOriginalFormat.wdOriginalDocumentFormat;
        private object RouteDocument = false;
        private object readOnly = false;
        private object isVisible = true;
        private object missing = System.Reflection.Missing.Value;
        private object oMissing = System.Reflection.Missing.Value;
        private object oEndOfDoc = "\\endofdoc"; /* \endofdoc is a predefined bookmark */


        private void CopyHtmlToClipBoard(string html)
        {
            Encoding enc = Encoding.UTF8;

            string begin = "Version:0.9\r\nStartHTML:{0:000000}\r\nEndHTML:{1:000000}"
               + "\r\nStartFragment:{2:000000}\r\nEndFragment:{3:000000}\r\n";

            string html_begin = "<html>\r\n<head>\r\n"
               + "<meta http-equiv=\"Content-Type\""
               + " content=\"text/html; charset=" + enc.WebName + "\">\r\n"
               + "<title>HTML clipboard</title>\r\n</head>\r\n<body>\r\n"
               + "<!--StartFragment-->";

            string html_end = "<!--EndFragment-->\r\n</body>\r\n</html>\r\n";

            string begin_sample = String.Format(begin, 0, 0, 0, 0);

            int count_begin = enc.GetByteCount(begin_sample);
            int count_html_begin = enc.GetByteCount(html_begin);
            int count_html = enc.GetByteCount(html);
            int count_html_end = enc.GetByteCount(html_end);

            string html_total = String.Format(
               begin
               , count_begin
               , count_begin + count_html_begin + count_html + count_html_end
               , count_begin + count_html_begin
               , count_begin + count_html_begin + count_html
               ) + html_begin + html + html_end;

            DataObject obj = new DataObject();
            obj.SetData(DataFormats.Html, new System.IO.MemoryStream(
               enc.GetBytes(html_total)));
            Clipboard.SetDataObject(obj, true);
        }

        private Word.Table CreateTable(Word.Application wordApp, Word.Document aDoc, int lines, int cols)
        {
            Word.Table newTable;
            Word.Range wrdRng = aDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
            newTable = aDoc.Tables.Add(wrdRng, lines, cols, ref oMissing, ref oMissing);
            newTable.Borders.InsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
            newTable.Borders.OutsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
            newTable.AllowAutoFit = true;
            return newTable;
        }


        //public void ExportToWord(DataGridView dgv)
        //{

        //    word.ApplicationClass word = null;



        //    word.Document doc = null;
        //    try
        //    {
        //        word = new word.ApplicationClass();
        //        word.Visible = true;
        //        doc = word.Documents.Add(ref oMissing, ref oMissing, ref oMissing, ref oMissing);
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorLog(ex);
        //    }
        //    finally
        //    {
        //    }
        //    if (word != null && doc != null)
        //    {
        //        word.Table newTable;
        //        word.Range wrdRng = doc.Bookmarks.get_Item(ref oEndOfDoc).Range;
        //        newTable = doc.Tables.Add(wrdRng, 1, dgv.Columns.Count - 1, ref oMissing, ref oMissing);
        //        newTable.Borders.InsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
        //        newTable.Borders.OutsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
        //        newTable.AllowAutoFit = true;

        //        foreach (DataGridViewCell cell in dgv.Rows[0].Cells)
        //        {
        //            newTable.Cell(newTable.Rows.Count, cell.ColumnIndex).Range.Text = dgv.Columns[cell.ColumnIndex].Name;

        //        }
        //        newTable.Rows.Add();

        //        foreach (DataGridViewRow row in dgv.Rows)
        //        {
        //            foreach (DataGridViewCell cell in row.Cells)
        //            {
        //                newTable.Cell(newTable.Rows.Count, cell.ColumnIndex).Range.Text = cell.Value.ToString();
        //            }
        //            newTable.Rows.Add();
        //        }
        //    }

        //}

        private void InsertText(Word.Row row, int cellNo, string htmlText)
        {
            Word.Cell cell = row.Cells[cellNo];
            cell.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
            cell.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalTop;
            cell.LeftPadding = 10;
            cell.RightPadding = 10;
            cell.BottomPadding = 10;

            CopyHtmlToClipBoard(htmlText);

            // PasteSpecial has seven reference parameters, all of which are
            // optional. This example uses named arguments to specify values
            // for two of the parameters. Although these are reference
            // parameters, you do not need to use the ref keyword, or to create
            // variables to send in as arguments. You can send the values directly.
            cell.Range.PasteSpecial(missing, missing, missing, missing, Word.WdPasteDataType.wdPasteHTML, missing, missing);
        }


        public void Export(string pathWordFile, List<PT_AlternativeRecord> list)
        {
            Word.Application wordApp = new Word.Application();
            Word.Document aDoc = new Word.Document();
            wordApp.Visible = true;

            // The Add method has four reference parameters, all of which are
            // optional. Visual C# allows you to omit arguments for them if
            // the default values are what you want.
            aDoc = wordApp.Documents.Add(ref missing, ref missing, ref missing, ref missing);

            aDoc.Application.Options.Pagination = false;
            aDoc.Application.ScreenUpdating = false;
            aDoc.PageSetup.Orientation = Word.WdOrientation.wdOrientLandscape;

            Word.Table table = CreateTable(wordApp, aDoc, list.Count, 3);
            int lineNumber = 1;
            foreach (PT_AlternativeRecord p in list)
            {
                Word.Row row = table.Rows[lineNumber];
                lineNumber++;

                InsertText(row, 1, p.English);
                InsertText(row, 2, p.Portugues2007);
                InsertText(row, 3, p.Text);
            }

            object fileName = pathWordFile;
            aDoc.SaveAs(ref fileName);
            aDoc.Close();
        }
    }
}
