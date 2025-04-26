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


            short maxPkSeq= (short)Math.Max((listEnglish.Any() ? listEnglish.Max(p => p.Pk_seq) : -1),
                                          (listPtBr.Any() ? listPtBr.Max(p => p.Pk_seq) : -1));
            short minPkSeq = (short)Math.Min((listEnglish.Any() ? listEnglish.Min(p => p.Pk_seq) : -1),
                                          (listPtBr.Any() ? listPtBr.Min(p => p.Pk_seq) : -1));

            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"PK\tReference\tPaper {paperNo}\tPT-BR {DateTime.Now.ToString("dd/MM/yyyy")}\tFormat EN\tFormat PT-BR\tCss");


            for (int pk_seq = minPkSeq; pk_seq < maxPkSeq; pk_seq++)
            {
                ParagraphExport parEnglish = listEnglish.Find(p => p.Pk_seq == pk_seq);
                ParagraphExport parPtBr = listPtBr.Find(p => p.Pk_seq == pk_seq);

                string englishText = parEnglish == null ? "MISSING" : parEnglish.Text;
                string reference = parEnglish == null ? "ERR" : parEnglish.Reference;
                string formatEnglish = parEnglish == null ? "ERR" : ((ParagraphExportHtmlType)parEnglish.Format).ToString();

                string ptBrText = parPtBr == null ? "MISSING" : parPtBr.Text;
                string formatPtBr = parPtBr == null ? "ERR" : ((ParagraphExportHtmlType)parPtBr.Format).ToString();
                string css = parPtBr == null ? "ERR" : parPtBr.CssClass;

                sb.AppendLine($"{pk_seq}\t{reference}\t{englishText}\t{ptBrText}\t{formatEnglish}\t{formatPtBr}\t{css}");
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
