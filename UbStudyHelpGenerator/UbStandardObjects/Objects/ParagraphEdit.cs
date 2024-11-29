using System;
using System.IO;
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


        /// <summary>
        /// Convert paragraph markdown to HTML
        /// The markdown used for TUB paragraphs has just italics
        /// </summary>
        /// <param name="markDownText"></param>
        /// <returns></returns>
        public static string MarkdownToHtml(string markDownText)
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
                    newText = regex.Replace(newText, openItalics ? "<em>" : "</em>", 1);
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
                Notes.StoreParagraphNote(this);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
