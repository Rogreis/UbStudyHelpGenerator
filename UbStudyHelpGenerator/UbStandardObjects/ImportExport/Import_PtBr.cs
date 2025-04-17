using LiteDB;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UbStandardObjects.Objects;
using UbStudyHelpGenerator.UbStandardObjects.ImportExport.Models;
using UbStudyHelpGenerator.UbStandardObjects.Objects;

namespace UbStudyHelpGenerator.UbStandardObjects.ImportExport
{
    public class Import_PtBr : Import_0base
    {
        public List<ParagraphSpecial> parts = new List<ParagraphSpecial>
        {
            new ParagraphSpecial {Paper = 1, pk_seq = -1, Text = "PARTE I<br /><br />O Universo Central e os Superuniversos<br /><br />Patrocinada por um corpo de personalidades de Uversa, atuando por autorização dos Anciãos dos Dias de Orvonton." },
            new ParagraphSpecial {Paper = 32, pk_seq = -1, Text = "PARTE II<br /><br />O Universo Local<br /><br />Patrocinada por um Corpo de Personalidades do Universo Local de Nebadon, atuando por autorização de Gabriel de Salvington."},
            new ParagraphSpecial {Paper = 57, pk_seq = -1, Text = "PARTE III<br /><br />A História de Urântia<br /><br />Patrocinados por um Corpo de Personalidades do Universo Local, atuando por autorização de Gabriel de Salvington."},
            new ParagraphSpecial {Paper = 120, pk_seq = -1, Text = "PARTE IV<br /><br />A Vida e os Ensinamentos de Jesus<br /><br />Esse grupo de documentos foi auspiciado por uma comissão de doze Seres Intermediários de Urântia atuando sob a supervisão de um diretor Melquisedeque de revelação.<br />A base dessa narrativa foi fornecida por um ser intermediário secundário que tinha sido designado anteriormente para a custódia suprahumana do Apóstolo André."}
        };


        protected void FillPaper(LiteDatabase db, string pathBase, short paperNo)
        {

            PaperExport paperExport = new PaperExport(paperNo);
            BookPtBr.Papers.Add(paperExport);
            PaperEdit paperEdit = new PaperEdit(paperNo, pathBase);
            List<Note> listNotes = Notes.GetNotes(paperNo);
            short pk_seq = 0;


            string folderPath = Path.Combine(pathBase, $"Doc{paperNo:000}");
            var sortedFiles = Directory.GetFiles(folderPath, "Par*.md")
                                       .OrderBy(f => f) // Sort by name (case-sensitive)
                                       .ToArray();
            StaticObjects.FireShowPaperNumber(paperNo);

            foreach (string filePath in sortedFiles)
            {
                ParagraphEdit parPtBr = new ParagraphEdit(filePath);
                Note note = GetNote(listNotes, parPtBr.Paper, parPtBr.Section, parPtBr.ParagraphNo);
                pk_seq++;

                ParagraphExport paragraphPtBr = new ParagraphExport()
                {
                    Paper = paperNo,
                    Pk_seq = pk_seq,
                    Section = parPtBr.Section,
                    Paragraph = parPtBr.ParagraphNo,
                    Text = parPtBr.Text,
                    Status = note.Status,
                    Format = note.Format
                };
                paperExport.Paragraphs.Add(paragraphPtBr);
                IsSeparatorParagraph(paragraphPtBr, paperExport.Paragraphs, ref pk_seq);
            }
            LiteDbStore(db, BookPtBr.TranslationLanguage, paperNo, paperExport.Paragraphs);
        }



        /// <summary>
        /// Coordinate the filling of the PT-BR translation (all papers)
        /// </summary>
        /// <param name="pathBase">Path is the repository root</param>
        protected override void FillBook(LiteDatabase db, string pathBase)
        {
            StaticObjects.FireShowMessage("Getting papers from repository...");
            if (!ResetCollection<ParagraphExport>(db, BookPtBr.TranslationLanguage)) return;
            for (short paperNo = 0; paperNo < 197; paperNo++)
            {
                FillPaper(db, pathBase, paperNo);
            }
        }

        public override void Run(string pathBase, string pathDatabase)
        {
            StaticObjects.FireShowMessage("Importing PtBr...");
            using (var db = new LiteDatabase(pathDatabase))
            {
                FillBook(db, pathBase);
                AddPartIntroduction(db, parts, BookPtBr.TranslationLanguage);
            }
            StaticObjects.FireShowMessage("Finished");
        }

    }
}
