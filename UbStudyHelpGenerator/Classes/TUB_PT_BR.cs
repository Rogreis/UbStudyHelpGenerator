using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UbStandardObjects;
using UbStandardObjects.Objects;

namespace UbStudyHelpGenerator.Classes
{
    public class TUB_PT_BR
    {
        public event ShowMessage ShowMessage = null;

        public event ShowPaperNumber ShowPaperNumber = null;

        public event ShowStatusMessage ShowStatusMessage = null;

        private ParametersGenerator Param = null;

        #region events
        private void FireShowMessage(string message)
        {
            ShowMessage?.Invoke(message);
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
            ShowMessage?.Invoke(message);
        }
        #endregion

        public TUB_PT_BR(ParametersGenerator param)
        {
            Param = param;
        }

        public bool RepositoryToBookHtmlPages(BootstrapBook Formatter, TUB_TOC_Html toc_table)
        {
            try
            {
                for (short paperNo = 0; paperNo < 197; paperNo++)
                {
                    FireShowMessage($"Exporting paper {paperNo}");
                    FireShowPaperNumber((short)paperNo);
                    string pathPaperFolder = Path.Combine(Param.EditParagraphsRepositoryFolder, $"Doc{paperNo:000}");

                    Paper paper = new Paper();
                    foreach (string file in Directory.GetFiles(pathPaperFolder, "*.md"))
                    {
                        ParagraphMarkDown paragraph = new ParagraphMarkDown(file);
                        paper.Paragraphs.Add(paragraph);
                    }
                    Formatter.GeneratePaper(Param.EditBookRepositoryFolder, Param.TranslationLeft, Param.TranslationRight, toc_table, paperNo);
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


    }
}
