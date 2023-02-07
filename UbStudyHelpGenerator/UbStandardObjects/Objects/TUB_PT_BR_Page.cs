using System;
using System.Collections.Generic;
using System.Text;

namespace UbStudyHelpGenerator.UbStandardObjects.Objects
{
    public class TUB_PT_BR_Page : BootstrapBook
    {
        public TUB_PT_BR_Page(Parameters parameters) : base(parameters)
        {
        }

        public void GenerateGitHubPage(string destinationFolder, Paper leftPaper, Paper rightPaper, TUB_TOC_Html toc_table, short paperNo)
        {
            try
            {
                FireSendMessage(leftPaper.ToString());
                PrintPaperForGitHubWebSite(destinationFolder, paperNo, leftPaper, rightPaper, toc_table);
            }
            catch (Exception)
            {

                throw;
            }
        }




    }
}
