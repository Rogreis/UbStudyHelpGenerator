using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using UbStandardObjects.Objects;

namespace UbStudyHelpGenerator.UbStandardObjects.Objects
{
    // ----------------------------------
    // Classes used to get data from file


    public class Note
    {
        public int Paper { get; set; }
        public int Section { get; set; }
        public int Paragraph { get; set; }
        public short Status { get; set; } = 0;
        public short Format { get; set; } = 0;
    }


    public static class Notes
    {
        private class NotesRoot
        {
            public Note[] Notes { get; set; }
        }

        private static JsonSerializerOptions Options = new JsonSerializerOptions
        {
            AllowTrailingCommas = true,
            WriteIndented = true,
        };


        private static string RepositoryNotesPath(short paperNo)
        {
            string pathSubFolder = $@"{ParagraphEdit.FolderPath(paperNo)}";
            string notesFolder = Path.Combine(StaticObjects.Parameters.EditParagraphsRepositoryFolder, pathSubFolder);
            return Path.Combine(notesFolder, "Notes.json");
        }


        public static List<Note> GetNotes(short paperNo)
        {
            string notesPath = RepositoryNotesPath(paperNo);
            string jsonString = File.ReadAllText(notesPath);
            NotesRoot root = JsonSerializer.Deserialize<NotesRoot>(jsonString, Options);
            if (root == null)
            {
                throw new Exception($"Could not get notes for paper {paperNo}");
            }
            return new List<Note>(root.Notes);
        }

        public static void StoreParagraphNote(ParagraphEdit p)
        {
            try
            {
                List<Note> list = GetNotes(p.Paper);
                Note note = list.Find(n => n.Paper == p.Paper && n.Section == p.Section && n.Paragraph == p.ParagraphNo);
                if (note == null)
                {
                    throw new Exception($"Could not get note for Paragraph {p}");
                }

                note.Status = (short)p._status;
                note.Format = (short)p.FormatInt;

                StorePaperNote(list, p.Paper);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void StorePaperNote(List<Note> list, short paperNo)
        {
            try
            {
                NotesRoot root = new NotesRoot();
                root.Notes = list.ToArray();
                string jsonString = JsonSerializer.Serialize<NotesRoot>(root, Options);
                File.WriteAllText(RepositoryNotesPath(paperNo), jsonString);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void CountStatus(ref int[] countStatus)
        {
            countStatus= new int[] { 0, 0, 0, 0, 0 };
            for (short paperNo = 0; paperNo < 197; paperNo++)
            {
                List<Note> nites = GetNotes(paperNo);
                foreach (Note n in nites)
                {
                    countStatus[n.Status]++;
                }
            }

        }


    }
}
