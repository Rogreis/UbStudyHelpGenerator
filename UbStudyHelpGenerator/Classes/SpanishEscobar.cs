using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using UbStandardObjects;
using UbStandardObjects.Objects;
using static System.Net.Mime.MediaTypeNames;


namespace UbStudyHelpGenerator.Classes
{
    internal class SpanishEscobar
    {
        private FormatTable Format = StaticObjects.Book.GetFormatTable();

        public Translation Import(string pathToWordFile)
        {
            // Open an existing document
            StaticObjects.FireSendMessage(null);
            //string regExPattern = "(\"\\d{1,3}):(\\d{1,3}.)(\\d{1,3} )(\\(\\d{1,4}.)(\\d{1,3}\\))\"gm";
            // "(\d{1,3}):(\d{1,3}.)(\d{1,3} )(\(\d{1,4}.)(\d{1,3}\))"gm
            // /(\d{1,3}):(\d{1,3}.)(\d{1,3} )(\(\d{1,4}.)(\d{1,3}\))/gm
            StaticObjects.FireSendMessage("Importing Escobar word file");
            int nrPar = 0, contType1= 0, contType2= 0;
            char[] sep1= {')'}, sep2={' '}, sepIdent= { '.', ':', ' ' };
            Translation translation= new Translation();
            translation.LanguageID = 144;
            translation.Description = "Spanish Escobar";
            translation.Version = 1;
            translation.TIN = "";
            translation.TUB = "Los Escritos de Urantia";
            translation.TextButton = "es  ";
            translation.CultureID = 3082;
            translation.UseBold = false;
            translation.RightToLeft = false;
            translation.StartingYear = 2005;
            translation.EndingYear = 2022;
            translation.PaperTranslation = "Documento";
            for(short i= 0; i<197; i++)
            {
                translation.Papers.Add(new Paper());
            }

 
            foreach (string s in File.ReadAllLines(pathToWordFile))
            {
                nrPar++;
                int lineType = 0;
                //if (nrPar > 200) return translation;
                //Regex reg = new Regex("/(\\d{1,3}):(\\d{1,3}.)(\\d{1,3} )(\\(\\d{1,4}.)(\\d{1,3}\\))/gm");
                string patternWithPage = @"(\d{1,3}):(\d{1,3}.)(\d{1,3} )(\(\d{1,4}.)(\d{1,3}\))";
                string patternWithoutPage = @"(\d{1,3}):(\d{1,3}.)(\d{1,3})";
                //string input = @"13:0.4 (143.4) Parece que las energías impersonales de luminosidad espiritual se originan en los siete mundos sagrados del Hijo Eterno. Ningún ser personal puede habitar en ninguno de estos siete ámbitos de resplandor. Con su gloria espiritual iluminan todo el Paraíso y Havona, y dirigen hacia los siete suprauniversos una luminosidad espiritual pura. Asimismo, estas esferas brillantes de la segunda vía emiten su luz (luz sin calor) al Paraíso y a las siete vías circulatorias formadas por los mil millones de mundos del universo central.";
                RegexOptions options = RegexOptions.Multiline;
                var matchesFound = Regex.Matches(s, patternWithPage, options);
                if (matchesFound.Count > 0)
                {
                    lineType = 1;
                    contType1++;
                }
                else
                {
                    matchesFound = Regex.Matches(s, patternWithoutPage, options);
                    lineType = 2;
                    contType2++;
                }
                string[] lines = lineType == 1 ? s.Split(sep1, StringSplitOptions.RemoveEmptyEntries) : s.Split(sep2,StringSplitOptions.RemoveEmptyEntries);
                string ident = lines[0];
                string text= lines[1].Replace(")", "").Trim();
                string[] partsIdent= ident.Split(sepIdent, StringSplitOptions.RemoveEmptyEntries);

                UbStandardObjects.Objects.Paragraph par= new UbStandardObjects.Objects.Paragraph();
                par.Paper = short.Parse(partsIdent[0]);
                par.Section = short.Parse(partsIdent[1]);
                par.ParagraphNo = short.Parse(partsIdent[2]);
                par.Text = text;

                par.TranslationId = 144;
                Format.GetParagraphFormatData(par);

                translation.Papers[par.Paper].Paragraphs.Add(par);

                //int maxSize = Math.Min(text.Length, 50);
                //StaticObjects.FireSendMessage($"{text.Substring(0, maxSize)}");
            }
            StaticObjects.FireSendMessage($"nPar= {nrPar} Type 0= {nrPar- contType1- contType2}  Type 1= {contType1}  Type 3= {contType2}  ");
            return translation;
        }
    }
}
