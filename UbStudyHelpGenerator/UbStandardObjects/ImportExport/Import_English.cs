using LiteDB;
using Markdig.Parsers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using UbStudyHelpGenerator.UbStandardObjects.ImportExport.Models;
using UbStudyHelpGenerator.UbStandardObjects.Objects;

namespace UbStudyHelpGenerator.UbStandardObjects.ImportExport
{
    internal class Import_English : Import_0base
    {

        protected List<ParagraphSpecial> parts = new List<ParagraphSpecial>
        {
            new ParagraphSpecial {Paper = 1, pk_seq = -1, Text = "PART I<br /><br />The Central and Superuniverses<br /><br />Sponsored by a Uversa Corps of Superuniverse Personalities acting by authority of the Orvonton Ancients of Days." },
            new ParagraphSpecial {Paper = 32, pk_seq = -1, Text = "PART II<br /><br />The Local Universe<br /><br />Sponsored by a Nebadon Corps of Local Universe Personalities acting by authority of Gabriel of Salvington."},
            new ParagraphSpecial {Paper = 57, pk_seq = -1, Text = "PART III<br /><br />The History of Urantia<br /><br />These papers were sponsored by a Corps of Local Universe Personalities acting by authority of Gabriel of Salvington."},
            new ParagraphSpecial {Paper = 120, pk_seq = -1, Text = "PART IV<br /><br />This group of papers was sponsored by a commission of twelve Urantia midwayers acting under the supervision of a Melchizedek revelatory director.<br />The basis of this narrative was supplied by a secondary midwayer who was onetime assigned to the superhuman watchcare of the Apostle Andrew."}
        };


        private string ExtractAndRemoveStarContent(ref string line, out string starValue)
        {
            starValue = string.Empty;

            if (line.StartsWith("*"))
            {
                // Find the first and second asterisk positions after </a>*
                int firstStar = line.IndexOf('*');
                int secondStar = line.IndexOf('*', firstStar + 1);

                if (firstStar >= 0 && secondStar > firstStar)
                {
                    // Extract the value between the asterisks
                    starValue = line.Substring(firstStar + 1, secondStar - firstStar - 1);

                    // Remove that part (including the asterisks)
                    line = line.Remove(firstStar, secondStar - firstStar + 1);
                }
            }

            return line;
        }


        private void ParseLine(string line, out string nameAttr, out string remainingText, out short page, out short lineNo, ref bool isSeparator)
        {
            nameAttr = remainingText = string.Empty;
            page = lineNo = 0;

            // Matches any of the three formats
            var match = Regex.Match(line, @"^(##\s*)?(###\s*)?<a\s+name=""([^""]+)""></a>(.*)");

            if (match.Success)
            {
                nameAttr = match.Groups[3].Value;
                remainingText = match.Groups[4].Value.Trim();
                var (paper, section, paragraph) = ParseUString(nameAttr);
                if (remainingText.Contains("===separator==="))
                {
                    remainingText= remainingText.Replace("===separator===", "* * * * *");
                    isSeparator = true;
                    return;
                }
                else
                    isSeparator = false;

                ExtractAndRemoveStarContent(ref remainingText, out string starValue);
                if (starValue != string.Empty)
                {
                    var parts = starValue.Split(new[] { '(', ')', ' ', ':', '.' }, StringSplitOptions.RemoveEmptyEntries);
                    page = Convert.ToInt16(parts[3]);
                    lineNo = Convert.ToInt16(parts[4]);
                }
            }
        }

        private (short, short, short) ParseUString(string input)
        {
            // Remove the leading 'U' and split by '_'
            var parts = input.TrimStart('U').Split('_');

            if (parts.Length != 3)
                return (-1, -1, -1);

            return (
                short.Parse(parts[0]),
                short.Parse(parts[1]),
                short.Parse(parts[2])
            );
        }




