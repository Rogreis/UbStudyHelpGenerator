using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using UbStudyHelpGenerator.UbStandardObjects.ImportExport.Models;
using UbStudyHelpGenerator.UbStandardObjects.Objects;



namespace UbStudyHelpGenerator.UbStandardObjects.ImportExport
{
    public abstract class Import_0base : _0CommonBase
    {

        private List<(int, int, int)> tuplesSeparators = new List<(int, int, int)>
            {
                (31, 10, 21),
                (56, 10, 22),
                (120, 3, 11),
                (134, 6, 14),
                (144, 5, 11),
                (144, 5, 25),
                (144, 5, 38),
                (144, 5, 54),
                (144, 5, 73),
                (144, 5, 87)
            };


        protected bool DeleteTable(LiteDatabase db, string tableName)
        {
            var success = db.DropCollection(tableName);

            if (success)
                StaticObjects.FireShowMessage("Coleção deletada com sucesso.");
            else
                StaticObjects.FireShowMessage("A coleção não existia.");
            return success;
        }



        protected bool ResetCollection<T>(LiteDatabase db, string collectionName) where T : class, new()
        {
            try
            {
                // Delete if exists
                if (db.GetCollectionNames().Contains(collectionName))
                {
                    db.DropCollection(collectionName);
                }

                // Recreate by inserting a dummy and deleting it
                var col = db.GetCollection<T>(collectionName);
                var dummy = new T();
                col.Insert(dummy);       // create collection structure
                col.DeleteAll();         // clean dummy entry
                return true;
            }
            catch (Exception ex)
            {
                StaticObjects.FireShowMessage(ex.Message);
                return false;
            }
        }


        protected void AddPartIntroduction(LiteDatabase db, List<ParagraphSpecial> parts, string collectionNAme)
        {
            var col = db.GetCollection<ParagraphExport>(collectionNAme);
            foreach (var part in parts)
            {
                ParagraphExport paragraphIntro = new ParagraphExport()
                {
                    Paper = part.Paper,
                    Pk_seq = part.pk_seq,
                    Section = 0,
                    Paragraph = 0,
                    Text = part.Text,
                    Status = 4,
                    Format = part.Format
                };
                col.DeleteMany(x => x.Paper == part.Paper &&
                                    x.Pk_seq == part.pk_seq);
                col.Insert(paragraphIntro);
            }
        }


        protected bool LiteDbStore(LiteDatabase db, string translationLanguage, short paper, List<ParagraphExport> paragraphList)
        {
            var col = db.GetCollection<ParagraphExport>(translationLanguage);
            int removidos = col.DeleteMany(x => x.Paper == paper);

            // Primary key
            col.EnsureIndex("pk", x => new { x.Paper, x.Pk_seq }, unique: true);
            col.InsertBulk(paragraphList);
            return true;
        }

        protected bool IsSeparator(short paper, short section, short paragraph)
        {
            (int, int, int) result = tuplesSeparators.Find(t => t.Item1 == paper &&
                                                    t.Item2 == section &&
                                                    t.Item3 == paragraph);
            return result != (0, 0, 0);
        }

        protected bool IsSeparator(ParagraphExport par)
        {
            return IsSeparator(par.Paper, par.Section, par.Paragraph);
        }

        //protected void IsSeparatorParagraph(ParagraphExport par, List<ParagraphExport> paragraphList, ref short pk_seq)
        //{
        //    if (IsSeparator(par)) return;

        //    // Add and special separator paragraph
        //    pk_seq++;
        //    paragraphList.Add(new ParagraphExport()
        //    {
        //        Pk_seq = pk_seq,
        //        Paper = par.Paper,
        //        Section = 0,
        //        Paragraph = 0,
        //        Text = "* * * * *",
        //        Status = 4,
        //        Format = (short)ParagraphHtmlType.Divider
        //    });
        //}

        /// <summary>
        /// Get a paragraph specific note
        /// </summary>
        /// <param name="section"></param>
        /// <param name="paragraphNo"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        protected Note GetNote(List<Note> notes, short paper, short section, short paragraph)
        {
            Note note = notes.Find(n => n.Paper == paper && n.Section == section && n.Paragraph == paragraph);
            if (note == null)
            {
                throw new Exception($"Nota não encontrada para {paper}:{section}-{paragraph}");
            }
            return note;
        }


        /// <summary>
        /// Coordinate the filling of a book (all papers)
        /// </summary>
        /// <param name="pathBase">Path information for the filling actions</param>
        protected abstract void FillBook(LiteDatabase db, string pathBase);

    }
}
