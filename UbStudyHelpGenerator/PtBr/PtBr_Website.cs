using System;
using System.Globalization;
using System.IO;
using System.Text;
using UbStudyHelpGenerator.HtmlFormatters;
using UbStudyHelpGenerator.PtBr;
using UbStudyHelpGenerator.UbStandardObjects;
using UbStudyHelpGenerator.UbStandardObjects.Objects;

namespace UbStudyHelpGenerator.PtBr
{
    internal class PtBr_Website : PtBr_Abstract
    {

        public PtBr_Website(Parameters param, HtmlFormat_Palternative formatter)
        {
            Param = param;
            Formatter = formatter;
        }

        protected void PrintCompare(Translation englishTranslation, Translation translatioPt2007, Translation translatioPtAlternative, string destinationFolder, short paperNoToPrint)
        {
            FireShowMessage($"Comparing paper {paperNoToPrint}");
            FireShowPaperNumber(paperNoToPrint);
            Paper paperEnglish = englishTranslation.Paper(paperNoToPrint);
            Paper paperPt2007 = translatioPt2007.Paper(paperNoToPrint);
            PaperEdit paperEdit = new PaperEdit(paperNoToPrint, Param.EditParagraphsRepositoryFolder);
            Formatter.GenerateCompare(paperEnglish, paperPt2007.Paragraphs, paperEdit.Paragraphs, destinationFolder, paperNoToPrint);
        }


        public override bool Print(TUB_TOC_Html toc_table, Translation englishTranslation, Translation portuguse2007Translation, Translation ptAlternativeTranslation, short paperNoToPrint= -1)
        {
            try
            {
                if (paperNoToPrint >= 0)
                {
                    PrintPaper(toc_table, englishTranslation, ptAlternativeTranslation, paperNoToPrint);
                }
                else
                {
                    for (short paperNo = 0; paperNo < 197; paperNo++)
                    {
                        PrintPaper(toc_table, englishTranslation, ptAlternativeTranslation, paperNo);
                    }
                }
                FireShowMessage("Finished");
                return true;
            }
            catch (Exception ex)
            {
                FireShowMessage($"Exporting translation alternative {ex.Message}");
                StaticObjects.Logger.Error("Exporting translation alternative", ex);
                return false;
            }
        }

        public bool Compare(Translation englishTranslation, Translation portuguse2007Translation, Translation ptAlternativeTranslation, string destinationFolder, short paperNoToPrint = -1)
        {
            try
            {
                if (paperNoToPrint >= 0)
                {
                    PrintCompare(englishTranslation, portuguse2007Translation, ptAlternativeTranslation, destinationFolder, paperNoToPrint);
                }
                else
                {
                    for (short paperNo = 0; paperNo < 197; paperNo++)
                    {
                        PrintCompare(englishTranslation, portuguse2007Translation, ptAlternativeTranslation, destinationFolder, paperNo);
                    }
                }
                FireShowMessage("Finished");
                return true;
            }
            catch (Exception ex)
            {
                FireShowMessage($"Exporting translation alternative {ex.Message}");
                StaticObjects.Logger.Error("Exporting translation alternative", ex);
                return false;
            }
        }

        //public void Test() => Formatter.Test();

    }
}
