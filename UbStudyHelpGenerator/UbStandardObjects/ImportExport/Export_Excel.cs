using DocumentFormat.OpenXml.Spreadsheet;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UbStudyHelpGenerator.UbStandardObjects.ImportExport.Models;

namespace UbStudyHelpGenerator.UbStandardObjects.ImportExport
{
    public class Export_Excel : Export_0base
    {

        private void CopyToClipboard(List<ParagraphExport> englishParagraphs, List<ParagraphExport> ptBrParagraphs, string destinationFolder, short paperNo)
        {
        }



        protected override void FillPaper(LiteDatabase db, string destinationFolder, short paperNo)
        {
            StaticObjects.FireShowPaperNumber(paperNo);
            List<ParagraphExport> listEnglish = GetPaper(db, BookEnglish.TranslationLanguage, paperNo);
            List<ParagraphExport> listPtBr = GetPaper(db, BookPtBr.TranslationLanguage, paperNo);
            short totPar= (short)Math.Max(listEnglish.Count, listPtBr.Count);

            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Reference\tPaper {paperNo}\tPT-BR {DateTime.Now.ToString("dd/MM/yyyy")}\tFormat\tCss");


            for (int i = 0; i < totPar; i++)
            {
                string englishText = i >= listEnglish.Count ? "MISSING" : listEnglish[i].Text;
                string ptBrText = i >= listPtBr.Count ? "MISSING" : listPtBr[i].Text;
                string reference= i >= listEnglish.Count ? "ERR" : listEnglish[i].Reference;
                string format= i >= listEnglish.Count ? "ERR" : ((ParagraphExportHtmlType)listEnglish[i].Format).ToString();
                string css = i >= listPtBr.Count ? "ERR" : listPtBr[i].CssClass;
                sb.AppendLine($"{reference}\t{englishText}\t{ptBrText}\t{format}\t{css}");
            }
            System.Windows.Forms.Clipboard.SetText(sb.ToString());



            CopyToClipboard(listEnglish, listPtBr, destinationFolder, paperNo);
            StaticObjects.FireShowMessage("Text copied to clipboard, paper {paperNo}");
        }

        /// <summary>
        /// Export the bilingual html English/Portuguese for rogreis.github.io
        /// </summary>
        /// <param name="pathBase">Destination folder</param>
        /// <param name="pathDatabase">LiteDb file location</param>
        public override void Run(string paperNoString, string pathDatabase)
        {
            short paperNo= Convert.ToInt16(paperNoString);
            using (var db = new LiteDatabase(pathDatabase))
            {
                FillPaper(db, "", paperNo);
            }
        }


    }
}
