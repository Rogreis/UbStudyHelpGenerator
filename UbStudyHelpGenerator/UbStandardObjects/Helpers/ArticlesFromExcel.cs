using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UbStudyHelpGenerator.UbStandardObjects.Objects;
using static UbStudyHelpGenerator.Classes.EventsControl;

namespace UbStudyHelpGenerator.UbStandardObjects.Helpers
{
    public class ArticlesFromExcel
    {
        public event ShowMessageDelegate ShowMessageEvent;

        private void ShowMessage(string message)
        {
            ShowMessageEvent?.Invoke(message);
        }

        private string FormataLink(TOC_Entry entry)
        {
            return $"**<a href=\"javascript:showParagraph({entry.Paper},{entry.Section},{entry.ParagraphNo})\" title=\"Abrir o parágrafo {entry.Ident}\">{entry.Ident}</a>**";
        }


        public void UniqueTable()
        {
            ShowMessage(null);

            GetDataFiles dataFiles = new GetDataFiles();
            Translation trans = dataFiles.GetTranslation(2);

            string pathExcel = @"C:\Urantia\PT-BR\Moral.xlsm";
            string sheetName = "Referencias";
            Excel excel = new Excel();
            if (!excel.Open(pathExcel))
            {
                ShowMessage($"Excel not open: {pathExcel}");
                return;
            }

            int lines = 0, columns = 0;
            excel.GetTotalColumnsLines(sheetName, ref lines, ref columns);

            Dictionary<int, HorizontalAlignment> alignments = excel.GetColumnAlignments(sheetName);
            foreach (var alignment in alignments)
            {
                Console.WriteLine($"Column {alignment.Key}: {alignment.Value}");
            }

            ShowMessage(null);
            ShowMessage("|No.|Assunto|Referência|Texto|Grupo|");
            ShowMessage("|:---:|:---|:---:|:---|:---|");


            for (int lin = 2; lin <= lines; lin++)
            {
                int number = excel.ReadValue<int>(sheetName, lin, 1);
                string assunto = excel.ReadValue<string>(sheetName, lin, 2);
                string referencia = excel.ReadValue<string>(sheetName, lin, 3);
                string grupo = excel.ReadValue<string>(sheetName, lin, 5);


                TOC_Entry entry = TOC_Entry.FromHref(referencia);
                PaperEdit paper = new PaperEdit(entry.Paper, StaticObjects.Parameters.EditParagraphsRepositoryFolder);
                Paragraph par = (from p in paper.Paragraphs
                                 where p.Entry * entry
                                 select p).FirstOrDefault();
                ShowMessage($"|{number}|{assunto}|{FormataLink(entry)}|{par.Text}|{grupo}|");
            }
        }

        public void ByGroups()
        {
            ShowMessage(null);

            GetDataFiles dataFiles = new GetDataFiles();
            Translation trans = dataFiles.GetTranslation(2);

            string pathExcel = @"C:\Urantia\PT-BR\Moral.xlsm";
            string sheetName = "Referencias";
            Excel excel = new Excel();
            if (!excel.Open(pathExcel))
            {
                ShowMessage($"Excel not open: {pathExcel}");
                return;
            }

            int lines = 0, columns = 0;
            excel.GetTotalColumnsLines(sheetName, ref lines, ref columns);

            Dictionary<int, HorizontalAlignment> alignments = excel.GetColumnAlignments(sheetName);
            foreach (var alignment in alignments)
            {
                Console.WriteLine($"Column {alignment.Key}: {alignment.Value}");
            }


            string lastGroup = "";

            for (int lin = 2; lin <= lines; lin++)
            {
                int number = excel.ReadValue<int>(sheetName, lin, 1);
                string assunto = excel.ReadValue<string>(sheetName, lin, 2);
                string referencia = excel.ReadValue<string>(sheetName, lin, 3);
                string group = excel.ReadValue<string>(sheetName, lin, 5);

                if (group != lastGroup)
                {
                    ShowMessage("");
                    ShowMessage("---");
                    ShowMessage($"## {group}");
                    lastGroup= group;
                    ShowMessage("");
                    ShowMessage("|Assunto|Referência|Texto|");
                    ShowMessage("|:---|:---:|:---|");
                }

                TOC_Entry entry = TOC_Entry.FromHref(referencia);
                PaperEdit paper = new PaperEdit(entry.Paper, StaticObjects.Parameters.EditParagraphsRepositoryFolder);
                Paragraph par = (from p in paper.Paragraphs
                                 where p.Entry * entry
                                 select p).FirstOrDefault();
                ShowMessage($"|{assunto}|{FormataLink(entry)}|{par.Text}|");
            }
            ShowMessage("");
            ShowMessage("---");
        }

    }
}
