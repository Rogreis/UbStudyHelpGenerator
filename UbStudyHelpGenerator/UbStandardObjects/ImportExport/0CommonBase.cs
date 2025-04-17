using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UbStudyHelpGenerator.UbStandardObjects.ImportExport.Models;

namespace UbStudyHelpGenerator.UbStandardObjects.ImportExport
{
    public abstract class _0CommonBase
    {
        /// <summary>
        /// Path to the LiteDB database inside the AmadonWeb repository.
        /// </summary>
        protected BookExport BookEnglish = new BookExport("en");
        protected BookExport BookPtBr = new BookExport("pt_br");

        protected string HtmlToUtf8(string input)
        {
            return WebUtility.HtmlDecode(input);
        }


        /// <summary>
        /// Execute the main function of the class
        /// </summary>
        /// <param name="pathBase">
        ///     A base path to be used: for import it could be location of md file or repository folder
        ///     For export destination of the files produced
        /// </param>
        public abstract void Run(string pathBase, string pathDatabase);
    }
}
