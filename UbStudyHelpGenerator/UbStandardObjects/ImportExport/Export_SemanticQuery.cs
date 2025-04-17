using LiteDB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UbStudyHelpGenerator.UbStandardObjects.ImportExport;
using UbStudyHelpGenerator.UbStandardObjects.Objects;

namespace UbStudyHelpGenerator.UbStandardObjects.Exporters
{
    public class Export_SemanticQuery : Export_0base
    {

        //private void ExportToCsv(BookExport book, string pathOutput, string language)
        //{
        //    using (var writer = new StreamWriter(pathOutput))
        //    {
        //        // Writing header
        //        writer.WriteLine("texto\tidioma\treferencia");
        //        foreach (PaperExport paper in book.Papers)
        //        {
        //            foreach (ParagraphExport paragraph in paper.Paragraphs)
        //            {
        //                writer.WriteLine($"{paragraph.Text}\t{language}\t{paragraph.Reference}");
        //            }
        //        }
        //    }
        //}


        /// <summary>
        /// Export the bilingual html English/Portuguese for rogreis.github.io
        /// </summary>
        /// <param name="pathBase">Destination folder</param>
        /// <param name="pathDatabase">LiteDb file location</param>
        public override void Run(string pathBase, string pathDatabase)
        {
            StaticObjects.FireShowMessage("Inicializando dados...");
            //StaticObjects.FireShowMessage("Exportando para busca semântica...");
            //string semanticDataEnglish = Path.Combine(StaticObjects.Parameters.EditBookRepositoryFolder, @"Y:\home\r\tools\semantica\data\data_en.csv");
            //ClearFile(semanticDataEnglish);
            //ExportToCsv(BookEnglish, semanticDataEnglish, "en");

            //string semanticDataPtBr = Path.Combine(StaticObjects.Parameters.EditBookRepositoryFolder, @"Y:\home\r\tools\semantica\data\data_pt.csv");
            //ClearFile(semanticDataPtBr);
            //ExportToCsv(BookPtBr, semanticDataPtBr, "pt");
        }



        protected override void FillPaper(LiteDatabase db, string destyinationFolder, short paperNo)
        {
            throw new NotImplementedException();
        }
    }
}
