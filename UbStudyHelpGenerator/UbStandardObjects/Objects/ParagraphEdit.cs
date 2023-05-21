﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using UbStudyHelpGenerator.UbStandardObjects.Objects;

namespace UbStandardObjects.Objects
{
    public class ParagraphEdit : Paragraph
    {

        public string TranslatorNote { get; set; }

        public string Comment { get; set; }

        public DateTime LastDate { get; set; }

        public string MarkdownContent { get; set; } = "";


        public static string RelativeFilePath(Paragraph p)
        {
            return $"Doc{p.Paper:000}/Par_{p.Paper:000}_{p.Section:000}_{p.ParagraphNo:000}.md";
        }

        public static string Url(Paragraph p)
        {
            return $"https://github.com/Rogreis/PtAlternative/blob/correcoes/Doc{p.Paper:000}/Par_{p.Paper:000}_{p.Section:000}_{p.ParagraphNo:000}.md";
        }


        public static string RelativeFilePathWindows(Paragraph p)
        {
            return $@"Doc{p.Paper:000}\Par_{p.Paper:000}_{p.Section:000}_{p.ParagraphNo:000}.md";
        }

        public static string FullPath(string repositoryPath, short paperNo, short sectionNo, short paragraphNo)
        {
            return Path.Combine(repositoryPath, $@"Doc{paperNo:000}\Par_{paperNo:000}_{sectionNo:000}_{paragraphNo:000}.md");
            //return Path.Combine(repositoryPath, $@"Par_{paperNo:000}_{sectionNo:000}_{paragraphNo:000}.md");
        }

        public static string FullPath(string repositoryPath, Paragraph p)
        {
            return Path.Combine(repositoryPath, RelativeFilePathWindows(p));
        }

        public static string FullPath(string repositoryPath, TOC_Entry entry)
        {
            return FullPath(repositoryPath, entry.Paper, entry.Section, entry.ParagraphNo);
        }


        public ParagraphEdit(string filePath)
        {
            MarkdownContent = File.ReadAllText(filePath);
            Text = MarkdownToHtml(MarkdownContent);
            char[] sep = { '_' };
            string fileName = Path.GetFileNameWithoutExtension(filePath).Remove(0, 4);
            string[] parts = fileName.Split(sep, StringSplitOptions.RemoveEmptyEntries);
            Paper = Convert.ToInt16(parts[0]);
            Section = Convert.ToInt16(parts[1]);
            ParagraphNo = Convert.ToInt16(parts[2]);
            IsEditTranslation = true;
        }

        public ParagraphEdit(string repositoryPath, string ident)
        {
            char[] sep = { '_' };
            string[] parts = ident.Remove(0,1).Split(sep, StringSplitOptions.RemoveEmptyEntries);
            Paper = Convert.ToInt16(parts[0]);
            Section = Convert.ToInt16(parts[1]);
            ParagraphNo = Convert.ToInt16(parts[2]);
            IsEditTranslation = true;
            string filePath = FullPath(repositoryPath, this);
            Text = MarkdownToHtml(File.ReadAllText(filePath));
        }


        public ParagraphEdit(string repositoryPath, short paperNo, short sectionNo, short paragraphNo)
        {
            Paper = paperNo;
            Section = sectionNo;
            ParagraphNo = paragraphNo;
            string filePath = FullPath(repositoryPath, this);
            if (!File.Exists(filePath))
            {
                throw new Exception($"Paragraph does not exist: {paperNo} {sectionNo} {paragraphNo}");
            }
            IsEditTranslation = true;
            Text = MarkdownToHtml(File.ReadAllText(filePath));
        }


        /// <summary>
        /// Convert paragraph markdown to HTML
        /// The markdown used for TUB paragraphs has just italics
        /// </summary>
        /// <param name="markDownText"></param>
        /// <returns></returns>
        private string MarkdownToHtml(string markDownText)
        {
            int position = 0;
            bool openItalics = true;

            string newText = markDownText;
            var regex = new Regex(Regex.Escape("*"));
            while (position >= 0)
            {
                position = newText.IndexOf('*');
                if (position >= 0)
                {
                    newText = regex.Replace(newText, openItalics ? "<i>" : "</i>", 1);
                    openItalics = !openItalics;
                }
            }
            return newText;
        }

        public static string FolderPath(short paperNo)
        {
            return $"Doc{paperNo:000}";
        }


        public static string FilePath(short paperNo, short section, short paragraphNo)
        {
            return $"{FolderPath(paperNo)}\\Par_{paperNo:000}_{section:000}_{paragraphNo:000}.md";
        }

        public static string FilePath(string ident)
        {
            // 120:0-1 (0.0)
            char[] separators = { ':', '-', ' ' };
            string[] parts = ident.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            short paperNo = Convert.ToInt16(parts[0]);
            short section = Convert.ToInt16(parts[1]);
            short paragraphNo = Convert.ToInt16(parts[2]);
            return $"Doc{paperNo:000}\\Par_{paperNo:000}_{section:000}_{paragraphNo:000}.md";
        }

        public void SetNote(Note note)
        {

            if (note != null)
            {
                TranslatorNote = note.TranslatorNote;
                Comment = note.Notes;
                LastDate = note.LastDate;
                _status = note.Status;
            }
            else
            {
                TranslatorNote = "";
                Comment = "";
                LastDate = DateTime.MinValue;
                _status = 0;
            }
        }


        public bool SaveText(string repositoryPath)
        {
            try
            {
                File.WriteAllText(FullPath(repositoryPath, this), Text);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool SaveNotes(string repositoryPath)
        {
            try
            {
                Notes.SaveNotes(this);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
