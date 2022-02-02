using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UbStudyHelpGenerator.Generators.Classes
{

    public class Translation
    {
        public int LanguageID { get; set; }
        public string Description { get; set; }
        public string TUB { get; set; }
        public string PaperTranslation { get; set; }
        public string TIN { get; set; }
        public bool UseBold { get; set; }
        public bool RightToLeft { get; set; }
        public int CultureID { get; set; }
    }

    public class Translations
    {
        public Translation[] Translation { get; set; }
    }



}
