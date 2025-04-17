using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using UbStudyHelpGenerator.UbStandardObjects.ImportExport;
using UbStudyHelpGenerator.UbStandardObjects.ImportExport.Models;

namespace UbStudyHelpGenerator.UbStandardObjects.Exporters
{
    internal class Export_SimpleQuery : Export_0base
    {
        private class Referencia
        {
            public long StartIndex { get; set; }
            public long Size { get; set; }
            public string Reference { get; set; }
        }

        private void ExportLanguage(BookExport book, StringBuilder sb, List<Referencia> references)
        {
            foreach (PaperExport paper in book.Papers)
            {
                foreach (ParagraphExport paragraph in paper.Paragraphs)
                {
                    Referencia reference = new Referencia()
                    {
                        StartIndex = sb.Length,
                        Size = paragraph.Text.Length,
                        Reference = paragraph.Reference
                    };
                    references.Add(reference);
                    sb.Append(paragraph.Text);
                }
            }
        }

        public override void Run(string pathBase, string pathDatabase)
        {
            StaticObjects.FireShowMessage("Exportando para busca simples...");

            string queryDataEnglish = Path.Combine(StaticObjects.Parameters.EditBookRepositoryFolder, @"query\qsd_en.zip");
            string idxDataEnglish = Path.Combine(StaticObjects.Parameters.EditBookRepositoryFolder, @"query\idx_en.zip");
            ClearFile(queryDataEnglish);
            ClearFile(idxDataEnglish);

            string queryDataPtBr = Path.Combine(StaticObjects.Parameters.EditBookRepositoryFolder, @"query\qsd_ptbr.zip");
            string idxDataPtBr = Path.Combine(StaticObjects.Parameters.EditBookRepositoryFolder, @"query\idx_ptbr.zip");
            ClearFile(queryDataPtBr);
            ClearFile(idxDataPtBr);

            StringBuilder sbEnglish = new StringBuilder();
            StringBuilder sbPtBr = new StringBuilder();
            List<Referencia> referenciasEnglish = new List<Referencia>();
            List<Referencia> referenciasPtBr = new List<Referencia>();
            ExportLanguage(BookEnglish, sbEnglish, referenciasEnglish);
            ExportLanguage(BookPtBr, sbPtBr, referenciasPtBr);

            CompressToZip(sbEnglish.ToString(), queryDataEnglish);
            CompressToZip(sbPtBr.ToString(), queryDataPtBr);

            string jsonEnglish = JsonSerializer.Serialize(referenciasEnglish, new JsonSerializerOptions { WriteIndented = true });
            string jsonPtBr = JsonSerializer.Serialize(referenciasPtBr, new JsonSerializerOptions { WriteIndented = true });

            CompressToZip(jsonEnglish, idxDataEnglish);
            CompressToZip(jsonPtBr, idxDataPtBr);
        }

        public Tuple<string, string> Import(string pathText, string pathIndex)
        {
            return new Tuple<string, string>(UnzipFromZip(pathText), UnzipFromZip(pathIndex));
        }


        protected override void FillPaper(LiteDB.LiteDatabase db, string destyinationFolder, short paperNo)
        {
            throw new NotImplementedException();
        }
    }
}