        /// <summary>
        /// Coordinate the filling of the English book (all papers)
        /// </summary>
        /// <param name="pathBase">Path in the markdown file location with the full Eenglish text imported form UF</param>
        protected override void FillBook(LiteDatabase db, string pathBase)
        {
            var regex = new Regex(@"^<a name=""U(\d{1,3})_(\d{1,3})_(\d{1,3})""></a>(.*)");
            var starRegex = new Regex(@"\*(.*?)\*");
            if (!ResetCollection<ParagraphExport>(db, BookEnglish.TranslationLanguage)) return;

            short currentPaper = -1;
            short pk_seq = 0;
            List<ParagraphExport> paragraphList = new List<ParagraphExport>();
            List<Note> notes = null;
            //int counter = 0;
            bool isSeparator = false;
            foreach (string line in File.ReadAllLines(pathBase, Encoding.UTF8))
            {
                //counter++;
                //if (counter < 6900) continue;
                //if (counter > 7000) break;
                if (string.IsNullOrEmpty(line)) continue;

                ParseLine(line, out var nameAttr, out var rest, out var page, out var lineNo, ref isSeparator);
                var (paper, section, paragraph) = ParseUString(nameAttr);
                if (paper < 0) continue;

                if (!isSeparator) rest = HtmlToUtf8(starRegex.Replace(rest, "", 1).Trim());
                //if (!(section == 0 && paragraph == 0))
                //{
                //    StaticObjects.FireShowMessage($"{paper}:{section}-{paragraph} «{page}-{lineNo}» {rest.Substring(0, Math.Min(100, rest.Length))}");
                //}
                //else 
                //    StaticObjects.FireShowMessage($"{paper}:{section}-{paragraph}  {rest}");

                if (paper != currentPaper)
                {
                    pk_seq = 0;
                    notes = Notes.GetNotes(paper);
                    if (paper != -1)
                    {
                        LiteDbStore(db, BookEnglish.TranslationLanguage, currentPaper, paragraphList);
                        StaticObjects.FireShowPaperNumber(currentPaper);
                        currentPaper = paper;
                        paragraphList = new List<ParagraphExport>();
                    }
                }
                pk_seq++;


                Note note = GetNote(notes, paper, section, paragraph);
                ParagraphExport par = new ParagraphExport()
                {
                    Pk_seq = pk_seq,
                    Paper = paper,
                    Section = section,
                    Paragraph = paragraph,
                    Page = page,
                    Line = lineNo,
                    Text = rest,
                    Status = 4,
                    Format = (isSeparator? (short)ParagraphExportHtmlType.Divider : note.Format) 
                };
                paragraphList.Add(par);


                //    var match = regex.Match(line);
                //    if (match.Success)
                //    {
                //        // Valida se os números estão no intervalo de 0 a 197
                //        if (int.TryParse(match.Groups[1].Value, out int n1) &&
                //            int.TryParse(match.Groups[2].Value, out int n2) &&
                //            int.TryParse(match.Groups[3].Value, out int n3) &&
                //            n1 <= 197 && n2 <= 197 && n3 <= 197)
                //        {
                //            // Remove o primeiro trecho entre * e *
                //            string content = match.Groups[4].Value.Trim();
                //            content = HtmlToUtf8(starRegex.Replace(content, "", 1).Trim());
                //            short paper = Convert.ToInt16(n1);
                //            short section = Convert.ToInt16(n2);
                //            short paragraph = Convert.ToInt16(n3);

                //            if (content.Contains("* * *"))
                //            {
                //                content = content.Replace("**", "*");
                //            }

                //        }
                //    }
            }
            LiteDbStore(db, BookEnglish.TranslationLanguage, currentPaper, paragraphList);
            StaticObjects.FireShowPaperNumber(currentPaper);
            StaticObjects.FireShowMessage("Finished");
        }

        public override void Run(string pathBase, string pathDatabase)
        {
            StaticObjects.FireShowMessage("Importing English...");
            using (var db = new LiteDatabase(pathDatabase))
            {
                FillBook(db, pathBase);
                AddPartIntroduction(db, parts, BookEnglish.TranslationLanguage);
            }
        }

    }
}
