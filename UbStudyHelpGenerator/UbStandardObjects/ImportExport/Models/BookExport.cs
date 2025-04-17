using System;
using System.Collections.Generic;

namespace UbStudyHelpGenerator.UbStandardObjects.ImportExport.Models
{
    public class BookExport
    {
        public const string EnglishTranslation = "en";
        public const string PtBrTranslation = "pt_br";

        public const int MaxPapers = 197;
        public string TranslationLanguage { get; set; } = "en";
        public List<PaperExport> Papers { get; set; }= new List<PaperExport>();

        public BookExport(string translationLanguage)
        {
            if (translationLanguage != EnglishTranslation && translationLanguage != PtBrTranslation)
            {
                throw new ArgumentException("Invalid translation language. Use 'en' or 'pt_br'.");
            }
            switch(translationLanguage)
            {
                case EnglishTranslation:
                    TranslationLanguage = EnglishTranslation;
                    break;
                case PtBrTranslation:
                    TranslationLanguage = PtBrTranslation;
                    break;
            }
        }

        public override string ToString()
        {
            return TranslationLanguage;
        }


    }
}
