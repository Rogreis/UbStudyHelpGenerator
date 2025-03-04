using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using UbStandardObjects.Objects;
using File = System.IO.File;

namespace UbStudyHelpGenerator.UbStandardObjects.Objects
{
    public class TranslationEdit : Translation
    {

        private string LocalRepositoryFolder = null;
        private string TocTableFileName = "TOC_Table.json";

        public TranslationEdit(Translation trans, string localRepositoryFolder)
        {
            LocalRepositoryFolder = localRepositoryFolder;
            this.LanguageID = trans.LanguageID;
            this.Description = trans.Description;
            this.TIN = trans.TIN;
            this.TUB = trans.TUB;
            this.Version = trans.Version;
            this.TextButton = trans.TextButton;
            this.CultureID = trans.CultureID;
            this.UseBold = trans.UseBold;
            this.RightToLeft = trans.RightToLeft;
            this.StartingYear = trans.StartingYear;
            this.EndingYear = trans.EndingYear;
            this.PaperTranslation = trans.PaperTranslation;
        }

        public PaperEdit GetPaperEdit(short paperNo)
        {
            PaperEdit paper = new PaperEdit(paperNo)
            {
                Paragraphs = new List<Paragraph>()
            };
            Translation EnglishTranslation = StaticObjects.Book.GetTranslation(0);
            string folderPaper = Path.Combine(LocalRepositoryFolder, $"Doc{paperNo:000}");
            foreach (string filePath in Directory.GetFiles(folderPaper, "*.md"))
            {

                ParagraphEdit paragraph = new ParagraphEdit(filePath);
                paragraph.FormatInt = EnglishTranslation.GetFormat(paragraph);
                paper.Paragraphs.Add(paragraph);
            }
            return paper;
        }


        public new PaperEdit Paper(short paperNo)
        {
            PaperEdit paper = paperNo < this.Papers.Count? (PaperEdit)this.Papers[paperNo] : null;
            if (paper == null)
            {
                paper = GetPaperEdit(paperNo);
                this.Papers.Add(paper);
            }
            return paper;
        }

        public ParagraphEdit GetParagraph(TOC_Entry entry)
        {
            PaperEdit paperEdit = GetPaperEdit(entry.Paper);
            string paragraphPath = ParagraphEdit.FullPath(LocalRepositoryFolder, entry.Paper, entry.Section, entry.ParagraphNo);
            ParagraphEdit paragraphEdit = paperEdit.GetParagraphFromRepository(paragraphPath);
            return paragraphEdit;
        }


        #region Index
        private TUB_TOC_Entry JsonIndexEntry(string fileNamePath, bool isPaperTitle, short paperNoParam = -1)
        {
            char[] separators = { '_' };
            string fileName = Path.GetFileNameWithoutExtension(fileNamePath);

            string[] parts = fileName.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            short paperNo = Convert.ToInt16(parts[1]);
            short sectionNo = Convert.ToInt16(parts[2]);
            short paragraphNo = Convert.ToInt16(parts[3]);
            if (!isPaperTitle && sectionNo == 0 && paragraphNo == 0)
            {
                return null;
            }
            string text = File.ReadAllText(fileNamePath, Encoding.UTF8);
            return new TUB_TOC_Entry()
            {
                Text = paperNoParam > 0 ? paperNoParam.ToString() + " - " + text : text,
                PaperNo = paperNo,
                SectionNo = sectionNo,
                ParagraphNo = paragraphNo,
                Expanded = sectionNo == 0
            };
        }

        private TUB_TOC_Entry JsonIndexEntry(string text)
        {
            return new TUB_TOC_Entry()
            {
                Text = text,
                Expanded = true
            };
        }

        /// <summary>
        /// Create an object with all the index entry for the editing translation
        /// </summary>
        /// <returns></returns>
        public List<TUB_TOC_Entry> GetTranslation_TOC_Table(bool forceGeneration = false)
        {
            string indexJsonFilePath = Path.Combine(StaticObjects.Parameters.EditParagraphsRepositoryFolder, TocTableFileName);
            var options = new JsonSerializerOptions
            {
                AllowTrailingCommas = true,
                WriteIndented = true,
                IncludeFields = true
            };

            if (File.Exists(indexJsonFilePath) && !forceGeneration)
            {
                string json = File.ReadAllText(indexJsonFilePath);
                return JsonSerializer.Deserialize<List<TUB_TOC_Entry>>(json, options);
            }

            List<TUB_TOC_Entry> list = new List<TUB_TOC_Entry>();
            string pathIntroduction = Path.Combine(LocalRepositoryFolder, "Doc000\\Par_000_000_000.md");
            TUB_TOC_Entry intro = JsonIndexEntry(pathIntroduction, true, 0);
            TUB_TOC_Entry part1 = JsonIndexEntry("Parte I");
            TUB_TOC_Entry part2 = JsonIndexEntry("Parte II");
            TUB_TOC_Entry part3 = JsonIndexEntry("Parte III");
            TUB_TOC_Entry part4 = JsonIndexEntry("Parte IV");

            list.Add(intro);
            list.Add(part1);
            list.Add(part2);
            list.Add(part3);
            list.Add(part4);

            for (short paperNo = 0; paperNo < 197; paperNo++)
            {
                string folderPath = Path.Combine(LocalRepositoryFolder, $"Doc{paperNo:000}");
                TUB_TOC_Entry jsonIndexPaper = null;
                string pathTitle = Path.Combine(folderPath, $"Par_{paperNo:000}_000_000.md");
                if (paperNo == 0)
                {
                    jsonIndexPaper = intro;
                }
                else
                {
                    jsonIndexPaper = JsonIndexEntry(pathTitle, true, paperNo);
                }
                if (paperNo == 0)
                {
                    // do nothing
                }
                else if (paperNo < 32)
                {
                    part1.Nodes.Add(jsonIndexPaper);
                }
                else if (paperNo < 57)
                {
                    part2.Nodes.Add(jsonIndexPaper);
                }
                else if (paperNo < 120)
                {
                    part3.Nodes.Add(jsonIndexPaper);
                }
                else
                {
                    part4.Nodes.Add(jsonIndexPaper);
                }

                foreach (string mdFile in Directory.GetFiles(folderPath, $"Par_{paperNo:000}_???_000*.md"))
                {
                    TUB_TOC_Entry paperSection = JsonIndexEntry(mdFile, false);
                    if (paperSection != null)
                        jsonIndexPaper.Nodes.Add(paperSection);
                };
            }

            // Serialize the index
            string jsonString = JsonSerializer.Serialize<List<TUB_TOC_Entry>>(list, options);
            File.WriteAllText(indexJsonFilePath, jsonString);
            //File.WriteAllText(Path.Combine(StaticObjects.Parameters.TUB_Files_RepositoryFolder, TocTableFileName), jsonString);
            //File.WriteAllText(Path.Combine(StaticObjects.Parameters.EditBookRepositoryFolder, TocTableFileName), jsonString);

            return list;
        }

        #endregion


    }
}
