﻿using System;
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
        public string TranslatorNote { get; set; }
        public string Notes { get; set; }
        public DateTime LastDate { get; set; }
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
            return Path.Combine(StaticObjects.Parameters.EditParagraphsRepositoryFolder, $@"{ParagraphEdit.FolderPath(paperNo)}\Notes.json");
        }


        private static List<Note> GetNotes(short paperNo)
        {
            string jsonString = File.ReadAllText(RepositoryNotesPath(paperNo));
            NotesRoot root = JsonSerializer.Deserialize<NotesRoot>(jsonString, Options);
            if (root == null)
            {
                throw new Exception($"Could not get notes for paper {paperNo}");
            }
            return new List<Note>(root.Notes);
        }

        public static Note GetNote(Paragraph p)
        {
            List<Note> list = GetNotes(p.Paper);
            Note note = list.Find(n => n.Paper == p.Paper && n.Section == p.Section && n.Paragraph == p.ParagraphNo);
            if (note == null)
            {
                note= new Note();
                note.Paper = p.Paper;
                note.Section = p.Section;
                note.Paragraph = p.ParagraphNo;

                note.TranslatorNote = "";
                note.Notes = "";
                note.LastDate= DateTime.Now;
            }
            return note;
        }

        public static void SaveNotes(ParagraphEdit p)
        {
            try
            {
                List<Note> list = GetNotes(p.Paper);
                Note note = list.Find(n => n.Paper == p.Paper && n.Section == p.Section && n.Paragraph == p.ParagraphNo);
                if (note == null)
                {
                    throw new Exception($"Could not get note for Paragraph {p}");
                }

                note.TranslatorNote= p.TranslatorNote;
                note.Status = (short)p._status;
                note.Notes= p.Comment;
                note.LastDate= DateTime.Now.ToUniversalTime();
                note.Format = (short)p.FormatInt;

                NotesRoot root = new NotesRoot();
                root.Notes = list.ToArray();
                string jsonString = JsonSerializer.Serialize<NotesRoot>(root, Options);
                File.WriteAllText(RepositoryNotesPath(p.Paper), jsonString);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
