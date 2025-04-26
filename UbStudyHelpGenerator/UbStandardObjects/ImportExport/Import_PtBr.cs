using LiteDB;
using Microsoft.Office.Interop.Word;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UbStandardObjects.Objects;
using UbStudyHelpGenerator.UbStandardObjects.ImportExport.Models;
using UbStudyHelpGenerator.UbStandardObjects.Objects;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace UbStudyHelpGenerator.UbStandardObjects.ImportExport
{
    public class Import_PtBr : Import_0base
    {
        public List<ParagraphSpecial> PartsIntroductionForPtBr = new List<ParagraphSpecial>
        {
            new ParagraphSpecial {Paper = 1, pk_seq = -3, Text = "PARTE I", Format= (short)ParagraphExportHtmlType.BookTitle },
            new ParagraphSpecial {Paper = 1, pk_seq = -2, Text = "O Universo Central e os Superuniversos", Format= (short)ParagraphExportHtmlType.BookTitle },
            new ParagraphSpecial {Paper = 1, pk_seq = -1, Text = "<p>Patrocinada por um corpo de personalidades de Uversa, atuando por autorização dos Anciãos dos Dias de Orvonton.</p>" },

            new ParagraphSpecial {Paper = 32, pk_seq = -3, Text = "PARTE II", Format= (short)ParagraphExportHtmlType.BookTitle },
            new ParagraphSpecial {Paper = 32, pk_seq = -2, Text = "O Universo Local", Format= (short)ParagraphExportHtmlType.BookTitle },
            new ParagraphSpecial {Paper = 32, pk_seq = -1, Text = "<p>Patrocinada por um Corpo de Personalidades do Universo Local de Nebadon, atuando por autorização de Gabriel de Salvington.</p>"},

            new ParagraphSpecial {Paper = 57, pk_seq = -3, Text = "PARTE III", Format= (short)ParagraphExportHtmlType.BookTitle },
            new ParagraphSpecial {Paper = 57, pk_seq = -2, Text = "A História de Urântia", Format =(short) ParagraphExportHtmlType.BookTitle},
            new ParagraphSpecial {Paper = 57, pk_seq = -1, Text = "<p>Estes documentos foram patrocinados por um Corpo de Personalidades do Universo Local, agindo por autoridade de Gabriel de Sálvington.</p>"},

            new ParagraphSpecial {Paper = 120, pk_seq = -3, Text = "PARTE IV", Format= (short)ParagraphExportHtmlType.BookTitle },
            new ParagraphSpecial {Paper = 120, pk_seq = -2, Text = "A Vida e os Ensinamentos de Jesus", Format =(short) ParagraphExportHtmlType.BookTitle},
            new ParagraphSpecial {Paper = 120, pk_seq = -1, Text = "<p>Esse grupo de documentos foi patrocinado por uma comissão de doze seres intermediários de Urântia, atuando sob a supervisão de um diretor revelador Melquisedeque.</p>" + 
                                                                   "<p>A base desta narrativa foi fornecida por um ser intermediário secundário que, outrora, foi designado à vigilância super-humana do Apóstolo André.</p>"}
        };

        private int CountErrors = 0;
        private List<string> FilesNotUsed = new List<string>();

        /// <summary>
        /// Store a PTBr paragraph where only text and status are different from English text
        /// </summary>
        /// <param name="parEnglish"></param>
        /// <param name="listParagraphs"></param>
        /// <param name="text"></param>
        /// <param name="status"></param>
        private void AddParagraph(ParagraphExport parEnglish, List<ParagraphExport> listParagraphs, string text, short status)
        {
            try
            {
                ParagraphExport parCreated = new ParagraphExport()
                {
                    Paper = parEnglish.Paper,
                    Pk_seq = parEnglish.Pk_seq,
                    Section = parEnglish.Section,
                    Paragraph = parEnglish.Paragraph,
                    Page = parEnglish.Page,
                    Line = parEnglish.Line,
                    Text = text,
                    Status = status,
                    Format = parEnglish.Format
                };
                listParagraphs.Add(parCreated);
            }
            catch (System.Exception ex)
            {
                CountErrors++;
                StaticObjects.FireShowMessage($"Error {CountErrors:0000}: could not store  - {parEnglish.RepositoryFileName}, erro: {ex.Message}");
            }
        }

        /// <summary>
        /// Import PT-BR from repository.
        /// Uses English text as reference
        /// Run some consistences
        /// </summary>
        /// <param name="db"></param>
        /// <param name="pathBase"></param>
        /// <param name="paperNo"></param>
        protected void FillPaper(LiteDatabase db, string pathBase, short paperNo)
        {

            List<ParagraphExport> englishParagraphs = GetPaper(db, BookEnglish.TranslationLanguage, paperNo);
            List<ParagraphExport> ptBrParagraphs = new List<ParagraphExport>();

            PaperEdit paperEdit = new PaperEdit(paperNo, pathBase);
            List<Note> listNotes = Notes.GetNotes(paperNo);

            string folderPath = Path.Combine(pathBase, $"Doc{paperNo:000}");
            List<string> sortedFiles = Directory.GetFiles(folderPath, "Par*.md")
                                       .OrderBy(f => f) // Sort by name (case-sensitive)
                                       .ToList();

            StaticObjects.FireShowPaperNumber(paperNo);

            foreach (ParagraphExport parEnglish in englishParagraphs)
            {

                // Ignore the parts introduction (for now they are not subjected to revision)
                if (parEnglish.Pk_seq < 0) continue;

                string filePath = sortedFiles.Find(f => f.EndsWith(parEnglish.RepositoryFileName));

                if (filePath == null && !parEnglish.IsSeparator)
                {
                    CountErrors++;
                    StaticObjects.FireShowMessage($"Error {CountErrors:0000}: english paragraph missing in repository - {parEnglish.RepositoryFileName}");
                    continue;
                }


                // Check for file existance, but not for separators
                // Separators are not subjected to translation review
                if (!File.Exists(filePath) && !parEnglish.IsSeparator)
                {
                    CountErrors++;
                    StaticObjects.FireShowMessage($"Error {CountErrors:0000}: non exsiting repository file - {filePath}");
                    continue;
                }

                if (parEnglish.IsSeparator)
                {
                    // Insert a separator paragraph avoinding any revision
                    AddParagraph(parEnglish, ptBrParagraphs, parEnglish.Text, (short)ParagraphExportStatus.Closed);
                    if (filePath == null && File.Exists(filePath))
                    {
                        CountErrors++;
                        StaticObjects.FireShowMessage($"Error {CountErrors:0000}: separator file can be deleted - {filePath}");
                    }
                    continue;
                }

                ParagraphEdit parPtBr = new ParagraphEdit(filePath);
                if (parPtBr == null)
                {
                    CountErrors++;
                    StaticObjects.FireShowMessage($"Error {CountErrors:0000}: getting parPtBr - {filePath}");
                }
                Note note = GetNote(listNotes, parPtBr.Paper, parPtBr.Section, parPtBr.ParagraphNo);
                if (note == null)
                {
                    CountErrors++;
                    StaticObjects.FireShowMessage($"Error {CountErrors:0000}: getting note for - {filePath}");
                }

                AddParagraph(parEnglish, ptBrParagraphs, parPtBr.Text, note.Status);
                sortedFiles.Remove(filePath);
            }
            LiteDbStore(db, BookPtBr.TranslationLanguage, paperNo, ptBrParagraphs);
            if (sortedFiles.Count != 0) FilesNotUsed.AddRange(sortedFiles);
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
                if (CountErrors > 100) 
                {
                    StaticObjects.FireShowMessage($"=== Too many errors, stoppping: {CountErrors}");
                    return;
                }
            }
        }

        public override void Run(string pathBase, string pathDatabase)
        {

            StaticObjects.FireShowMessage("Importing PtBr...");
            CountErrors = 0;
            FilesNotUsed = new List<string>();
            using (var db = new LiteDatabase(pathDatabase))
            {
                FillBook(db, pathBase);
                AddPartIntroduction(db, PartsIntroductionForPtBr, BookPtBr.TranslationLanguage);
            }
            if (FilesNotUsed.Count != 0)
            {
                StaticObjects.FireShowMessage("Files in repository not used:");
                foreach (var file in FilesNotUsed)
                {
                    StaticObjects.FireShowMessage($"   {file}");
                }
            }
            StaticObjects.FireShowMessage($"Finished with {CountErrors} errors.");
        }

    }
}
