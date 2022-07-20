using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UbStandardObjects;
using UbStandardObjects.Objects;

namespace UbStudyHelpGenerator.Classes
{
    internal class BookGenerator : Book
    {
        public override void DeleteAnnotations(TOC_Entry entry)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// In the book generator, the initialize initializes empty lists
        /// </summary>
        /// <param name="baseDataPath"></param>
        /// <param name="leftTranslationId"></param>
        /// <param name="rightTranslationID"></param>
        /// <returns></returns>
        public override bool Inicialize(string baseDataPath, short leftTranslationId, short rightTranslationID)
        {
			FilesPath = baseDataPath;
			Translations = new List<Translation>();
			return true;
		}

        public override void StoreAnnotations(TOC_Entry entry, List<UbAnnotationsStoreData> annotations)
        {
            throw new NotImplementedException();
        }
    }
}
