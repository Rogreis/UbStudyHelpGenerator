using DocumentFormat.OpenXml.Office2010.PowerPoint;
using System;
using System.Collections.Generic;
using System.IO;
using UbStandardObjects.Objects;

namespace UbStudyHelpGenerator.UbStandardObjects.Objects
{
    public class PaperEdit : Paper
    {

        private string RepositoryFolder { get; set; } = "";
        private short paperEditNo = -1;
        private List<Note> notes = null;  // Store additional information about the paper, like format and status


        public PaperEdit(short paperNo)
        {
            paperEditNo = paperNo;
        }


        public PaperEdit(short paperNo, string repositoryFolder)
        {
            paperEditNo = paperNo;
            RepositoryFolder = repositoryFolder;
            GetParagraphsFromRepository();
        }

        public bool AddParagraph(string pathParagraphFile)
        {
            ParagraphEdit p = new ParagraphEdit(pathParagraphFile);
            StaticObjects.Book.FormatTableObject.GetParagraphFormatData(p);
            Note note = GetMyNote(p.Section, p.ParagraphNo);
            p._status = note.Status;
            Paragraphs.Add(p);
            return true;
        }

        #region Region Notes
        // Notes are used to store format and status of each paragraph
        
        /// <summary>
        /// Make sure notes were got
        /// </summary>
        private void GetAllNotes()
        {
            if (notes == null)
            {
                notes = Notes.GetNotes(paperEditNo);
            }
        }

        /// <summary>
        /// Get a paragraph specidif nore
        /// </summary>
        /// <param name="section"></param>
        /// <param name="paragraphNo"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private Note GetMyNote(short section, short paragraphNo)
        {
            GetAllNotes();
            Note note = notes.Find(n => n.Paper == paperEditNo && n.Section == section && n.Paragraph == paragraphNo);
            if (note == null)
            {
                throw new Exception($"Nota não encontrada para {paperEditNo}:{section}-{paragraphNo}");
            }
            return note;
        }

        /// <summary>
        /// Store the notes for the current paper
        /// </summary>
        /// <param name="paperNo"></param>
        /// <param name="status"></param>
        public void StoreFullPaperStatus(short paperNo, short status)
        {
            GetAllNotes();
            foreach (Note note in notes) note.Status = status;
            Notes.StorePaperNote(notes, paperNo);
        }


        #endregion


        /// <summary>
        /// Read all paragraph from disk
        /// </summary>
        private void GetParagraphsFromRepository()
        {
            foreach (string pathParagraphFile in Directory.GetFiles(RepositoryFolder, $@"Doc{paperEditNo:000}\Par_{paperEditNo:000}_*.md"))
            {
                ParagraphEdit p = new ParagraphEdit(pathParagraphFile);
                StaticObjects.Book.FormatTableObject.GetParagraphFormatData(p);
                Note note = GetMyNote(p.Section, p.ParagraphNo);
                p._status = note.Status;
                Paragraphs.Add(p);
            }
            // Sort

            Paragraphs.Sort(delegate (Paragraph p1, Paragraph p2)
            {
                if (p1.Section < p2.Section)
                {
                    return -1;
                }
                if (p1.Section > p2.Section)
                {
                    return 1;
                }
                if (p1.ParagraphNo < p2.ParagraphNo)
                {
                    return -1;
                }
                if (p1.ParagraphNo > p2.ParagraphNo)
                {
                    return 1;
                }
                return 0;
            });

        }

        /// <summary>
        /// Return an specific paragraph
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public ParagraphEdit GetParagraphFromRepository(string filePath)
        {
            //if (Paragraphs.Count == 0)
            //{
            //    GetParagraphsFromRepository();
            //}
            // Always get the paragraph from repository
            if (!File.Exists(filePath)) return null;
            ParagraphEdit par = new ParagraphEdit(filePath);
            Note note = GetMyNote(par.Section, par.ParagraphNo);
            par._status = note.Status;
            return par;
        }

        public ParagraphEdit GetParagraphFromRepository(TOC_Entry entry)
        {
            try
            {
                ParagraphEdit par = new ParagraphEdit(entry);
                Note note = GetMyNote(par.Section, par.ParagraphNo);
                par._status = note.Status;
                return par;
            }
            catch (Exception ex)
            {
                throw new Exception("PaperEdit.GetParagraphFromRepository", ex);
            }
        }

        public bool SaveParagraph(string repositoryPath, ParagraphEdit par)
        {
            try
            {
                par.SaveText(repositoryPath);
                Note note = GetMyNote(par.Section, par.ParagraphNo);
                note.Status= (short)par.Status;
                Notes.StorePaperNote(notes, par.Paper);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("PaperEdit.SaveParagraph", ex);
            }
        }

    }
}
