using System;
using UbStudyHelpGenerator.HtmlFormatters;
using UbStudyHelpGenerator.UbStandardObjects;
using UbStudyHelpGenerator.UbStandardObjects.Objects;

namespace UbStudyHelpGenerator.PtBr
{
    internal class PtBr_Compare : PtBr_Abstract
    {


        public PtBr_Compare(Parameters param, HtmlFormat_PTalternative formatter)
        {
            Param = param;
            Formatter = formatter;
        }

        public override bool Print(Translation englishTranslation, Translation portuguse2007Translation, Translation ptAlternativeTranslation, short paperNoToPrint= -1)
        {
            try
            {
                if (paperNoToPrint >= 0)
                {
                    PrintPaper(englishTranslation, ptAlternativeTranslation, paperNoToPrint);
                }
                else
                {
                    for (short paperNo = 0; paperNo < 197; paperNo++)
                    {
                        PrintPaper(englishTranslation, ptAlternativeTranslation, paperNo);
                    }
                }
                StaticObjects.FireShowMessage("Finished");
                return true;
            }
            catch (Exception ex)
            {
                StaticObjects.FireShowMessage($"Exporting translation alternative {ex.Message}");
                StaticObjects.Logger.Error("Exporting translation alternative", ex);
                return false;
            }
        }


        //public void Test() => Formatter.Test();

    }
}
