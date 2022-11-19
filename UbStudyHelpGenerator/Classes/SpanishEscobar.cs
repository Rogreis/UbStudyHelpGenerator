using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UbStandardObjects;
using Xceed.Document.NET;
using Xceed.Words.NET;
using Paragraph = Xceed.Document.NET.Paragraph;

namespace UbStudyHelpGenerator.Classes
{
    internal class SpanishEscobar
    {
        public bool Import(string pathToWordFile)
        {
            // Open an existing document
            StaticObjects.FireSendMessage(null);
            string regExPattern = "(\"\\d{1,3}):(\\d{1,3}.)(\\d{1,3} )(\\(\\d{1,4}.)(\\d{1,3}\\))\"gm";
            // "(\d{1,3}):(\d{1,3}.)(\d{1,3} )(\(\d{1,4}.)(\d{1,3}\))"gm
            // /(\d{1,3}):(\d{1,3}.)(\d{1,3} )(\(\d{1,4}.)(\d{1,3}\))/gm
            StaticObjects.FireSendMessage("Importing Escobar word file");
            int nrPar = 0;
            int matches = 0;
            using (var document = DocX.Load(pathToWordFile))
            {
                StaticObjects.FireSendMessage($"{document.Paragraphs.Count} Paragraphs");

                foreach (Paragraph paragraph in document.Paragraphs)
                {


                    Regex reg = new Regex("/(\\d{1,3}):(\\d{1,3}.)(\\d{1,3} )(\\(\\d{1,4}.)(\\d{1,3}\\))/gm");
                    nrPar++;
                    //if (nrPar > 100)
                    //    return true;

                    List<FormattedText> magicText = paragraph.MagicText;
                    string text = "";
                    foreach(FormattedText f in magicText)
                    {
                        if (f.formatting != null && f.formatting.Italic != null && f.formatting.Italic.Value)
                        {
                            text += $"<i>{f.text}</i>";
                        }
                        else // if (f.formatting. != null && f.formatting.Italic.Value)
                        {
                            text += f.text;
                        }
                    }


                    string pattern = @"(\d{1,3}):(\d{1,3}.)(\d{1,3} )(\(\d{1,4}.)(\d{1,3}\))";
                    //string input = @"13:0.4 (143.4) Parece que las energías impersonales de luminosidad espiritual se originan en los siete mundos sagrados del Hijo Eterno. Ningún ser personal puede habitar en ninguno de estos siete ámbitos de resplandor. Con su gloria espiritual iluminan todo el Paraíso y Havona, y dirigen hacia los siete suprauniversos una luminosidad espiritual pura. Asimismo, estas esferas brillantes de la segunda vía emiten su luz (luz sin calor) al Paraíso y a las siete vías circulatorias formadas por los mil millones de mundos del universo central.";
                    RegexOptions options = RegexOptions.Multiline;
                    var matchesFound = Regex.Matches(text, pattern, options);
                    if (matchesFound.Count == 0 && text.Length > 60)
                    {
                        StaticObjects.FireSendMessage($"Not matching: {text}.");
                    }




                    //var result = string.Empty;
                    //var rx = new Regex(regExPattern);
                    //var matchesFound = rx.Matches(text);
                    //if (matchesFound.Count > 0)
                    //{
                    //    result = matchesFound[0].Groups[1].Value;
                    //}
                    //matches+= matchesFound.Count;

                    //    Match m = Regex.Match(text, regExPattern, RegexOptions.None);
                    //    if (!m.Success)
                    //    {
                    //        StaticObjects.FireSendMessage($"{text}");
                    //        StaticObjects.FireSendMessage($"    value: {m.Value}  index: {m.Index}");
                    //    }
                    //    else
                    //        matches++;
                }
                StaticObjects.FireSendMessage($"");
                StaticObjects.FireSendMessage($"Npar {nrPar}  Matches= {matches}   Errors= {(nrPar - matches)}");
            }
            return true;
        }
    }
}
