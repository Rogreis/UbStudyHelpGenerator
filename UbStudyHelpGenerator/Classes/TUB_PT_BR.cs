using System;
using UbStudyHelpGenerator.UbStandardObjects;
using UbStudyHelpGenerator.UbStandardObjects.Objects;

namespace UbStudyHelpGenerator.Classes
{
    public class TUB_PT_BR
    {
        public event ShowPaperNumber ShowPaperNumber = null;

        public event ShowStatusMessage ShowStatusMessage = null;

        private Parameters Param = null;

        private TUB_PT_BR_Page Formatter = null;

        #region events
        private void FireShowMessage(string message)
        {
            StaticObjects.FireSendMessage(message);
        }

        private void FireShowStatusMessage(string message)
        {
            ShowStatusMessage?.Invoke(message);
        }

        private void FireShowPaperNumber(short paperNo)
        {
            ShowPaperNumber?.Invoke(paperNo);
        }

        private void HtmlGenerator_ShowPaperNumber(short paperNo)
        {
            ShowPaperNumber?.Invoke(paperNo);
        }

        private void HtmlGenerator_ShowMessage(string message, bool isError = false, bool isFatal = false)
        {
            StaticObjects.FireSendMessage(message);
        }
        #endregion

        public TUB_PT_BR(Parameters param, TUB_PT_BR_Page formatter)
        {
            Param = param;
            Formatter = formatter;
        }

        public void MainPage(string mainPageFilePath)
        {
            //StringBuilder sb = new StringBuilder();
            //Formatter.MainPage(sb);
            //File.WriteAllText(mainPageFilePath, sb.ToString(), Encoding.UTF8);
        }

        public bool RepositoryToBookHtmlPages(TUB_TOC_Html toc_table, Translation translatioLeft, Translation translatioRight)
        {
            try
            {
                for (short paperNo = 0; paperNo < 197; paperNo++)
                {
                    FireShowMessage($"Exporting paper {paperNo}");
                    FireShowPaperNumber((short)paperNo);
                    Paper paperEnglish = translatioLeft.Paper(paperNo);
                    PaperEdit paperEdit = new PaperEdit(paperNo, Param.EditParagraphsRepositoryFolder);
                    Formatter.GenerateGitHubPage(Param.EditBookRepositoryFolder, paperEnglish, paperEdit, toc_table, paperNo);
                }
                FireShowMessage("Finished");
                return true;
            }
            catch (Exception ex)
            {
                FireShowMessage($"Exporting translation alternative {ex.Message}");
                UbStandardObjects.StaticObjects.Logger.Error("Exporting translation alternative", ex);
                return false;
            }
        }

        public void Test() => Formatter.Test();

    }
}
