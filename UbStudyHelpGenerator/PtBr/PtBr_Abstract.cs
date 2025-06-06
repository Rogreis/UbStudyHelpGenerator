﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UbStudyHelpGenerator.UbStandardObjects.Objects;
using UbStudyHelpGenerator.UbStandardObjects;
using System.Globalization;
using System.IO;
using UbStudyHelpGenerator.HtmlFormatters;

namespace UbStudyHelpGenerator.PtBr
{
    internal abstract class PtBr_Abstract
    {
        protected Parameters Param = null;

        protected HtmlFormat_PTalternative Formatter = null;

        #region events

        protected void HtmlGenerator_ShowMessage(string message, bool isError = false, bool isFatal = false)
        {
            StaticObjects.FireShowMessage(message);
        }
        #endregion

        //protected int GetIso8601WeekOfYear(DateTime time)
        //{
        //    DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
        //    if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
        //    {
        //        time = time.AddDays(3);
        //    }
        //    return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        //}

        //protected int WeekDay()
        //{
        //    DateTime currentDate = DateTime.Now;
        //    return GetIso8601WeekOfYear(currentDate);
        //}

        protected void PrintPaper(Translation englishTranslation, Translation translatioRight, short paperNoToPrint)
        {
            StaticObjects.FireShowMessage($"Exporting paper {paperNoToPrint}");
            StaticObjects.FireShowPaperNumber(paperNoToPrint);
            Paper paperEnglish = englishTranslation.Paper(paperNoToPrint);
            PaperEdit paperEdit = new PaperEdit(paperNoToPrint, Param.EditParagraphsRepositoryFolder);
            Formatter.GenerateGitHubPage(paperEnglish, paperEdit.Paragraphs, Param.EditBookRepositoryFolder, paperNoToPrint, null, true);
        }

        public abstract bool Print(Translation englishTranslation, Translation portuguse2007Translation, Translation ptAlternativeTranslation, short paperNoToPrint = -1);

    }
}
